using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField]
    private List<Item> ItemLists = new List<Item>();

    public List<Item> GetItemLists()
    {
        return ItemLists;
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