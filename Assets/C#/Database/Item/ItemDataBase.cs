using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]//  CreateからCreateItemというメニューを表示し、Itemを作成する
public class ItemDataBase : ScriptableObject
{

    [SerializeField]
    private List<Item> ItemLists = new List<Item>();//  Itemのリストを新しく生成する

    public List<Item> GetItemLists()//  Itemのリストがあったら、
    {
        return ItemLists;//  ItemListsに返す
    }

    public Item FindItemFromId(int id)
    {
        return ItemLists.Find(Item => Item.GetId() == id);
    }

    public Item FindItemFromName(string name)
    {
        return ItemLists.Find(Item => Item.GetName() == name);
    }
}