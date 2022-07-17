using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int MaxHP = 100;
    public int HP = 100;
    public int Emerald = 0;
    public int Defeat = 0;
    public int ALLEXP = 0;
    public int EXP = 0;
    public int Lv = 1;
}

public interface IData
{
    int id { get; set; }
    int phase { get; set; }
    string type { get; set; }
}

public class IDataObject
{
    public Sprite Image;
    public string Type;
    public string Name;
    public string Phase;
    public string Description;
}

public class PlayerStatus : MonoBehaviour
{
    static public PlayerStatus instance;

    public Text DefeatCount;
    public Text EmeraldCount;
    public Text LvCount;
    public RectTransform ExpBar;

    public GameObject Chara;
    public GameObject HeartsBar;
    public GameObject DamageEffects;
    public GameObject LvUpSlotList;
    public GameObject OptionLevelUpObject;
    public GameObject Slot4Text;

    public Image Option;

    public Sprite FullHp;
    public Sprite HalfHp;
    public Sprite NoHp;
    public Sprite DamageEffectImage;
    public Sprite SpecialItemId3;
    public Sprite SpecialItemId9;

    public AudioClip LvUp;
    public AudioClip PlayerDamage;

    private readonly static float MaxPercentEXP = 21.469f;
    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly WeaponDataBase WeaponDataBase = Json.instance.WeaponDataBase;
    private readonly ItemDataBase ItemDataBase = Json.instance.ItemDataBase;

    private List<Json.WeaponData> CanUseWeaponList;
    private List<Json.ItemData> CanUseItemList;

    private PlayerData PlayerData = new PlayerData();
    private List<IDataObject> IDataObjectList = new List<IDataObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitStatus();
        CanUseWeaponList = player.Weapon.FindAll(w => w.use);
        CanUseItemList = player.Item.FindAll(i => i.use);
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        int NeedExp = NeedExpToNextLv();
        ExpBar.anchoredPosition = new Vector3(EXPBarX(NeedExp), 0, 0);
        if (PlayerData.EXP < NeedExp) return;

        PlayerLvUP(NeedExp);
    }

    private void InitStatus()
    {
        PlayerData.MaxHP = 100 + 10 * ItemStatus.instance.GetAllStatusPhase(3);
        PlayerData.HP = PlayerData.MaxHP;
    }

    public PlayerData GetStatus()
    {
        return PlayerData;
    }

    public void UpdateEmeraldCount(int AddInt)
    {
        PlayerData.Emerald += AddInt;
        EmeraldCount.text = PlayerData.Emerald.ToString();
    }

    public void UpdateDefeatCount()
    {
        PlayerData.Defeat++;
        DefeatCount.text = PlayerData.Defeat.ToString();
    }

    public void UpdateLvCount()
    {
        PlayerData.Lv++;
        LvCount.text = PlayerData.Lv.ToString();
    }

    public void UpdateHpStatus(int GrantHP)
    {
        PlayerData.HP += GrantHP;
        if (PlayerData.HP >= PlayerData.MaxHP) PlayerData.HP = PlayerData.MaxHP;

        UpdateHpBar(GrantHP);
        DisplayDamageEffect(GrantHP);

        if (PlayerData.HP <= 0)
        {

        }
    }

    private void DisplayDamageEffect(int damage)
    {
        if (damage >= 0) return;

        RectTransform EnemyDataObjectRectTransform = Chara.GetComponent<RectTransform>();
        float EnemyX = EnemyDataObjectRectTransform.anchoredPosition.x;
        float EnemyY = EnemyDataObjectRectTransform.anchoredPosition.y;

        GameObject Object = new GameObject("DamageEffect");
        Object.transform.SetParent(DamageEffects.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(2, 2);
        ObjectRectTransform.anchoredPosition = new Vector3(EnemyX, EnemyY, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = DamageEffectImage;
        ObjectImage.color = new Color(1, 0, 0, 1);

        Object.AddComponent<DamageEffect>();
        Music.instance.SoundEffect(PlayerDamage);
    }

    private GameObject GetHeartObject(int id)
    {
        return HeartsBar.transform.Find($"Heart_{id}").transform.Find("Heart").gameObject;
    }

    private Sprite ReturnHeartImage(int OnesPlace, int TensPlace, int i)
    {
        if (TensPlace == 10) return FullHp;
        if (TensPlace != i || OnesPlace == 0) return NoHp;
        return OnesPlace <= 5 ? HalfHp : FullHp;
    }

    private void UpdateHpBar(int GrantHP)
    {
        int HpPercentage = 100 * PlayerData.HP / PlayerData.MaxHP;
        int TensPlace = Mathf.FloorToInt(HpPercentage / 10);
        int OnesPlace = HpPercentage % 10;
        for (int i = 9; i >= 0; i--)
        {
            GameObject HeartObject = GetHeartObject(i);
            Image ObjectImage = HeartObject.GetComponent<Image>();
            Sprite sprite = ReturnHeartImage(OnesPlace, TensPlace, i);
            ObjectImage.sprite = sprite;

            if (GrantHP <= 0)
            {
                if (sprite != NoHp) break;
                if (OnesPlace == 0 && TensPlace == i) break;
            };
        }
    }

    private int NeedExpToNextLv()
    {
        int InitLv = 2;
        int NextLv = PlayerData.Lv + 1;
        int InitEXP = 5;

        while (NextLv > InitLv)
        {
            if (2 <= InitLv && InitLv < 20) InitEXP += 10;
            if (InitLv == 20) InitEXP += 600;
            if (21 <= InitLv && InitLv < 40) InitEXP += 13;
            if (InitLv == 40) InitEXP += 2400;
            if (41 <= InitLv) InitEXP += 16;

            InitLv++;
        }
        return InitEXP;
    }

    public void UpdateExpStatus(int GrantEXP)
    {
        GrantEXP += 10000;
        PlayerData.ALLEXP += GrantEXP;
        PlayerData.EXP += GrantEXP;
    }

    private float EXPBarX(int NeedExp)
    {
        if (PlayerData.EXP > NeedExp) return 0;

        float Parsentage = (float)PlayerData.EXP / NeedExp;
        return -MaxPercentEXP + Parsentage * MaxPercentEXP;
    }

    private void  PlayerLvUP(int NeedExp)
    {
        Music.instance.SoundEffect(LvUp);
        UpdateLvCount();
        ExpBar.anchoredPosition = new Vector3(-MaxPercentEXP, 0, 0);
        PlayerData.EXP -= NeedExp;

        Setting.instance.OpenOption(OptionLevelUpObject);
        FallExpList.instance.onFallExp();
        Option.color = new Color(0, 0, 0, 0);
        for (int i = 1; i <= 4; i++)
        {
            LvUPSlotButton(i);
        }
    }

    private GameObject GetLvUPSlotObject(int index)
    {
        return LvUpSlotList.transform.Find($"Slot_{index}").gameObject;
    }

    private bool FourthSlotChance(int index, GameObject Object)
    {
        if (index != 4) return true;

        float ChanceForth = (1 / ItemStatus.instance.GetAllStatusPhase(12)) * 100;
        bool DiscplaySlot = ExpStatus.instance.Probability(ChanceForth);

        Object.SetActive(DiscplaySlot);
        Slot4Text.SetActive(!DiscplaySlot);
        return DiscplaySlot;
    }

    private void LvUPSlotButton(int index)
    {
        GameObject Object = GetLvUPSlotObject(index);
        if (!FourthSlotChance(index, Object)) return;

        IDataObject IDataObject = GetSlotItem(index);
        if (IDataObject == null)
        {
            Slot4Text.SetActive(false);
            Object.SetActive(false);
            return;
        };

        IDataObjectList.Add(IDataObject);

        GameObject SlotItemObject = Object.transform.Find("SlotItemBackGround").transform.Find("SlotItemImage").gameObject;
        Image SlotItemObjectImage = SlotItemObject.GetComponent<Image>();
        SlotItemObjectImage.sprite = IDataObject.Image;

        GameObject NameObject = Object.transform.Find("Name").gameObject;
        Text NameObjectText = NameObject.GetComponent<Text>();
        NameObjectText.text = IDataObject.Name;

        GameObject PhaseObject = Object.transform.Find("Phase").gameObject;
        Text PhaseObjectText = PhaseObject.GetComponent<Text>();
        PhaseObjectText.text = IDataObject.Phase;
        PhaseObjectText.color = IDataObject.Phase == "NEW!" ? new Color(1, 1, 0, 1) : new Color(1, 1, 1, 1);

        GameObject DescriptionObject = Object.transform.Find("Description").gameObject;
        Text DescriptionObjectText = DescriptionObject.GetComponent<Text>();
        DescriptionObjectText.text = IDataObject.Description;

        Button ObjectButton = Object.GetComponent<Button>();
        ObjectButton.onClick.RemoveAllListeners();
        ObjectButton.onClick.AddListener(() =>
        {
            UpdateData(IDataObject);
            IDataObjectList = new List<IDataObject>();

            FallExpList.instance.offFallExp();
            Setting.instance.CloseOption(OptionLevelUpObject);
            Option.color = new Color(0, 0, 0, 0.75f);
        });
    }

    private void UpdateData(IDataObject IDataObject)
    {
        if (IDataObject.Type == "special" && IDataObject.Name == "ステーキ")
        {
            UpdateHpStatus(30);
        }
        else if (IDataObject.Type == "special" && IDataObject.Name == "エメラルド原石")
        {
            UpdateEmeraldCount(25);
        }
        else if (IDataObject.Type == "item")
        {
            List<ItemData> ItemDataList = ItemStatus.instance.GetStatusList();
            Item item = ItemDataBase.FindItemFromName(IDataObject.Name);
            if (!ItemDataList.Any(i => i.item == item))
            {
                ItemStatus.instance.AddItemDataList(item);
            }
            else
            {
                ItemStatus.instance.UpdateItemPhase(item);
            }
        }
        else if (IDataObject.Type == "weapon")
        {
            List<WeaponData> WeaponDataList = WeaponStatus.instance.GetStatusList();
            Weapon weapon = WeaponDataBase.FindWeaponFromName(IDataObject.Name);
            if (!WeaponDataList.Any(i => i.weapon == weapon))
            {
                WeaponStatus.instance.AddWeaponDataList(weapon);
            }
            else
            {
                WeaponStatus.instance.UpdateWeaponPhase(weapon);
            }
        }
    }

    private IDataObject? GetSlotItem(int index)
    {
        List<IData> PowerUpDataList = new List<IData>();

        List<ItemData> ItemDataList = ItemStatus.instance.GetStatusList();
        List<WeaponData> WeaponDataList = WeaponStatus.instance.GetStatusList();

        List<ItemData> CanPowerUpItemList = ItemDataList.FindAll(i => i.item.GetPlayCount() != i.phase);
        List<WeaponData> CanPowerUpWeaponList = WeaponDataList.FindAll(i => i.weapon.GetPlayCount() != i.phase);
        PowerUpDataList.AddRange(CanPowerUpItemList.Cast<IData>().ToList());
        PowerUpDataList.AddRange(CanPowerUpWeaponList.Cast<IData>().ToList());

        List<ItemData> MaxPowerUpItemList = ItemDataList.FindAll(i => i.item.GetPlayCount() == i.phase);
        List<WeaponData> MaxPowerUpWeaponList = WeaponDataList.FindAll(i => i.weapon.GetPlayCount() == i.phase);

        int ItemMax = CanUseItemList.Count >= 6 ? 6 : CanUseItemList.Count;
        int WeaponMax = CanUseWeaponList.Count >= 6 ? 6 : CanUseWeaponList.Count;
        int RemainSlotCount = ItemMax + WeaponMax - (MaxPowerUpItemList.Count + MaxPowerUpWeaponList.Count);

        if (RemainSlotCount == 0) {
            if (index >= 3) return null;
            GameObject Object = GetLvUPSlotObject(index);
            Object.SetActive(true);

            if (index == 1) return CreateIDataObject(SpecialItemId3, "special", "エメラルド原石", "", "エメラルドの合計に25個追加。");
            if (index == 2) return CreateIDataObject(SpecialItemId9, "special", "ステーキ", "", "ライフを30回復する。");
        }

        if (RemainSlotCount < index) return null;

        if (3 >= RemainSlotCount || ItemDataList.Count >= ItemMax && WeaponDataList.Count >= WeaponMax) return CheckIDataType(PowerUpDataList);

        int LvPhase = PlayerData.Lv % 2 == 0 ? 2 : 1;
        int Luck = ItemStatus.instance.GetAllStatusPhase(12) + 1;
        Luck = (1 / Luck) * 100;
        float ChanceOwnItem = 100 - Luck + 30 * LvPhase;

        if (ExpStatus.instance.Probability(ChanceOwnItem)) return CheckIDataType(PowerUpDataList);

        return NewItemData();
    }

    private IDataObject NewItemData()
    {
        IDataObject NewIDataObject;

        while (true)
        {
            int index = Random.Range(0, CanUseWeaponList.Count + CanUseItemList.Count);
            if (index < CanUseWeaponList.Count)
            {
                Weapon weapon = WeaponDataBase.FindWeaponFromId(CanUseWeaponList[index].id);
                if (WeaponStatus.instance.GetStatusList().Any(w => w.weapon == weapon)) continue;

                NewIDataObject = CreateIDataObject(weapon.GetIcon(), "weapon", weapon.GetName(), "NEW!", weapon.GetDescription());
            }
            else
            {
                Item item = ItemDataBase.FindItemFromId(CanUseItemList[index - CanUseWeaponList.Count].id);
                if (ItemStatus.instance.GetStatusList().Any(w => w.item == item)) continue;

                NewIDataObject = CreateIDataObject(item.GetIcon(), "item", item.GetName(), "NEW!", item.GetDescription());
            }

            if (IDataObjectList.Any(d => d.Name == NewIDataObject.Name && d.Type == NewIDataObject.Type)) continue;
            return NewIDataObject;
        }
    }

    private IDataObject CheckIDataType(List<IData> PowerUpDataList)
    {
        if (PowerUpDataList.Count == 0) return NewItemData();

        IDataObject NewIDataObject;

        List<IDataObject> CheckIDataObjectList = new List<IDataObject>();

        while (true)
        {
            IData GetRandomIData = GetRandom(PowerUpDataList);
            if (GetRandomIData.type == "item")
            {
                ItemData ItemData = (ItemData) GetRandomIData;
                Item Item = ItemData.item;
                NewIDataObject = CreateIDataObject(Item.GetIcon(), GetRandomIData.type, Item.GetName(), $"レベル：{ItemData.phase + 1}", Item.GetDescription());
            }
            else
            {
                WeaponData WeaponData = (WeaponData) GetRandomIData;
                Weapon Weapon = WeaponData.weapon;
                NewIDataObject = CreateIDataObject(Weapon.GetIcon(), GetRandomIData.type, Weapon.GetName(), $"レベル：{WeaponData.phase + 1}", Weapon.GetDescription());
            }

            CheckIDataObjectList.Add(NewIDataObject);
            if (IDataObjectList.Any(d => d.Name == NewIDataObject.Name && d.Type == NewIDataObject.Type)) {
                if (CheckIDataObjectList.Count != PowerUpDataList.Count) continue;

                return NewItemData();
            };
            return NewIDataObject;
        }
    }

    private IDataObject CreateIDataObject(Sprite Image, string Type, string Name, string Phase, string Description)
    {
        return new IDataObject
        {
            Image = Image,
            Type = Type,
            Name = Name,
            Phase = Phase,
            Description = Description
        };
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}