using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataBase", menuName = "CreateMapDataBase")]
public class MapDataBase : ScriptableObject
{
    [SerializeField]
    private List<Map> MapLists = new List<Map>();

    public List<Map> GetMapLists()
    {
        return MapLists;
    }

    public Map FindMapFromId(int id)
    {
        return MapLists.Find(mob => mob.GetId() == id);
    }

    public Map FindMapFromName(string name)
    {
        return MapLists.Find(mob => mob.GetName() == name);
    }
}