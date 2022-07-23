using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackWeapon15_16 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private Vector3 InitLocalScale;
    private List<GameObject> EnemyObjectList = new List<GameObject>();
    private WeaponParn WeaponParn;

    IEnumerator Start()
    {
        InitLocalScale = transform.localScale;
        while (true)
        {
            WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);

            yield return new WaitForSeconds(WeaponParn.cooldown);

            if (IsPlaying.instance.isPlay()) AttackToAllEnemy();
        }
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        transform.position = new Vector3(Chara.position.x, Chara.position.y - 0.475f);
        transform.localScale = InitLocalScale * WeaponParn.range;
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
        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == Object);
        if (EnemyData == null) return;

        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, (int)WeaponParn.damage);
    }
}