using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DropItemCountData
{
    public Sprite sprite;
    public int id;
    public int Count = 0;
}

public class DropItemCount
{
    public int LightSource = 0;
    public List<DropItemCountData> DropItemCountData = new List<DropItemCountData>();
}

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
    public GameObject LightSourceObject;
    public GameObject Slot_1;

    public GameObject SpecialItem;

    public Sprite Zone;
    public Sprite Emerald;
    public Sprite LightSourceImage;
    public List<Sprite> ArrowImages;

    private Sprite[] ChestLv1;
    private Sprite[] ChestLv2;
    private Sprite[] ChestLv3;

    private DropItemCount DropItemCount = new DropItemCount(){};

    private List<GameObject> DropItemObjects = new List<GameObject>();
    private List<GameObject> LightSourceList = new List<GameObject>();

    private Map Map;

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

        Map = MapDataBase.FindMapFromId(player.Latest_Map);
        List<Stageitems> StageItems = Map.GetStageitems();
        for (int i = 0; i < StageItems.Count; i++)
        {
            Stageitems StageItem = StageItems[i];
            if (!ExpStatus.instance.Probability(StageItem.Chance)) continue;

            CreateDropItem(StageItem);
        }

        StartCoroutine(nameof(LightSourceSpawnInterval));
    }

    public List<GameObject> GetDropItemObjects()
    {
        return DropItemObjects;
    }

    public DropItemCount GetDropItemCount()
    {
        return DropItemCount;
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

    // 光源の処理

    private IEnumerator LightSourceSpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (ExpStatus.instance.Probability(Map.GetLightSourceChance()))
            {
                CreateLightSource();
                LightSourceLimit();
            };
        }
    }

    private void LightSourceLimit()
    {
        int DeleteCount = LightSourceList.Count - 10;
        for (int i = 0; i < DeleteCount; i++)
        {
            GameObject Object = LightSourceList[i];
            LightSourceList.Remove(Object);
            Destroy(Object);
        }
    }

    private void CreateLightSource()
    {
        SpawnRange GetSpawn = SpawnEnemy.instance.GetRandomSpawnRange();
        float SpawnX = Chara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float SpawnY = Chara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);

        GameObject Object = new GameObject("LightSource");

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1f, 1f);
        ObjectRectTransform.position = new Vector3(SpawnX, SpawnY, 0);

        Image ImageObject = Object.AddComponent<Image>();
        ImageObject.sprite = LightSourceImage;
        ImageObject.preserveAspect = true;

        Rigidbody2D Rigidbody2DObject = Object.AddComponent<Rigidbody2D>();
        Rigidbody2DObject.gravityScale = 0;

        CircleCollider2D CircleCollider2DObject = Object.AddComponent<CircleCollider2D>();
        CircleCollider2DObject.radius = 0.5f;
        CircleCollider2DObject.isTrigger = true;

        LightSource ObjectLightSource = Object.AddComponent<LightSource>();
        ObjectLightSource.Chara = Chara;
        ObjectLightSource.LightSourceObject = LightSourceObject;

        Object.transform.SetParent(LightSourceObject.transform);

        LightSourceList.Add(Object);
    }

    public void DropItemCountUp(int id, Sprite sprite)
    {
        DropItemCountData DropItemCountData = DropItemCount.DropItemCountData.Find(i => i.id == id);
        if (DropItemCountData == null)
        {
            DropItemCountData NewDropItemCountData = new DropItemCountData
            {
                id = id,
                sprite = sprite,
                Count = 1
            };
            DropItemCount.DropItemCountData.Add(NewDropItemCountData);

        } else
        {
            DropItemCountData.Count++;
        }
    }

    public void RemoveLightSourceData(GameObject Object)
    {
        DropItemCount.LightSource++;
        LightSourceList.Remove(Object);
        Destroy(Object);
    }

    // 光源から落ちたアイテムの処理

    public void GetLightSourceItem(GameObject Object, SpecialItem SpecialItem)
    {
        DropItemCountUp(SpecialItem.GetId(), SpecialItem.GetIcon());

        int id = SpecialItem.GetId();
        if (id == 1) GetSpecialItem1();
        if (id == 2 || id == 3 || id == 4) GetSpecialItem234(id);
        if (id == 5) GetSpecialItem5();
        if (id == 6) GetSpecialItem6();
        if (id == 7) GetSpecialItem7();
        if (id == 8) GetSpecialItem8();
        if (id == 9) GetSpecialItem9();
        if (id == 10) GetSpecialItem10();
        if (id == 11) GetSpecialItem11();

        Destroy(Object);
    }

    private GameObject SpecialItemObject(int id)
    {
        return SpecialItem.transform.Find($"ItemId_{id}").gameObject;
    }

    private Image SpecialItemObjectImage(GameObject GetSpecialItemObject, string name)
    {
        GameObject Object = GetSpecialItemObject.transform.Find(name).gameObject;
        return Object.GetComponent<Image>();
    }

    private void GetSpecialItem1()
    {
        PlayerStatus.instance.UpdateExpStatus(1);
    }

    private void GetSpecialItem234(int id)
    {
        int EmeraldCount = id == 2 ? 1 : id == 3 ? 10 : 100;
        PlayerStatus.instance.UpdateEmeraldCount(EmeraldCount);
    }

    public void GetSpecialItem5() // コマンドブロックを拾った処理
    {
        StartCoroutine(IntervalSpecialItem5());

        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;

        List<EnemyData> EnemyDataList = EnemyStatus.instance.GetEnemyDataList().FindAll(e => {
            Vector3 p = e.Object.transform.position;
            return CameraLeftX <= p.x && p.x <= CameraRightX && CameraBottomY <= p.y && p.y <= CameraTopY;
        });

        for (int i = 0; i < EnemyDataList.Count; i++)
        {
            EnemyData EnemyData = EnemyDataList[i];
            int value = Random.Range(500, 750);
            EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, value);
        }
    }

    private IEnumerator IntervalSpecialItem5()
    {
        float IntervalTime = 0.075f;
        Sprite[] Images = Resources.LoadAll<Sprite>($"SpecialItem/id_5/");

        GameObject GetSpecialItemObject = SpecialItemObject(5);
        Image BackGroundImage = SpecialItemObjectImage(GetSpecialItemObject, "BackGround");
        Image ItemintervalImage = SpecialItemObjectImage(GetSpecialItemObject, "ItemintervalImage");

        GetSpecialItemObject.SetActive(true);

        BackGroundImage.color = new Color(1, 1, 1, 0);
        ItemintervalImage.sprite = Images[0];
        yield return new WaitForSeconds(IntervalTime);

        BackGroundImage.color = new Color(1, 1, 1, 0.25f);
        ItemintervalImage.sprite = Images[1];
        yield return new WaitForSeconds(IntervalTime);

        BackGroundImage.color = new Color(1, 1, 1, 0.5f);
        ItemintervalImage.sprite = Images[2];
        yield return new WaitForSeconds(IntervalTime);

        ItemintervalImage.sprite = Images[3];
        yield return new WaitForSeconds(IntervalTime);

        ItemintervalImage.sprite = Images[4];
        yield return new WaitForSeconds(IntervalTime);

        BackGroundImage.color = new Color(1, 1, 1, 0);
        GetSpecialItemObject.SetActive(false);
        yield break;
    }

    private void GetSpecialItem6() // ブレイズ・パウダーを拾った処理
    {
        PlayerStatus.instance.SetFireFritta();
    }

    private void GetSpecialItem7() // 懐中時計を拾った処理
    {
        GameObject GetSpecialItemObject = SpecialItemObject(7);
        GetSpecialItemObject.SetActive(true);
        PlayerStatus.instance.SetTheWorld();
    }

    private void GetSpecialItem8()
    {
        PlayerStatus.instance.SetExpVacuum();
    }

    private void GetSpecialItem9()
    {
        PlayerStatus.instance.UpdateHpStatus(30);
    }

    private void GetSpecialItem10()
    {
        // マジでフィーバーって何？無理
    }

    private void GetSpecialItem11()
    {
        // 時間が残ってたら気分で作るかも
    }
}
