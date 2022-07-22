using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Weapon6 : MonoBehaviour
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

        AttackWeapon6 ObjectAttackWeapon6 = Object.AddComponent<AttackWeapon6>();
        ObjectAttackWeapon6.weapon = weapon;
        ObjectAttackWeapon6.Chara = Chara;
        ObjectAttackWeapon6.Radian = Radian;

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

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }
}
