using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataBase", menuName = "CreateMapDataBase")]//  Create����CreateMap�Ƃ������j���[��\�����AMap���쐬����
public class MapDataBase : ScriptableObject
{

    [SerializeField]
    private List<Map> MapLists = new List<Map>();//  Map�̃��X�g��V������������

    public List<Map> GetMapLists()//  Map�̃��X�g����������A
    {
        return MapLists;//  MapLists�ɕԂ�
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