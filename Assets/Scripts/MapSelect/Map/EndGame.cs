using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    static public EndGame instance;

    public GameObject WeaponResultSlotList;
    public GameObject InitWeaponResult;
    public GameObject PowerUpBar;
    public GameObject InitPowerUpData;
    public GameObject CharaPowerUpBar;
    public GameObject InitCharaPowerUpData;
    public GameObject DropItemBar;
    public GameObject InitDropItemData;
    public GameObject LightSourceData;

    public GameObject Option;
    public GameObject Death;
    public GameObject GameResult;

    public Text StageNameValue;
    public Text TimeLimitValue;
    public Text GoldCountValue;
    public Text PlayerLvValue;
    public Text DefeatEnemyValue;

    public Image CharaImage;
    public Text CharaName;

    public Button EndGameButton;

    public Enemy Skelton;
    public Enemy LionHead;
    public Enemy MilkElementals;
    public Enemy DragonShrimps;
    public Enemy Map1Boss;
    public Enemy Map2Boss;
    public Enemy Map3Boss;
    public Enemy Map4Boss;

    private Map Map;
    private Mob Mob;
    private PlayerData PlayerData;
    private Timer Timer;

    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MapDataBase MapDataBase = Json.instance.MapDataBase;
    private readonly MobDataBase MobDataBase = Json.instance.MobDataBase;
    private readonly ItemDataBase ItemDataBase = Json.instance.ItemDataBase;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Map = MapDataBase.FindMapFromId(player.Latest_Map);
        Mob = MobDataBase.FindMobFromId(player.Latest_Chara);
    }

    public void ShowGameResult()
    {
        Option.SetActive(false);
        Death.SetActive(false);
        GameResult.SetActive(true);
        Setting.instance.StopButtonCanvasAdd();

        PlayerData = PlayerStatus.instance.GetStatus();
        Timer = CountTimer.instance.GetTimer();

        SetStageName();
        SetClearTime();
        SetGoldCount();
        SetPlayerLv();
        SetDefeatCount();
        SetWeaponResult();
        SetCharaImage();
        SetCharaName();
        SetPowerUpBar();
        SetCharaPowerUpBar();
        SetDropItemBar();
        CountUpDefeatEnemy();
        UnlockAchievement();

        EndGameButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("Title");
            Music.instance.UnPauseMusic();
            Music.instance.PlayRandomMusic();
        });
    }

    private void SetStageName()
    {
        string StageType = player.Latest_Map_Hyper ? "ハイパー" : "ノーマル";
        StageNameValue.text = $"{Map.GetName()} - {StageType}";
    }

    private void SetClearTime()
    {
        string SecondText = Timer.Second.ToString().PadLeft(2, '0');

        if (Timer.Minute >= 30)
        {
            TimeLimitValue.text = $"(MAX) {Timer.Minute}:{SecondText}";
            TimeLimitValue.color = new Color(1, 1, 0, 1);
        } else
        {
            TimeLimitValue.text = $"{Timer.Minute}:{SecondText}";
        }
    }

    private void SetGoldCount()
    {
        GoldCountValue.text = PlayerData.Emerald.ToString();
        player.Money += PlayerData.Emerald;
    }

    private void SetPlayerLv()
    {
        PlayerLvValue.text = PlayerData.Lv.ToString();
    }

    private void SetDefeatCount()
    {
        DefeatEnemyValue.text = PlayerData.Defeat.ToString();
    }

    private static string ConvertFormat(float d)
    {
        d = (int)d;
        if (d == 0f) return d.ToString();

        float exponent = Mathf.Log10(Mathf.Abs(d));
        switch ((int)Mathf.Floor(exponent))
        {
            case 0:
            case 1:
            case 2:
                return d.ToString();
            case 3:
            case 4:
            case 5:
                return (d / 1e3).ToString() + "K";
            case 6:
            case 7:
            case 8:
                return (d / 1e6).ToString() + "M";
            case 9:
            case 10:
            case 11:
                return (d / 1e9).ToString() + "B";
            default:
                return (d / 1e12).ToString() + "T";
        }
    }

    private void SetWeaponResult()
    {
        float MaxWeaponDamage = 0;
        float MaxWeaponDPS = 0;
        Text MaxWeaponDamageText = null;
        Text MaxWeaponDPSText = null;

        List<AllWeaponData> AllWeaponDataList = WeaponStatus.instance.GetAllStatusList();
        for (int i = 0; i < AllWeaponDataList.Count; i++)
        {
            AllWeaponData AllWeaponData = AllWeaponDataList[i];
            Weapon weapon = AllWeaponData.weapon;

            GameObject Object = Instantiate(InitWeaponResult);
            Object.transform.localScale = new Vector3(1 / 34.36427f, 1 / 34.36427f, 1 / 34.36427f);
            Object.name = $"WeaponResult_{weapon.GetId()}";
            Object.SetActive(true);

            GameObject ImageObject = Object.transform.Find("WeaponImage").gameObject;
            Image ImageObjectImage = ImageObject.GetComponent<Image>();
            ImageObjectImage.sprite = weapon.GetIcon();

            GameObject NameObject = Object.transform.Find("WeaponName").gameObject;
            Text NameObjectText = NameObject.GetComponent<Text>();
            NameObjectText.text = weapon.GetName();

            GameObject LvObject = Object.transform.Find("WeaponLv").gameObject;
            Text LvObjectText = LvObject.GetComponent<Text>();
            LvObjectText.text = AllWeaponData.Phase.ToString();

            GameObject DamageObject = Object.transform.Find("WeaponDamage").gameObject;
            Text DamageObjectText = DamageObject.GetComponent<Text>();
            DamageObjectText.text = ConvertFormat(AllWeaponData.Damage).ToString();
            if (AllWeaponData.Damage >= MaxWeaponDamage)
            {
                MaxWeaponDamage = AllWeaponData.Damage;
                MaxWeaponDamageText = DamageObjectText;
            }

            GameObject TimeObject = Object.transform.Find("WeaponTime").gameObject;
            Text TimeObjectText = TimeObject.GetComponent<Text>();
            TimeObjectText.text = AllWeaponData.SetTime;

            float BitweenSecound = Timer.AllSecond - AllWeaponData.SetSecoundTime;
            float DPS = BitweenSecound != 0 ? AllWeaponData.Damage / BitweenSecound : 0;
            GameObject DPSObject = Object.transform.Find("WeaponDPS").gameObject;
            Text DPSObjectText = DPSObject.GetComponent<Text>();
            DPSObjectText.text = ConvertFormat(DPS).ToString();
            if (DPS >= MaxWeaponDPS)
            {
                MaxWeaponDPS = DPS;
                MaxWeaponDPSText = DPSObjectText;
            }

            Object.transform.SetParent(WeaponResultSlotList.transform);
        }

        MaxWeaponDamageText.color = new Color(1, 1, 0, 1);
        MaxWeaponDPSText.color = new Color(1, 1, 0, 1);
    }

    private void SetCharaImage()
    {
        CharaImage.sprite = Mob.GetIcon();
    }

    private void SetCharaName()
    {
        CharaName.text = Mob.GetName();
    }

    private GameObject CreatePowerUpBarObject(Sprite Icon, int Phase, bool MaxPhase)
    {
        GameObject Object = Instantiate(InitPowerUpData);
        Object.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);
        Object.SetActive(true);

        GameObject ImageObject = Object.transform.Find("PowerUp").transform.Find("PowerUp").gameObject;
        Image ImageObjectImage = ImageObject.GetComponent<Image>();
        ImageObjectImage.sprite = Icon;

        GameObject PhaseLvObject = Object.transform.Find("Phase").transform.Find("PhaseLv").gameObject;
        Text PhaseLvObjectText = PhaseLvObject.GetComponent<Text>();
        PhaseLvObjectText.text = Phase.ToString();
        PhaseLvObjectText.color = MaxPhase ? new Color(1, 1, 0, 1) : new Color(1, 1, 1, 1);

        Object.transform.SetParent(PowerUpBar.transform);
        return Object;
    }

    private void SetPowerUpBar()
    {
        List<ItemData> ItemDataList = ItemStatus.instance.GetStatusList();
        for (int i = 0; i < ItemDataList.Count; i++)
        {
            ItemData ItemData = ItemDataList[i];
            Item item = ItemData.item;

            GameObject Object = CreatePowerUpBarObject(item.GetIcon(), ItemData.phase, ItemData.phase == item.GetPlayCount());
            Object.name = $"PowerUpBar_item_{item.GetId()}";
        }

        List<WeaponData> WeaponDataList = WeaponStatus.instance.GetStatusList();
        for (int i = 0; i < WeaponDataList.Count; i++)
        {
            WeaponData WeaponData = WeaponDataList[i];
            Weapon weapon = WeaponData.weapon;

            GameObject Object = CreatePowerUpBarObject(weapon.GetIcon(), WeaponData.phase, WeaponData.phase == weapon.GetPlayCount());
            Object.name = $"PowerUpBar_weapon_{weapon.GetId()}";
        }
    }

    private GameObject CreateCharaPowerUpBarObject(Sprite Icon, int Phase, bool MaxPhase)
    {
        GameObject Object = Instantiate(InitCharaPowerUpData);
        Object.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);
        Object.SetActive(true);

        GameObject ImageObject = Object.transform.Find("PowerUp").transform.Find("PowerUp").gameObject;
        Image ImageObjectImage = ImageObject.GetComponent<Image>();
        ImageObjectImage.sprite = Icon;

        GameObject PhaseLvObject = Object.transform.Find("Phase").transform.Find("PhaseLv").gameObject;
        Text PhaseLvObjectText = PhaseLvObject.GetComponent<Text>();
        PhaseLvObjectText.text = Phase.ToString();
        PhaseLvObjectText.color = MaxPhase ? new Color(1, 1, 0, 1) : new Color(1, 1, 1, 1);

        Object.transform.SetParent(CharaPowerUpBar.transform);
        return Object;
    }

    private void SetCharaPowerUpBar()
    {
        List<Item> ItemList = ItemDataBase.GetItemLists();
        for (int i = 0; i < ItemList.Count; i++)
        {
            Item Item = ItemList[i];
            Json.PowerUpList PoweroupData = player.PowerUp.poweruplist.Find(data => data.id == Item.GetId());
            int PowerUpCount = Item.GetCount() - PoweroupData.powerupcount;
            if (PowerUpCount == 0) continue;

            GameObject Object = CreateCharaPowerUpBarObject(Item.GetIcon(), PowerUpCount, Item.GetCount() == PowerUpCount);
            Object.name = $"CharaPowerUpBar_item_{Item.GetId()}";
        }
    }

    private GameObject CreateDropItemBarObject(Sprite Icon, int Phase)
    {
        GameObject Object = Instantiate(InitDropItemData);
        Object.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);
        Object.SetActive(true);

        GameObject ImageObject = Object.transform.Find("SpecialItem").transform.Find("Icon").gameObject;
        Image ImageObjectImage = ImageObject.GetComponent<Image>();
        ImageObjectImage.sprite = Icon;

        GameObject PhaseLvObject = Object.transform.Find("Count").transform.Find("CountText").gameObject;
        Text PhaseLvObjectText = PhaseLvObject.GetComponent<Text>();
        PhaseLvObjectText.text = Phase.ToString();

        Object.transform.SetParent(DropItemBar.transform);
        return Object;
    }

    private void SetDropItemBar()
    {
        DropItemCount DropItemCount = DropItemStatus.instance.GetDropItemCount();
        LightSourceData.SetActive(DropItemCount.LightSource != 0);
        if (LightSourceData.activeSelf)
        {
            GameObject CountObject = LightSourceData.transform.Find("LightSourceCount").transform.Find("CountText").gameObject;
            Text CountObjectText = CountObject.GetComponent<Text>();
            CountObjectText.text = DropItemCount.LightSource.ToString();
        }

        for (int i = 0; i < DropItemCount.DropItemCountData.Count; i++)
        {
            DropItemCountData DropItemCountData = DropItemCount.DropItemCountData[i];
            CreateDropItemBarObject(DropItemCountData.sprite, DropItemCountData.Count);
        }
    }

    private void CountUpDefeatEnemy()
    {
        List<AllEnemyData> GetAllEnemyDataList = EnemyStatus.instance.GetAllEnemyDataList();
        GetAllEnemyDataList.ForEach(enemy =>
        {
            player.Enemy.Find(e => e.id == enemy.enemy.GetId()).count += enemy.DefeatCount;
        });
    }

    private void UnlockAchievement()
    {
        DropItemCount DropItemCount = DropItemStatus.instance.GetDropItemCount();
        List<DropItemCountData> DropItemList = DropItemCount.DropItemCountData;

        List<AllEnemyData> GetAllEnemyDataList = EnemyStatus.instance.GetAllEnemyDataList();

        int ReturnDropItemCount(int id)
        {
            DropItemCountData Data = DropItemCount.DropItemCountData.Find(d => d.id == id);
            return Data == null ? 0 : Data.Count; 
        }

        void UnlockFunction(string Type, int id)
        {
            if (Type == "item") player.Item.Find(i => i.id == id).use = true;
            if (Type == "weapon" || Type == "money")
            {
                if (Type == "moeny" && player.Weapon.Find(w => w.id == id).use == false)
                {
                    player.Money += 500;
                }
                player.Weapon.Find(w => w.id == id).use = true;
            }
            if (Type == "chara")
            {
                player.Character.Find(c => c.id == id).use = true;
                player.Character.Find(c => c.id == id).hidden = false;
            }
            if (Type == "map") player.Map.Find(m => m.id == id).use = true;
            if (Type == "hyper") player.Map.Find(m => m.id == id).hyper = true;

            player.Achievement.Find(a => a.type == Type && a.type_id == id).use = true;
        }

        if (PlayerData.Lv >= 5) UnlockFunction("item", 10);
        if (PlayerData.Lv >= 10) UnlockFunction("item", 13);
        if (player.Latest_Map == 1 && PlayerData.Lv >= 20) UnlockFunction("map", 2);
        if (player.Latest_Map == 2 && PlayerData.Lv >= 40) UnlockFunction("map", 3);
        if (player.Latest_Map == 3 && PlayerData.Lv >= 60) UnlockFunction("map", 4);
        if (Timer.Minute >= 1) UnlockFunction("item", 3);
        if (Timer.Minute >= 5 && player.Latest_Chara == 2) UnlockFunction("weapon", 19);
        if (Timer.Minute >= 5 && player.Latest_Chara == 3) UnlockFunction("item", 4);
        if (Timer.Minute >= 10) UnlockFunction("weapon", 25);
        if (Timer.Minute >= 10 && player.Latest_Chara == 12) UnlockFunction("weapon", 28);
        if (Timer.Minute >= 15 && player.Latest_Chara == 12) UnlockFunction("weapon", 29);
        if (Timer.Minute >= 15 && player.Latest_Chara == 14) UnlockFunction("weapon", 33);
        if (Timer.Minute >= 15 && player.Latest_Chara == 13) UnlockFunction("weapon", 31);
        if (Timer.Minute >= 15 && player.Latest_Chara == 15) UnlockFunction("weapon", 35);
        if (Timer.Minute >= 20) UnlockFunction("weapon", 23);
        if (Timer.Minute >= 20 && player.Latest_Chara == 10) UnlockFunction("item", 16);
        if (Timer.Minute >= 20 && ItemStatus.instance.GetStatusPhase(15) >= 1) UnlockFunction("chara", 6);
        if (Timer.Minute >= 30 && player.Latest_Chara == 6) UnlockFunction("item", 15);
        if (WeaponStatus.instance.GetStatusPhase(11) >= 4) UnlockFunction("item", 7);
        if (WeaponStatus.instance.GetStatusPhase(17) >= 4) UnlockFunction("item", 6);
        if (WeaponStatus.instance.GetStatusPhase(13) >= 4) UnlockFunction("chara", 4);
        if (WeaponStatus.instance.GetStatusPhase(21) >= 4) UnlockFunction("chara", 5);
        if (WeaponStatus.instance.GetStatusPhase(3) >= 7) UnlockFunction("item", 9);
        if (WeaponStatus.instance.GetStatusPhase(25) >= 7) UnlockFunction("weapon", 26);
        if (WeaponStatus.instance.GetStatusPhase(19) >= 7) UnlockFunction("item", 8);
        if (WeaponStatus.instance.GetStatusPhase(15) >= 7) UnlockFunction("chara", 7);
        if (WeaponStatus.instance.GetStatusPhase(23) >= 7) UnlockFunction("chara", 11);
        if (WeaponStatus.instance.GetStatusList().Count >= 6) UnlockFunction("item", 5);
        if (player.GrantHP >= 1000) UnlockFunction("chara", 8);
        if (PlayerStatus.instance.GetStatus().Emerald >= 5000) UnlockFunction("chara", 8);
        if (DropItemCount.LightSource >= 20) UnlockFunction("weapon", 13);
        if (ReturnDropItemCount(9) >= 5) UnlockFunction("weapon", 15);
        if (ReturnDropItemCount(11) >= 1) UnlockFunction("item", 12);
        if (ReturnDropItemCount(8) >= 1) UnlockFunction("item", 11);
        if (ReturnDropItemCount(7) >= 1) UnlockFunction("weapon", 37);
        if (ReturnDropItemCount(5) >= 1) UnlockFunction("weapon", 9);
        if (ItemStatus.instance.GetStatusPhase(14) >= 1) UnlockFunction("item", 14);
        if (player.Latest_Map == 1 && Timer.Minute >= 30) UnlockFunction("chara", 12);
        if (player.Latest_Map == 2 && Timer.Minute >= 30) UnlockFunction("chara", 13);
        if (player.Latest_Map == 3 && Timer.Minute >= 30) UnlockFunction("chara", 14);
        if (player.Latest_Map == 4 && Timer.Minute >= 30) UnlockFunction("chara", 15);
        if (player.Enemy.Find(e => e.id == Skelton.GetId()).count >= 3000) UnlockFunction("chara", 16);
        if (player.Enemy.Find(e => e.id == LionHead.GetId()).count >= 3000) UnlockFunction("chara", 17);
        if (player.Enemy.Find(e => e.id == MilkElementals.GetId()).count >= 3000) UnlockFunction("chara", 18);
        if (player.Enemy.Find(e => e.id == DragonShrimps.GetId()).count >= 3000) UnlockFunction("chara", 19);
        if (player.Enemy.Sum(e => e.count) >= 5000) UnlockFunction("weapon", 21);
        if (player.Enemy.Sum(e => e.count) >= 10000) UnlockFunction("chara", 10);
        if (Timer.Minute >= 25 && GetAllEnemyDataList.Any(e => e.enemy == Map1Boss)) UnlockFunction("hyper", 1);
        if (Timer.Minute >= 25 && GetAllEnemyDataList.Any(e => e.enemy == Map2Boss)) UnlockFunction("hyper", 2);
        if (Timer.Minute >= 25 && GetAllEnemyDataList.Any(e => e.enemy == Map3Boss)) UnlockFunction("hyper", 3);
        if (Timer.Minute >= 25 && GetAllEnemyDataList.Any(e => e.enemy == Map4Boss)) UnlockFunction("hyper", 4);
        if (WeaponStatus.instance.GetStatusPhase(2) != 0) UnlockFunction("money", 2);
        if (WeaponStatus.instance.GetStatusPhase(4) != 0) UnlockFunction("money", 4);
        if (WeaponStatus.instance.GetStatusPhase(6) != 0) UnlockFunction("money", 6);
        if (WeaponStatus.instance.GetStatusPhase(18) != 0) UnlockFunction("money", 18);
        if (WeaponStatus.instance.GetStatusPhase(22) != 0) UnlockFunction("money", 22);
        if (WeaponStatus.instance.GetStatusPhase(12) != 0) UnlockFunction("money", 12);
        if (WeaponStatus.instance.GetStatusPhase(10) != 0) UnlockFunction("money", 10);
        if (WeaponStatus.instance.GetStatusPhase(14) != 0) UnlockFunction("money", 14);
        if (WeaponStatus.instance.GetStatusPhase(16) != 0) UnlockFunction("money", 16);
        if (WeaponStatus.instance.GetStatusPhase(20) != 0) UnlockFunction("money", 20);
        if (WeaponStatus.instance.GetStatusPhase(24) != 0) UnlockFunction("money", 24);
        if (WeaponStatus.instance.GetStatusPhase(27) != 0) UnlockFunction("money", 27);
        if (WeaponStatus.instance.GetStatusPhase(30) != 0) UnlockFunction("money", 30);
        if (WeaponStatus.instance.GetStatusPhase(32) != 0) UnlockFunction("money", 32);
        if (WeaponStatus.instance.GetStatusPhase(34) != 0) UnlockFunction("money", 34);
        if (WeaponStatus.instance.GetStatusPhase(36) != 0) UnlockFunction("money", 36);

        Json.instance.Save(player);
    }
}
