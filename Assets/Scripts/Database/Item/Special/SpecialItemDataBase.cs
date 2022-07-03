using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItemDataBase", menuName = "CreateSpecialItemDataBase")]
public class SpecialItemDataBase : ScriptableObject
{
    [SerializeField]
    private List<SpecialItem> SpecialItemLists = new List<SpecialItem>();

    public List<SpecialItem> GetSpecialItemLists()
    {
        return SpecialItemLists;
    }

    public SpecialItem FindSpecialItemFromId(int id)
    {
        return SpecialItemLists.Find(SpecialItem => SpecialItem.GetId() == id);
    }
}