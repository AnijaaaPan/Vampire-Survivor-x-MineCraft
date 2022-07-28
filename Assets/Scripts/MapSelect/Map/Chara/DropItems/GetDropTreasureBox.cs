using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class GetDropTreasureBox : MonoBehaviour
{
    public Sprite Emerald;
    public Sprite[] ChestImages;
    public List<string> Treasure;
    public int TreasureLv;

    public GameObject Option;
    public GameObject TreasureSlot;
    public GameObject WeaponItemBar;

    private GameObject Slots;
    private GameObject EffectBackGround;
    private Image BoxImage;
    private Image OptionImage;
    private Button DoneButton;

    private GameObject Slot1;
    private GameObject Slot2;
    private GameObject Slot3;
    private GameObject Slot4;
    private GameObject Slot5;

    private bool isOpen = false;
    private bool isChestOpen = false;

    private int ImageIndex = 0;
    private int CountChestImages;
    private int EmeraldCount = 0;

    private readonly SpecialItemDataBase SpecialItemDataBase = Json.instance.SpecialItemDataBase;

    void Start()
    {
        Slots = TreasureSlot.transform.Find("Viewport").transform.Find("Slots").gameObject;
        Slot1 = GetSlotObject(1);
        Slot2 = GetSlotObject(2);
        Slot3 = GetSlotObject(3);
        Slot4 = GetSlotObject(4);
        Slot5 = GetSlotObject(5);

        BoxImage = TreasureSlot.transform.Find("BoxImage").GetComponent<Image>();
        CountChestImages = ChestImages.Length;
    }

    void Update()
    {
        if (!isOpen || isChestOpen) return;

        BoxImage.sprite = ChestImages[ImageIndex];
        ImageIndex++;
        if (ImageIndex >= CountChestImages)
        {
            isChestOpen = true;

            ChangeSlotColor();
            OpenTreasureSlot();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("CharaImage")) return;

        IsPlaying.instance.Stop();
        Setting.instance.StopButtonCanvasAdd();
        int SpecialItemCcount = SpecialItemDataBase.GetSpecialItemLists().Count;
        DropItemStatus.instance.DropItemCountUp(SpecialItemCcount + TreasureLv, ChestImages[0]);

        isOpen = true;
        Option.SetActive(true);
        TreasureSlot.SetActive(true);
        WeaponItemBar.SetActive(false);

        OptionImage = Option.GetComponent<Image>();
        OptionImage.color = new Color(0, 0, 0, 0);

        EffectBackGround = Option.transform.Find("effect_background").gameObject;
        EffectBackGround.SetActive(false);

        DoneButton = TreasureSlot.transform.Find("DoneButton").GetComponent<Button>();
        DoneButton.onClick.RemoveAllListeners();
        DoneButton.onClick.AddListener(() =>
        {
            Setting.instance.StopButtonCanvasRemove();
            IsPlaying.instance.reStart();

            PlayerStatus.instance.UpdateEmeraldCount(EmeraldCount);

            EffectBackGround.SetActive(true);
            OptionImage.color = new Color(0, 0, 0, 0.75f);

            WeaponItemBar.SetActive(true);
            TreasureSlot.SetActive(false);
            Option.SetActive(false);

            Slot1.SetActive(false);
            Slot2.SetActive(false);
            Slot3.SetActive(false);
            Slot4.SetActive(false);
            Slot5.SetActive(false);
            isOpen = false;

            Destroy(gameObject);
        });
    }

    private void ChangeSlotColor()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        Slot4.SetActive(false);
        Slot5.SetActive(false);

        if (TreasureLv >= 1)
        {
            Slot1.SetActive(true);
            Slot1.GetComponent<Image>().color = new Color(0.2019847f, 0.2149098f, 0.8396226f, 0.65f);
        }

        if (TreasureLv >= 2)
        {
            Slot1.GetComponent<Image>().color = new Color(0.509804f, 0.1254902f, 0.9098039f, 0.65f);

            Slot2.SetActive(true);
            Slot3.SetActive(true);
            Slot2.GetComponent<Image>().color = new Color(0.6941177f, 0.2156863f, 0.8039216f, 0.65f);
            Slot3.GetComponent<Image>().color = new Color(0.6941177f, 0.2156863f, 0.8039216f, 0.65f);
        }

        if (TreasureLv >= 3)
        {
            Slot1.GetComponent<Image>().color = new Color(0.8509804f, 0.3764706f, 0.4980392f, 0.65f);
            Slot2.GetComponent<Image>().color = new Color(0.8784314f, 0.6705883f, 0.5058824f, 0.65f);
            Slot3.GetComponent<Image>().color = new Color(0.8784314f, 0.6705883f, 0.5058824f, 0.65f);

            Slot4.SetActive(true);
            Slot5.SetActive(true);
            Slot4.GetComponent<Image>().color = new Color(0.9490196f, 0.9568627f, 0.2509804f, 0.65f);
            Slot5.GetComponent<Image>().color = new Color(0.9490196f, 0.9568627f, 0.2509804f, 0.65f);
        }
    }

    private GameObject GetSlotObject(int index)
    {
        return Slots.transform.Find($"Slot_{index}").gameObject;
    }

    private void OpenTreasureSlot()
    {
        for (int i = 1; i <= 5; i++)
        {
            Sprite Image = LotterySlot(i);
            Image ObjectImage = GetSlotObject(i).transform.Find("BackGround").transform.Find("ItemImage").GetComponent<Image>();
            ObjectImage.sprite = Image;

            EmeraldCount += Random.Range(40, 60);

            if (TreasureLv == 1 && i == 1) break;
            if (TreasureLv == 2 && i == 3) break;
        }

        Text ObjectText = TreasureSlot.transform.Find("Text").GetComponent<Text>();
        ObjectText.text = $"{EmeraldCount}.00";
    }

    private Sprite LotterySlot(int index)
    {
        string type = Treasure[index - 1];
        if (type == "Evolution") return EvolutionWeapon();
        if (type == "UpgradeWeapon") return UpgradeWeapon();
        return UpgradeAny();
    }

    private Sprite EvolutionWeapon()
    {
        List<ItemData> ItemDataList = ItemStatus.instance.GetStatusList();
        List<WeaponData> WeaponDataList = WeaponStatus.instance.GetStatusList();

        List<ItemData> MaxPowerUpItemList = ItemDataList.FindAll(i => i.item.GetPlayCount() == i.phase);
        WeaponData EvolutionWeaponData = WeaponDataList.Find(w => w.weapon.GetPlayCount() == w.phase && MaxPowerUpItemList.Any(i => i.item.GetPowerup().Any(iw => iw == w.weapon)));
        if (EvolutionWeaponData == null) return UpgradeAny();

        Sprite Image = WeaponStatus.instance.EvolutionWeapon(EvolutionWeaponData.weapon);
        WeaponStatus.instance.UpdateOptionWeaponBar();
        return Image;
    }

    private Sprite UpgradeWeapon()
    {
        List<WeaponData> CanPowerUpWeaponList = WeaponStatus.instance.GetStatusList().FindAll(i => i.weapon.GetPlayCount() != i.phase);
        if (CanPowerUpWeaponList.Count == 0) return UpgradeAny();

        WeaponData WeaponData = GetRandom(CanPowerUpWeaponList);
        WeaponStatus.instance.UpdateWeaponPhase(WeaponData.weapon);
        return WeaponData.weapon.GetIcon();
    }

    private Sprite UpgradeAny()
    {
        List<IData> PowerUpDataList = new List<IData>();

        List<WeaponData> CanPowerUpWeaponList = WeaponStatus.instance.GetStatusList().FindAll(i => i.weapon.GetPlayCount() != i.phase);
        List<ItemData> CanPowerUpItemList = ItemStatus.instance.GetStatusList().FindAll(i => i.item.GetPlayCount() != i.phase);

        PowerUpDataList.AddRange(CanPowerUpItemList.Cast<IData>().ToList());
        PowerUpDataList.AddRange(CanPowerUpWeaponList.Cast<IData>().ToList());
        if (PowerUpDataList.Count == 0)
        {
            PlayerStatus.instance.UpdateEmeraldCount(25);
            return Emerald;
        };

        IData GetRandomIData = GetRandom(PowerUpDataList);
        if (GetRandomIData.type == "item")
        {
            ItemData ItemData = (ItemData)GetRandomIData;
            ItemStatus.instance.UpdateItemPhase(ItemData.item);
            return ItemData.item.GetIcon();

        }
        else
        {
            WeaponData WeaponData = (WeaponData)GetRandomIData;
            WeaponStatus.instance.UpdateWeaponPhase(WeaponData.weapon);
            return WeaponData.weapon.GetIcon();
        }
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
