using UnityEngine;
using System.Collections;

public class Weapon1 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    public GameObject Weapon1List;
    public GameObject InitWeapon1;

    IEnumerator Start()
    {
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
        int WeaponPhase = WeaponStatus.instance.GetStatusPhase(1);
        int AttackCount = ReturnAttackCount(WeaponPhase);

        while (WeaponCount < AttackCount)
        {
            float AttackSpeed = ReturnAttackSpeed();
            yield return new WaitForSeconds(AttackSpeed);

            CreateWeapon(WeaponCount, WeaponPhase);
            WeaponCount++;
        }
    }

    private void CreateWeapon(int WeaponCount, int WeaponPhase)
    {
        float AttackSize = ReturnAttackSize(WeaponPhase);

        GameObject Object = Instantiate(InitWeapon1);
        Object.name = "AttackWeapon1";
        Object.transform.SetParent(Weapon1List.transform);

        Object.SetActive(true);
        float WeaponX = 2 * Chara.localScale.x * -1;
        if (WeaponCount != 0 && WeaponCount % 1 == 0) {
            WeaponX *= -1;
        }
        float WeaponY = Chara.position.y + WeaponCount * 0.25f;

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + WeaponX, WeaponY, 0);
        if (WeaponCount != 0 && WeaponCount % 1 == 0) Object.transform.Rotate(0, 0, 180);
        ObjectRectTransform.transform.localScale = new Vector3(AttackSize * Chara.localScale.x * -1, AttackSize, 0);
    }

    private float ReturnWeaponCoolDown()
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(9) * 0.08f;
        float WeaponCoolDown = weapon.GetParameter().cooldown;
        return WeaponCoolDown - WeaponCoolDown * CoolDown;
    }

    private float ReturnAttackSpeed()
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(7) * 0.005f;
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
        AttackSize += ItemStatus.instance.GetAllStatusPhase(6);
        if (WeaponPhase >= 4) AttackSize += 0.1f;
        if (WeaponPhase >= 6) AttackSize += 0.1f;
        return AttackSize;
    }
}
