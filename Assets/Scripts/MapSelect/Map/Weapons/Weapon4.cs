using UnityEngine;
using System.Collections;

public class Weapon4 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private GameObject InitWeaponAttack;

    IEnumerator Start()
    {
        InitWeaponAttack = transform.Find("InitWeaponAttack").gameObject;

        float WeaponCoolDown = ReturnWeaponCoolDown();
        StartCoroutine(RepeatWeapon(WeaponCoolDown));

        while (true)
        {
            WeaponCoolDown = ReturnWeaponCoolDown();
            yield return new WaitForSeconds(WeaponCoolDown);

            if (IsPlaying.instance.isPlay()) StartCoroutine(RepeatWeapon(WeaponCoolDown));
        }
    }

    private IEnumerator RepeatWeapon(float WeaponCoolDown)
    {
        int WeaponCount = 0;
        int AttackCount = ReturnAttackCount();
        while (WeaponCount < AttackCount)
        {
            if (IsPlaying.instance.isPlay())
            {
                CreateWeapon();
                WeaponCount++;
            }

            yield return new WaitForSeconds(WeaponCoolDown / AttackCount);
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

        AttackWeapon4 ObjectAttackWeapon4 = Object.AddComponent<AttackWeapon4>();
        ObjectAttackWeapon4.weapon = weapon;
        ObjectAttackWeapon4.Chara = Chara;
        Object.SetActive(true);
    }

    private float ReturnWeaponCoolDown()
    {
        float CoolDown = ItemStatus.instance.GetAllStatusPhase(9) * 0.08f;
        float WeaponCoolDown = weapon.GetParameter().cooldown;
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
