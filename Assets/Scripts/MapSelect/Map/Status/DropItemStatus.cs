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

    public Sprite Emerald;
    public List<Sprite> ArrowImages;

    private Sprite[] ChestLv1;
    private Sprite[] ChestLv2;
    private Sprite[] ChestLv3;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChestLv1 = Resources.LoadAll<Sprite>($"Box/Chest/");
        ChestLv2 = Resources.LoadAll<Sprite>($"Box/ChristmasChest/");
        ChestLv3 = Resources.LoadAll<Sprite>($"Box/EnderChest/");
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

    public void CreateArrow(GameObject TresureBoxObject)
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

    public void CreateArrowCharaToItem(GameObject TresureBoxObject)
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
