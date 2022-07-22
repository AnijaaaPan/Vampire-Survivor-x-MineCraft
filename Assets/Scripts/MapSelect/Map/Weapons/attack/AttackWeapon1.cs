using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon1 : MonoBehaviour
{
    public Weapon weapon;

    IEnumerator Start()
    {
        Image ObjectImage = GetComponent<Image>();
        float ColorA = 1f;

        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            ObjectImage.color = new Color(1, 1, 1, ColorA);
            ColorA -= 0.3f;
            if (ColorA <= 0) Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;

        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == collision.gameObject);
        if (EnemyData == null) return;

        int Damage = GetWeaponDamage();
        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, Damage);
    }

    private int GetWeaponDamage()
    {
        int DefaultDamage = (int)weapon.GetParameter().damage;
        int WeaponPhase = WeaponStatus.instance.GetStatusPhase(weapon.GetId());

        if (WeaponPhase >= 3) DefaultDamage += 5;
        if (WeaponPhase >= 4) DefaultDamage += 5;
        if (WeaponPhase >= 5) DefaultDamage += 5;
        if (WeaponPhase >= 6) DefaultDamage += 5;
        if (WeaponPhase >= 7) DefaultDamage += 5;
        if (WeaponPhase >= 8) DefaultDamage += 5;

        return Random.Range(DefaultDamage, DefaultDamage + 10);
    }
}
