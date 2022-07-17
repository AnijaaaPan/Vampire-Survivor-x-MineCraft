using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataBase", menuName = "CreateWeaponDataBase")]
public class WeaponDataBase : ScriptableObject
{
    [SerializeField]
    private List<Weapon> WeaponLists = new List<Weapon>();

    public List<Weapon> GetWeaponLists()
    {
        return WeaponLists;
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