using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataBase", menuName = "CreateWeaponDataBase")]//  CreateからCreateWeaponというメニューを表示し、Weaponを作成する
public class WeaponDataBase : ScriptableObject
{

    [SerializeField]
    private List<Weapon> WeaponLists = new List<Weapon>();//  Weaponのリストを新しく生成する

    public List<Weapon> GetWeaponLists()//  Weaponのリストがあったら、
    {
        return WeaponLists;//  WeaponListsに返す
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