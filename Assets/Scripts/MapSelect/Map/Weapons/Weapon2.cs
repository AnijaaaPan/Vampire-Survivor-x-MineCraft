using UnityEngine;
using System.Collections;

public class Weapon2 : MonoBehaviour
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
        int AttackCount = ReturnAttackCount();

        while (WeaponCount < AttackCount)
        {
            if (IsPlaying.instance.isPlay())
            {
                CreateWeapon(WeaponCount);
                WeaponCount++;
            }

            float AttackSpeed = ReturnAttackSpeed();
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    private void CreateWeapon(int WeaponCount)
    {
        float AttackSize = ReturnAttackSize();

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

        AttackWeapon2 ObjectAttackWeapon2 = Object.AddComponent<AttackWeapon2>();
        ObjectAttackWeapon2.weapon = weapon;
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

    private int ReturnAttackCount()
    {
        int AttackCount = (int)weapon.GetParameter().atk_count;
        AttackCount += ItemStatus.instance.GetAllStatusPhase(9);
        return AttackCount;
    }

    private float ReturnAttackSize()
    {
        float AttackSize = weapon.GetParameter().range;
        AttackSize += 0.1f * ItemStatus.instance.GetAllStatusPhase(6);
        return AttackSize;
    }
}
