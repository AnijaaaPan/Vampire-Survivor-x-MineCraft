using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItemDataBase", menuName = "CreateSpecialItemDataBase")]//  Create����CreateSpecialItem�Ƃ������j���[��\�����ASpecialItem���쐬����
public class SpecialItemDataBase : ScriptableObject
{

    [SerializeField]
    private List<SpecialItem> SpecialItemLists = new List<SpecialItem>();//  SpecialItem�̃��X�g��V������������

    public List<SpecialItem> GetSpecialItemLists()//  SpecialItem�̃��X�g����������A
    {
        return SpecialItemLists;//  SpecialItemLists�ɕԂ�
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