using UnityEngine;
using System.Collections;

public class Weapon3 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private GameObject InitWeaponAttack;

    IEnumerator Start()
    {
        InitWeaponAttack = transform.Find("InitWeaponAttack").gameObject;

        int WeaponPhase = WeaponStatus.instance.GetStatusPhase(weapon.GetId());
        StartCoroutine(RepeatWeapon(WeaponPhase));

        while (true)
        {
            WeaponPhase = WeaponStatus.instance.GetStatusPhase(weapon.GetId());
            float WeaponCoolDown = ReturnWeaponCoolDown(WeaponPhase);
            yield return new WaitForSeconds(WeaponCoolDown);

            if (IsPlaying.instance.isPlay()) StartCoroutine(RepeatWeapon(WeaponPhase));
        }
    }

    private IEnumerator RepeatWeapon(int WeaponPhase)
    {
        int WeaponCount = 0;
        int AttackCount = ReturnAttackCount(WeaponPhase);

        while (WeaponCount < AttackCount)
        {
            if (IsPlaying.instance.isPlay())
            {
                CreateWeapon();
                WeaponCount++;
            }

            float AttackSpeed = ReturnAttackSpeed();
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    private void CreateWeapon()
    {
        float AttackSize = ReturnAttackSize();

        GameObject Object = Instantiate(InitWeaponAttack);
        Object.name = $"AttackWeapon{weapon.GetId()}";
        Object.transform.SetParent(transform);

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);
        ObjectRectTransform.transform.localScale = new Vector3(AttackSize, AttackSize, 0);

        AttackWeapon3 ObjectAttackWeapon3 = Object.GetComponent<AttackWeapon3>();
        ObjectAttackWeapon3.weapon = weapon;
        ObjectAttackWeapon3.Chara = Chara;
        Object.SetActive(true);
    }

    private float ReturnWeaponCoolDown(int WeaponPhase)
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(9) * 0.08f;
        float WeaponCoolDown = weapon.GetParameter().cooldown;
        if (WeaponPhase >= 3) WeaponCoolDown -= WeaponCoolDown * 0.2f;
        return WeaponCoolDown - WeaponCoolDown * CoolDown;
    }

    private float ReturnAttackSpeed()
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(7) * 0.1f;
        float WeaponCoolDown = weapon.GetParameter().atk_spd;
        return WeaponCoolDown - WeaponCoolDown * CoolDown;
    }

    private int ReturnAttackCount(int WeaponPhase)
    {
        int AttackCount = (int)weapon.GetParameter().atk_count;
        AttackCount += ItemStatus.instance.GetAllStatusPhase(9);
        if (WeaponPhase >= 2) AttackCount += 1;
        if (WeaponPhase >= 4) AttackCount += 1;
        if (WeaponPhase >= 6) AttackCount += 1;
        return AttackCount;
    }

    private float ReturnAttackSize()
    {
        float AttackSize = weapon.GetParameter().range;
        AttackSize += ItemStatus.instance.GetAllStatusPhase(6);
        return AttackSize;
    }
}
