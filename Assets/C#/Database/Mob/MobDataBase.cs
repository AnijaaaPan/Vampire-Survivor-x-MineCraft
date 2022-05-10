using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobDataBase", menuName = "CreateMobDataBase")]//  Create����CreateMob�Ƃ������j���[��\�����AMob���쐬����
public class MobDataBase : ScriptableObject
{

    [SerializeField]
    private List<Mob> MobLists = new List<Mob>();//  Mob�̃��X�g��V������������

    public List<Mob> GetMobLists()//  Mob�̃��X�g����������A
    {
        return MobLists;//  MobLists�ɕԂ�
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