using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackWeaponOnTrigger : MonoBehaviour
{
    public Weapon Weapon;

    private WeaponParn WeaponParn;
    private int Penetrate = 0;
    private List<GameObject> EnemyObjectList = new List<GameObject>();

    IEnumerator Start()
    {
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(Weapon);
        while (true)
        {
            yield return new WaitForSeconds(WeaponParn.cooldown);

            if (IsPlaying.instance.isPlay()) AttackToAllEnemy();
            WeaponParn = WeaponParameter.instance.GetWeaponParameter(Weapon);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;
        AttackToEnemy(collision.gameObject);
        AddEnemyObject(collision.gameObject);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;
        AddEnemyObject(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;
        RemoveEnemyObject(collision.gameObject);
    }

    private void AddEnemyObject(GameObject Object)
    {
        if (EnemyObjectList.Contains(Object)) return;
        EnemyObjectList.Add(Object);
    }

    private void RemoveEnemyObject(GameObject Object)
    {
        if (!EnemyObjectList.Contains(Object)) return;
        EnemyObjectList.Remove(Object);
    }

    private void AttackToAllEnemy()
    {
        EnemyObjectList.ForEach(o => AttackToEnemy(o));
        EnemyObjectList = new List<GameObject>();
    }

    private void AttackToEnemy(GameObject Object)
    {
        if (Object == null) return;

        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == Object);
        if (EnemyData == null) return;

        WeaponStatus.instance.WeaponDamage(Weapon, WeaponParn.damage);
        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, (int)WeaponParn.damage);

        Penetrate++;
        if (WeaponParn.penetrate == Penetrate) Destroy(gameObject);
    }
}
