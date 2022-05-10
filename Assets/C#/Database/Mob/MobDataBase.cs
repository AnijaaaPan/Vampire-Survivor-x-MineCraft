using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobDataBase", menuName = "CreateMobDataBase")]//  CreateからCreateMobというメニューを表示し、Mobを作成する
public class MobDataBase : ScriptableObject
{

    [SerializeField]
    private List<Mob> MobLists = new List<Mob>();//  Mobのリストを新しく生成する

    public List<Mob> GetMobLists()//  Mobのリストがあったら、
    {
        return MobLists;//  MobListsに返す
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