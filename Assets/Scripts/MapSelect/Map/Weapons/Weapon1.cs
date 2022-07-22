using UnityEngine;
using System.Collections;

public class Weapon1 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private GameObject InitWeaponAttack;

    IEnumerator Start()
    {
        InitWeaponAttack = transform.Find("InitWeaponAttack").gameObject;
        StartCoroutine(RepeatWeapon());

        while (true)
        {
            float WeaponCoolDown = ReturnWeaponCoolDown();
            yield return new WaitForSeconds(WeaponCoolDown);

            if (IsPlaying.instance.isPlay()) StartCoroutine(RepeatWeapon());
        }
    }

    private IEnumerator RepeatWeapon()
    {
        int WeaponCount = 0;
        int WeaponPhase = WeaponStatus.instance.GetStatusPhase(weapon.GetId());
        int AttackCount = ReturnAttackCount(WeaponPhase);

        while (WeaponCount < AttackCount)
        {
            if (IsPlaying.instance.isPlay())
            {
                CreateWeapon(WeaponCount, WeaponPhase);
                WeaponCount++;
            }

            float AttackSpeed = ReturnAttackSpeed();
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    private void CreateWeapon(int WeaponCount, int WeaponPhase)
    {
        float AttackSize = ReturnAttackSize(WeaponPhase);

        GameObject Object = Instantiate(InitWeaponAttack);
        Object.name = $"AttackWeapon{weapon.GetId()}";
        Object.transform.SetParent(transform);

        float WeaponX = 2 * Chara.localScale.x * -1;
        if (WeaponCount != 0 && WeaponCount % 1 == 0) {
            WeaponX *= -1;
        }
        float WeaponY = Chara.position.y + WeaponCount * 0.25f;

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + WeaponX, WeaponY, 0);
        if (WeaponCount != 0 && WeaponCount % 1 == 0) Object.transform.Rotate(0, 0, 180);
        ObjectRectTransform.transform.localScale = new Vector3(AttackSize * Chara.localScale.x * -1, AttackSize, 0);

        AttackWeapon1 ObjectAttackWeapon1 = Object.AddComponent<AttackWeapon1>();
        ObjectAttackWeapon1.weapon = weapon;
        Object.SetActive(true);
    }

    private float ReturnWeaponCoolDown()
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(9) * 0.08f;
        float WeaponCoolDown = weapon.GetParameter().cooldown;
        return WeaponCoolDown - WeaponCoolDown * CoolDown;
    }

    private float ReturnAttackSpeed()
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(7) * 0.05f;
        float WeaponCoolDown = weapon.GetParameter().atk_spd;
        return WeaponCoolDown - WeaponCoolDown * CoolDown;
    }

    private int ReturnAttackCount(int WeaponPhase)
    {
        int AttackCount = (int)weapon.GetParameter().atk_count;
        AttackCount += ItemStatus.instance.GetAllStatusPhase(9);
        if (WeaponPhase >= 2) AttackCount += 1;
        return AttackCount;
    }

    private float ReturnAttackSize(int WeaponPhase)
    {
        float AttackSize = weapon.GetParameter().range;
        AttackSize += 0.1f * ItemStatus.instance.GetAllStatusPhase(6);
        if (WeaponPhase >= 4) AttackSize += 0.1f;
        if (WeaponPhase >= 6) AttackSize += 0.1f;
        return AttackSize;
    }
}
