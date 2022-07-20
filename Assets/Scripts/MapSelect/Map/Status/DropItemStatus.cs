using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropItemStatus : MonoBehaviour
{

    static public DropItemStatus instance;

    public GameObject Option;
    public GameObject WeaponItemBar;
    public GameObject Chara;
    public GameObject TresureBoxs;
    public GameObject TreasureSlot;
    public GameObject StageItems;
    public GameObject GetDropItemObject;
    public GameObject Slot_1;

    public Sprite Zone;
    public Sprite Emerald;
    public List<Sprite> ArrowImages;

    private Sprite[] ChestLv1;
    private Sprite[] ChestLv2;
    private Sprite[] ChestLv3;

    private List<GameObject> DropItemObjects = new List<GameObject>();

    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MapDataBase MapDataBase = Json.instance.MapDataBase;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChestLv1 = Resources.LoadAll<Sprite>($"Box/Chest/");
        ChestLv2 = Resources.LoadAll<Sprite>($"Box/ChristmasChest/");
        ChestLv3 = Resources.LoadAll<Sprite>($"Box/EnderChest/");

        Map Map = MapDataBase.FindMapFromId(player.Latest_Map);
        List<Stageitems> StageItems = Map.GetStageitems();
        for (int i = 0; i < StageItems.Count; i++)
        {
            Stageitems StageItem = StageItems[i];
            if (!ExpStatus.instance.Probability(StageItem.Chance)) continue;

            CreateDropItem(StageItem);
        }
    }

    public List<GameObject> GetDropItemObjects()
    {
        return DropItemObjects;
    }

    public void RemoveDropItemObjects(GameObject Object)
    {
        DropItemObjects.Remove(Object);
    }

    public void CreateTresure(GameObject EnemyObject, List<string> Treasure)
    {
        int TreasureLv = RandomSelectChest();
        Sprite[] GetChestImageList = GetChestImages(TreasureLv);

        RectTransform EnemyDataObjectRectTransform = EnemyObject.GetComponent<RectTransform>();
        float EnemyX = EnemyDataObjectRectTransform.anchoredPosition.x;
        float EnemyY = EnemyDataObjectRectTransform.anchoredPosition.y;

        GameObject Object = new GameObject("TresureBox");
        Object.transform.SetParent(TresureBoxs.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1, 1);
        ObjectRectTransform.anchoredPosition = new Vector3(EnemyX, EnemyY, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = GetChestImageList[0];
        ObjectImage.preserveAspect = true;

        BoxCollider2D ObjectBoxCollider2D = Object.AddComponent<BoxCollider2D>();
        ObjectBoxCollider2D.offset = new Vector2(0, -0.15f);
        ObjectBoxCollider2D.size = new Vector2(0.65f, 0.65f);

        GetDropTreasureBox ObjectGetDropTreasureBox = Object.AddComponent<GetDropTreasureBox>();
        ObjectGetDropTreasureBox.Emerald = Emerald;
        ObjectGetDropTreasureBox.ChestImages = GetChestImageList;
        ObjectGetDropTreasureBox.Treasure = Treasure;
        ObjectGetDropTreasureBox.TreasureLv = TreasureLv;
        ObjectGetDropTreasureBox.Option = Option;
        ObjectGetDropTreasureBox.TreasureSlot = TreasureSlot;
        ObjectGetDropTreasureBox.WeaponItemBar = WeaponItemBar;

        CreateArrow(Object);
        CreateArrowCharaToItem(Object);
    }

    private GameObject CreateDropItemZone(Stageitems StageItem)
    {
        GameObject Object = new GameObject("DropItemZone");
        Object.transform.position = new Vector3(StageItem.x, StageItem.y, 0);
        Object.transform.SetParent(StageItems.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1.5f, 1.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = Zone;
        ObjectImage.preserveAspect = true;

        Rainbow ObjectRainbow = Object.AddComponent<Rainbow>();
        ObjectRainbow.a = 0.5f;
        ObjectRainbow.light = 0.5f;

        CircleCollider2D ObjectCircleCollider2D = Object.AddComponent<CircleCollider2D>();
        ObjectCircleCollider2D.radius = 0.4f;

        GetDropItem ObjectGetDropItem = Object.AddComponent<GetDropItem>();
        ObjectGetDropItem.item = StageItem.item;
        ObjectGetDropItem.Option = Option;
        ObjectGetDropItem.GetDropItemObject = GetDropItemObject;
        ObjectGetDropItem.WeaponItemBar = WeaponItemBar;
        ObjectGetDropItem.Slot_1 = Slot_1;
  
        return Object;
    }

    private void CreateDropItem(Stageitems StageItem)
    {
        GameObject ZoneObject = CreateDropItemZone(StageItem);

        GameObject Object = new GameObject($"DropItem_{StageItem.item.GetId()}");
        Object.transform.SetParent(ZoneObject.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        ObjectRectTransform.sizeDelta = new Vector2(0.65f, 0.65f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = StageItem.item.GetIcon();
        ObjectImage.preserveAspect = true;

        UpDown ObjectUpDown = Object.AddComponent<UpDown>();
        ObjectUpDown.ObjectRectTransform = ObjectRectTransform;

        DropItemObjects.Add(Object);
        CreateArrowCharaToItem(Object, ObjectImage.sprite);
    }

    private void CreateArrow(GameObject TresureBoxObject)
    {
        GameObject Object = new GameObject("ArrowToTresureBox");
        Object.transform.SetParent(TresureBoxObject.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.75f, 0.75f);
        ObjectRectTransform.Rotate(0, 0, -90);
        ObjectRectTransform.anchoredPosition = new Vector3(0, 0.65f, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ArrowImages[0];
        ObjectImage.preserveAspect = true;

        RotateArrow ObjectRotateArrow = Object.AddComponent<RotateArrow>();
        ObjectRotateArrow.ArrowImages = ArrowImages;
    }

    private void CreateArrowCharaToItem(GameObject TresureBoxObject, Sprite Image = null)
    {
        GameObject Object = new GameObject("ArrowCharaToItem");
        Object.transform.SetParent(TresureBoxObject.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.75f, 0.75f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ArrowImages[0];
        ObjectImage.preserveAspect = true;

        RotateArrow ObjectRotateArrow = Object.AddComponent<RotateArrow>();
        ObjectRotateArrow.ArrowImages = ArrowImages;

        RotateToDropItem ObjectRotateToDropItem = Object.AddComponent<RotateToDropItem>();
        ObjectRotateToDropItem.Chara = Chara;
        ObjectRotateToDropItem.DropItem = TresureBoxObject;
        if (Image != null)
        {
            ObjectRotateToDropItem.ItemImage = Image;
        }
    }

    private int RandomSelectChest()
    {
        int index = Random.Range(0, 100);

        if (index < 3) return 3;
        if (index < 20) return 2;
        return 1;
    }

    private Sprite[] GetChestImages(int TreasureLv)
    {
        if (TreasureLv == 1) return ChestLv1;
        if (TreasureLv == 2) return ChestLv2;
        return ChestLv3;
    }
}
