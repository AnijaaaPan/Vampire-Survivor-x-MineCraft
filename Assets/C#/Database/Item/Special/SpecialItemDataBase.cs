using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItemDataBase", menuName = "CreateSpecialItemDataBase")]//  CreateからCreateSpecialItemというメニューを表示し、SpecialItemを作成する
public class SpecialItemDataBase : ScriptableObject
{

    [SerializeField]
    private List<SpecialItem> SpecialItemLists = new List<SpecialItem>();//  SpecialItemのリストを新しく生成する

    public List<SpecialItem> GetSpecialItemLists()//  SpecialItemのリストがあったら、
    {
        return SpecialItemLists;//  SpecialItemListsに返す
    }

    public SpecialItem FindSpecialItemFromId(int id)
    {
        return SpecialItemLists.Find(SpecialItem => SpecialItem.GetId() == id);
    }

    public SpecialItem FindSpecialItemFromName(string name)
    {
        return SpecialItemLists.Find(SpecialItem => SpecialItem.GetName() == name);
    }
}