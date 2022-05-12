using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataBase", menuName = "CreateWeaponDataBase")]//  Create����CreateWeapon�Ƃ������j���[��\�����AWeapon���쐬����
public class WeaponDataBase : ScriptableObject
{

    [SerializeField]
    private List<Weapon> WeaponLists = new List<Weapon>();//  Weapon�̃��X�g��V������������

    public List<Weapon> GetWeaponLists()//  Weapon�̃��X�g����������A
    {
        return WeaponLists;//  WeaponLists�ɕԂ�
    }

    public Weapon FindWeaponFromId(int id)
    {
        return WeaponLists.Find(Weapon => Weapon.GetId() == id);
    }

    public Weapon FindWeaponFromName(string name)
    {
        return WeaponLists.Find(Weapon => Weapon.GetName() == name);
    }
}