using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CreateWeapon : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private GameObject InitWeaponAttack;
    private WeaponParn WeaponParn;

    // WeaponID: 5, 6
    public List<Sprite> Images;

    private Vector3 BeforePosition = new Vector3();
    private float Radian;

    IEnumerator Start()
    {
        InitWeaponAttack = transform.Find("InitWeaponAttack").gameObject;
        StartCoroutine(RepeatWeapon());
        while (true)
        {
            yield return new WaitForSeconds(WeaponParn.cooldown);

            if (IsPlaying.instance.isPlay()) StartCoroutine(RepeatWeapon());
        }
    }

    void Update()
    {
        int id = weapon.GetId();
        if (id == 5 || id == 6) UpdateWeaponID5_6();
    }

    private IEnumerator RepeatWeapon()
    {
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);

        int WeaponCount = 1;
        while (WeaponCount <= WeaponParn.atk_count)
        {
            if (IsPlaying.instance.isPlay())
            {
                CreateWeaponAttack(WeaponCount);
                WeaponCount++;
            }

            yield return new WaitForSeconds(WeaponParn.atk_spd);
        }
    }

    private void CreateWeaponAttack(int WeaponCount)
    {
        GameObject Object = Instantiate(InitWeaponAttack);

        int id = weapon.GetId();
        if (id == 1 || id == 2) Object = CreateWeaponID1_2(Object, WeaponCount);
        if (id == 3 || id == 4) Object = CreateWeaponID3_4(Object);
        if (id == 5 || id == 6) Object = CreateWeaponID5_6(Object);
        if (id == 7) Object = CreateWeaponID7(Object);

        Vector3 ObjectScale = Object.transform.localScale;
        Object.name = $"AttackWeapon{weapon.GetId()}";
        Object.transform.localScale = new Vector3(ObjectScale.x * WeaponParn.range, ObjectScale.y * WeaponParn.range, 0);
        Object.transform.SetParent(transform);

        AttackWeaponOnTrigger ObjectAttackWeaponOnTrigger = Object.AddComponent<AttackWeaponOnTrigger>();
        ObjectAttackWeaponOnTrigger.WeaponParn = WeaponParn;

        Object.SetActive(true);
    }

    private GameObject CreateWeaponID1_2(GameObject Object, int WeaponCount)
    {
        float WeaponX = 2 * Chara.localScale.x * -1;
        if (WeaponCount % 2 == 0)
        {
            WeaponX *= -1;
        }
        float WeaponY = Chara.position.y + WeaponCount * 0.5f;

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + WeaponX, WeaponY, 0);
        if (WeaponCount % 2 == 0) Object.transform.Rotate(0, 0, 180);
        ObjectRectTransform.transform.localScale = new Vector3(Chara.localScale.x * -1, 1, 0);

        Object.AddComponent<AttackWeapon1_2>();
        return Object;
    }

    private GameObject CreateWeaponID3_4(GameObject Object)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);

        AttackWeapon3_4 ObjectAttackWeapon3_4 = Object.AddComponent<AttackWeapon3_4>();
        ObjectAttackWeapon3_4.Chara = Chara;
        return Object;
    }

    private GameObject CreateWeaponID5_6(GameObject Object)
    {
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = GetRandom(Images);

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + Random.Range(-0.25f, 0.25f), Chara.position.y + Random.Range(-0.25f, 0.25f), 0);
        ObjectRectTransform.Rotate(0, 0, Radian);

        AttackWeapon5_6 ObjectAttackWeapon5_6 = Object.AddComponent<AttackWeapon5_6>();
        ObjectAttackWeapon5_6.Chara = Chara;
        ObjectAttackWeapon5_6.Radian = Radian;
        return Object;
    }

    private void UpdateWeaponID5_6()
    {
        if (Chara.transform.position.x == BeforePosition.x && Chara.transform.position.y == BeforePosition.y) return;

        Radian = GetRadian(BeforePosition.x, BeforePosition.y, Chara.transform.position.x, Chara.transform.position.y) * (180 / Mathf.PI);
        BeforePosition = Chara.transform.position;
    }

    private GameObject CreateWeaponID7(GameObject Object)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y + 1, 0);

        AttackWeapon7 ObjectAttackWeapon7 = Object.AddComponent<AttackWeapon7>();
        ObjectAttackWeapon7.Chara = Chara;
        return Object;
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
