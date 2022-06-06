using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataBase", menuName = "CreateMapDataBase")]//  CreateからCreateMapというメニューを表示し、Mapを作成する
public class MapDataBase : ScriptableObject
{

    [SerializeField]
    private List<Map> MapLists = new List<Map>();//  Mapのリストを新しく生成する

    public List<Map> GetMapLists()//  Mapのリストがあったら、
    {
        return MapLists;//  MapListsに返す
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