using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemData
{
    public int id;
    public Item item;
    public int phase;
}

public class ItemStatus : MonoBehaviour
{
    static public ItemStatus instance;

    public GameObject ItemBar;
    public GameObject InitItemObject;

    public GameObject OptionItemBar;
    public GameObject OptionInitItemObject;
    public GameObject OptionslotObject;

    public GameObject EffectList;

    public Sprite PowerUp;
    public Sprite UnPowerUp;

    private readonly static float ParameterInitInt = 1 / 1.425f;
    private readonly static List<int> ParameterList = new List<int> { 3, 4, 2, 10, 1, 7, 8, 6, 5, 9, 16, 11, 12, 13, 14, 15 };
    private Font TextFont;
    private Mob mob;

    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MobDataBase MobDataBase = Json.instance.MobDataBase;
    private readonly ItemDataBase ItemDataBase = Json.instance.ItemDataBase;
    private List<ItemData> ItemDataList = new List<ItemData>();

    private void Awake()
    {
        instance = this;
        mob = MobDataBase.FindMobFromId(player.Latest_Chara);
    }

    void Start()
    {
        TextFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        for (int i = 0; i < ItemDataBase.GetItemLists().Count; i++)
        {
            Item ItemData = ItemDataBase.FindItemFromId(ParameterList[i]);

            GameObject ParameterObject = CreateParameterObject(ItemData);
            GameObject ParameterImageObject = CreateParameterImageObject(ItemData);
            GameObject ParameterEffectNameObject = CreateParameterEffectNameObject(ItemData);
            GameObject ParameterEffectObject = CreateParameterEffectObject();

            ParameterImageObject.transform.SetParent(ParameterObject.transform);
            ParameterEffectNameObject.transform.SetParent(ParameterObject.transform);
            ParameterEffectObject.transform.SetParent(ParameterObject.transform);
            ParameterObject.transform.SetParent(EffectList.transform);
        };

        StartCoroutine("Interval");
    }

    IEnumerator Interval()
    {
        int index = 1;
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (IsPlaying.instance.isPlay())
            {
                Item item = ItemDataBase.FindItemFromId(index);
                if (!item) break;

                AddItemDataList(item);
                index++;
            };
        }
    }

    public List<ItemData> GetStatusList()
    {
        return ItemDataList;
    }

    public int GetStatusPhase(int id)
    {
        ItemData GetStatus = ItemDataList.Find(s => s.item.GetId() == id);
        return GetStatus == null ? 0 : GetStatus.phase;
    }

    public int GetAllStatusPhase(int id)
    {
        Item item = ItemDataBase.FindItemFromId(id);

        Parameter data = mob.GetParameter().Find(par => item.GetId() == par.item.GetId());
        Json.PowerUpList poweroup_data = player.PowerUp.poweruplist.Find(data => data.id == item.GetId());
        float ParameterPhase = data == null ? 0 : data.phase;

        return (int)ParameterPhase + item.GetCount() + GetStatusPhase(id) - poweroup_data.powerupcount;
    }

    public void AddItemDataList(Item item)
    {
        ItemData ItemData = new ItemData
        {
            id = ItemDataList.Count,
            phase = 1,
            item = item
        };

        ItemDataList.Add(ItemData);

        GameObject Object = GetItemObject(ItemData.id);
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = item.GetIcon();
        ObjectImage.color = new Color(1, 1, 1, 1);
    }

    public void UpdateItemPhase(int id)
    {

        ItemData ItemData = ItemDataList.Find(i => i.item.GetId() == id);
        ItemData.phase++;
    }

    private GameObject CreateItemObject(int id)
    {
        GameObject ItemObject = Instantiate(InitItemObject);
        ItemObject.name = $"item_{id}";
        ItemObject.SetActive(true);
        ItemObject.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);
        ItemObject.transform.SetParent(ItemBar.transform);
        return ItemObject;
    }

    private GameObject GetItemObject(int id)
    {
        if (ItemBar.transform.Find($"Item_{id}") == null)
        {
            GameObject ItemObject = CreateItemObject(id);
            return ItemObject.transform.Find("item").gameObject;
        }
        return ItemBar.transform.Find($"Item_{id}").transform.Find("item").gameObject;
    }

    // ここから先はアイテムの段階の処理

    public void UpdateOptionItemBar()
    {
        if (ItemDataList.Count == 0) return;

        for (int i = 0; i < ItemDataList.Count; i++)
        {
            ItemData ItemData = ItemDataList[i];

            if (OptionItemBar.transform.Find($"Item_{ItemData.id}") == null)
            {
                CreateBarItemObject(ItemData);

            } else
            {
                UpdateBarItemObject(ItemData);
            }
        }
    }

    private void CreateBarItemObject(ItemData ItemData)
    {
        GameObject Object = Instantiate(OptionInitItemObject);
        Object.name = $"Item_{ItemData.id}";
        Object.SetActive(true);
        Object.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);

        GameObject ItemObject = Object.transform.Find("Item").transform.Find("item").gameObject;

        Image ObjectImage = ItemObject.GetComponent<Image>();
        ObjectImage.sprite = ItemData.item.GetIcon();
        ObjectImage.color = new Color(1, 1, 1, 1);

        UpdateSlotObject(ItemData, Object);

        Object.transform.SetParent(OptionItemBar.transform);
    }

    private void UpdateBarItemObject(ItemData ItemData)
    {
        GameObject Object = OptionItemBar.transform.Find($"Item_{ItemData.id}").gameObject;
        UpdateSlotObject(ItemData, Object);
    }

    private void CreateSlotObject(GameObject SlotListObject, int i)
    {
        GameObject SlotObject = Instantiate(OptionslotObject);
        SlotObject.name = $"slot_{i}";
        SlotObject.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);

        SlotObject.transform.SetParent(SlotListObject.transform);
    }

    private void UpdateSlotObject(ItemData ItemData, GameObject Object)
    {
        GameObject SlotListObject = Object.transform.Find("Phase").transform.Find("SlotList").gameObject;
        for (int i = 1; i < ItemData.item.GetPlayCount(); i++)
        {
            if (SlotListObject.transform.Find($"slot_{i}") == null) CreateSlotObject(SlotListObject, i);
            
            GameObject SlotObject = SlotListObject.transform.Find($"slot_{i}").gameObject;
            Image SlotObjectImage = SlotObject.GetComponent<Image>();
            SlotObjectImage.sprite = i < ItemData.phase ? PowerUp : UnPowerUp;
        }
    }

    // ここから先はパラメータの処理

    public void UpdatePlayerEffect()
    {
        string ResultEffect(Item item_data)
        {
            string Coincidence(float result)
            {
                return result < 0 ? $"{result}" : $"+{result}";
            }

            int powerupcount = GetAllStatusPhase(item_data.GetId());
            if (powerupcount == 0)
                return item_data.GetId() == 3 ? "100" : " - ";
            if (new List<int> { 1, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 }.Contains(item_data.GetId()))
                return $"{Coincidence(powerupcount * 10)}%";
            if (new List<int> { 2, 9, 16 }.Contains(item_data.GetId()))
                return Coincidence(powerupcount);
            if (item_data.GetId() == 3)
                return $"{100 + powerupcount * 10}";
            return $"{powerupcount * 0.1}";
        }

        for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)
        {
            Item ItemData = ItemDataBase.FindItemFromId(i);
            GameObject ParameterObject = EffectList.transform.Find($"Parameter_{ItemData.GetId()}").gameObject;
            GameObject FindObject = ParameterObject.transform.Find("Effect").gameObject;

            string Effect = ResultEffect(ItemData);
            Text ObjectText = FindObject.GetComponent<Text>();
            ObjectText.text = Effect;
        }
    }

    private GameObject CreateParameterObject(Item ItemData)
    {
        GameObject Object = new GameObject($"Parameter_{ItemData.GetId()}");
        Object.transform.localScale = new Vector3(ParameterInitInt, ParameterInitInt, ParameterInitInt);

        Object.AddComponent<RectTransform>();
        return Object;
    }

    private GameObject CreateParameterImageObject(Item ItemData)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = new Vector3(ParameterInitInt, ParameterInitInt, ParameterInitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.5f, 0.5f);
        ObjectRectTransform.position = new Vector3(-1.425f, 0, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ItemData.GetIcon();
        return Object;
    }

    private GameObject CreateParameterEffectNameObject(Item ItemData)
    {
        GameObject Object = new GameObject("EffectName");
        Object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(790.6436f, 147.25f);
        ObjectRectTransform.position = new Vector3(-0.175f, 0, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = ItemData.GetName();
        ObjectText.font = TextFont;
        ObjectText.fontSize = 100;
        ObjectText.alignment = TextAnchor.MiddleLeft;
        return Object;
    }

    private GameObject CreateParameterEffectObject()
    {
        GameObject Object = new GameObject("Effect");
        Object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(318.66f, 147.25f);
        ObjectRectTransform.position = new Vector3(1.2225f, 0, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = "";
        ObjectText.font = TextFont;
        ObjectText.fontSize = 100;
        ObjectText.alignment = TextAnchor.MiddleLeft;
        return Object;
    }
}
