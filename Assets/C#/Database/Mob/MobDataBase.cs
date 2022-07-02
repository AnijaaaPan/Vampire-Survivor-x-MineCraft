using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobDataBase", menuName = "CreateMobDataBase")]
public class MobDataBase : ScriptableObject
{
    [SerializeField]
    private List<Mob> MobLists = new List<Mob>();

    public List<Mob> GetMobLists()
    {
        return MobLists;
    }

    public Mob FindMobFromId(int id)
    {
        return MobLists.Find(mob => mob.GetId() == id);
    }

    public Mob FindMobFromName(string name)
    {
        return MobLists.Find(mob => mob.GetName() == name);
    }
}