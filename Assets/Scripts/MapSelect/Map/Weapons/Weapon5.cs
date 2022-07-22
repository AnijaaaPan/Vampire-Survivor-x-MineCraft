using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Weapon5 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;
    public List<Sprite> Images;

    private GameObject InitWeaponAttack;
    private Vector3 BeforePosition = new Vector3();

    private float Radian;

    IEnumerator Start()
    {
        InitWeaponAttack = transform.Find("InitWeaponAttack").gameObject;

        StartCoroutine(RepeatWeapon());

        while (true)
        {
            int WeaponPhase = WeaponStatus.instance.GetStatusPhase(weapon.GetId());
            float WeaponCoolDown = ReturnWeaponCoolDown();
            yield return new WaitForSeconds(WeaponCoolDown);

            if (IsPlaying.instance.isPlay()) StartCoroutine(RepeatWeapon(WeaponPhase));
        }
    }

    private IEnumerator RepeatWeapon(int WeaponPhase = 1)
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

    void Update()
    {
        if (Chara.transform.position.x == BeforePosition.x && Chara.transform.position.y == BeforePosition.y) return;

        Radian = GetRadian(BeforePosition.x, BeforePosition.y, Chara.transform.position.x, Chara.transform.position.y) * (180 / Mathf.PI);
        BeforePosition = Chara.transform.position;
    }

    private void CreateWeapon()
    {
        float AttackSize = ReturnAttackSize();

        GameObject Object = Instantiate(InitWeaponAttack);
        Object.name = $"AttackWeapon{weapon.GetId()}";
        Object.transform.SetParent(transform);

        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = GetRandom(Images);

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + Random.Range(-0.25f, 0.25f), Chara.position.y + Random.Range(-0.25f, 0.25f), 0);
        ObjectRectTransform.transform.localScale = new Vector3(AttackSize, AttackSize, 0);
        ObjectRectTransform.Rotate(0, 0, Radian);

        AttackWeapon5 ObjectAttackWeapon5 = Object.AddComponent<AttackWeapon5>();
        ObjectAttackWeapon5.weapon = weapon;
        ObjectAttackWeapon5.Chara = Chara;
        ObjectAttackWeapon5.Radian = Radian;

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
        if (WeaponPhase >= 3) AttackCount += 1;
        if (WeaponPhase >= 4) AttackCount += 1;
        if (WeaponPhase >= 6) AttackCount += 1;
        if (WeaponPhase >= 7) AttackCount += 1;
        return AttackCount;
    }

    private float ReturnAttackSize()
    {
        float AttackSize = weapon.GetParameter().range;
        AttackSize += 0.1f * ItemStatus.instance.GetAllStatusPhase(6);
        return AttackSize;
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }
}
