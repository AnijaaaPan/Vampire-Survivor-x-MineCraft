using UnityEngine;

public class AttackWeaponOnTrigger : MonoBehaviour
{
    public WeaponParn WeaponParn;

    private int Penetrate = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;

        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == collision.gameObject);
        if (EnemyData == null) return;

        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, (int)WeaponParn.damage);

        Penetrate++;
        if (WeaponParn.penetrate == Penetrate) Destroy(gameObject);
    }
}
