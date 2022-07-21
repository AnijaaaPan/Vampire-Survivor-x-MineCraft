using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "CreateEnemyDataBase")]
public class EnemyDataBase : ScriptableObject
{
    [SerializeField]
    private List<Enemy> EnemyLists = new List<Enemy>();

    public List<Enemy> GetEnemyLists()
    {
        return EnemyLists;
    }

    public Enemy FindEnemyFromId(int id)
    {
        return EnemyLists.Find(Enemy => Enemy.GetId() == id);
    }
}