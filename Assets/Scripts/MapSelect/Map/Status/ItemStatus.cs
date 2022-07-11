using UnityEngine;
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

    private Json.PlayerData player = Json.instance.Load();
    private MobDataBase MobDataBase = Json.instance.MobDataBase;
    private ItemDataBase ItemDataBase = Json.instance.ItemDataBase;
    private List<ItemData> ItemDataList = new List<ItemData>();

    private Mob mob;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mob = MobDataBase.FindMobFromId(player.Latest_Chara);
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
}
