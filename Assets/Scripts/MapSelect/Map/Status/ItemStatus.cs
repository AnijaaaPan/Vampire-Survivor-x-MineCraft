using UnityEngine;
using UnityEngine.UI;
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

    private Json.PlayerData player = Json.instance.Load();
    private MobDataBase MobDataBase = Json.instance.MobDataBase;
    private ItemDataBase ItemDataBase = Json.instance.ItemDataBase;
    private List<ItemData> ItemDataList = new List<ItemData>();

    private Mob mob;

    private void Awake()
    {
        instance = this;
        mob = MobDataBase.FindMobFromId(player.Latest_Chara);
    }

    void Start()
    {
    }

    public List<ItemData> GetStatusList()
    {
        return ItemDataList;
    }

    public int GetStatusPhase(int id)
    {
        ItemData GetStatus = ItemDataList.Find(s => s.id == id);
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
        ItemData ItemData = new ItemData();
        ItemData.id = ItemDataList.Count;
        ItemData.phase = 1;
        ItemData.item = item;

        ItemDataList.Add(ItemData);

        GameObject Object = GetItemObject(ItemData.id);
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = item.GetIcon();
        ObjectImage.color = new Color(1, 1, 1, 1);
    }

    public void UpdateItemPhase(int id)
    {

        ItemData ItemData = ItemDataList.Find(i => i.id == id);
        ItemData.phase++;
    }

    private GameObject CreateItemObject(int id)
    {
        GameObject ItemObject = Instantiate(InitItemObject);
        ItemObject.name = $"item_{id}";
        ItemObject.SetActive(true);
        ItemObject.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);
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
}
