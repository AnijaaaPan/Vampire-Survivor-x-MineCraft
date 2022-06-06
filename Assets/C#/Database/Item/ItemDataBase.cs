using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]//  Create����CreateItem�Ƃ������j���[��\�����AItem���쐬����
public class ItemDataBase : ScriptableObject
{

    [SerializeField]
    private List<Item> ItemLists = new List<Item>();//  Item�̃��X�g��V������������

    public List<Item> GetItemLists()//  Item�̃��X�g����������A
    {
        return ItemLists;//  ItemLists�ɕԂ�
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