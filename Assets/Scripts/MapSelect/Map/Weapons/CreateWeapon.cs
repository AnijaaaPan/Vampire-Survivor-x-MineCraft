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

    private int WeaponID;

    // WeaponID: 5, 6
    public List<Sprite> ID5_6_Images;

    private Vector3 BeforePosition = new Vector3();
    private float ID5_6_Radian;

    // WeaponID: 11, 12
    public List<Sprite> ID12_Images;
    private float ID11_12_Radian;

    // WeaponID: 13, 14
    private float ID13_14_Radian;

    // WeaponID: 17, 18
    public GameObject InitAttackZone;
    private int AttackCount = 0;

    // WeaponID: 19, 20
    private float ID19_20_Radian;

    // WeaponID: 25, 26, 27
    public GameObject Bird;
    public GameObject Zone;

    IEnumerator Start()
    {
        WeaponID = weapon.GetId();
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);

        InitWeaponAttack = transform.Find("InitWeaponAttack").gameObject;
        while (true)
        {
            if (IsPlaying.instance.isPlay()) StartCoroutine(RepeatWeapon());

            if (WeaponParn.cooldown <= 0.25f) WeaponParn.cooldown = 0.25f;
            yield return new WaitForSeconds(WeaponParn.cooldown);
        }
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        if (WeaponID == 5 || WeaponID == 6) UpdateWeaponID5_6();
        if (WeaponID == 11 || WeaponID == 12) UpdateWeaponID11_12();
    }

    private IEnumerator RepeatWeapon()
    {
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);
        if (WeaponID == 12) WeaponParn.atk_count *= 3;
        if (WeaponID == 13 || WeaponID == 14) ID13_14_Radian = Random.Range(-180f, 180f);
        if (WeaponID == 19 || WeaponID == 20) ID19_20_Radian = Random.Range(-180f, 180f);

        int WeaponCount = 1;
        while (WeaponCount <= WeaponParn.atk_count)
        {
            if (IsPlaying.instance.isPlay())
            {
                CreateWeaponAttack(WeaponCount);
                WeaponCount++;
            }

            if (WeaponParn.atk_spd <= 0.05f) WeaponParn.atk_spd = 0.05f;
            yield return new WaitForSeconds(WeaponParn.atk_spd);
        }
    }

    private void CreateWeaponAttack(int WeaponCount)
    {
        GameObject Object = Instantiate(InitWeaponAttack);

        if (WeaponID == 1 || WeaponID == 2) 
            Object = CreateWeaponID1_2(Object, WeaponCount);
        else if (WeaponID == 3 || WeaponID == 4) 
            Object = CreateWeaponID3_4(Object);
        else if (WeaponID == 5 || WeaponID == 6) 
            Object = CreateWeaponID5_6(Object);
        else if (WeaponID == 7) 
            Object = CreateWeaponID7(Object);
        else if (WeaponID == 8) 
            Object = CreateWeaponID8(Object, WeaponCount);
        else if (WeaponID == 9 || WeaponID == 10) 
            Object = CreateWeaponID9_10(Object);
        else if (WeaponID == 11 || WeaponID == 12) 
            Object = CreateWeaponID11_12(Object, WeaponCount);
        else if (WeaponID == 13 || WeaponID == 14) 
            Object = CreateWeaponID13_14(Object);
        else if (WeaponID == 17 || WeaponID == 18)
            Object = CreateWeaponID17_18(Object);
        else if (WeaponID == 19 || WeaponID == 20)
            Object = CreateWeaponID19_20(Object);
        else if (WeaponID == 21 || WeaponID == 22)
            Object = CreateWeaponID21_22(Object);
        else if (WeaponID == 25 || WeaponID == 26 || WeaponID == 27)
            Object = CreateWeaponID25_26_27(Object);

        if (Object == null) return;

        Vector3 ObjectScale = Object.transform.localScale;
        Object.name = $"AttackWeapon{weapon.GetId()}";
        Object.transform.localScale = new Vector3(ObjectScale.x * WeaponParn.range, ObjectScale.y * WeaponParn.range, 0);
        Object.transform.SetParent(transform);

        AttackWeaponOnTrigger ObjectAttackWeaponOnTrigger = Object.AddComponent<AttackWeaponOnTrigger>();
        ObjectAttackWeaponOnTrigger.Weapon = weapon;
        Object.SetActive(true);
    }

    private GameObject CreateWeaponID1_2(GameObject Object, int WeaponCount)
    {
        float WeaponX = 2 * Mathf.Sign(Chara.localScale.x) * -1;
        if (WeaponCount % 2 == 0)
        {
            WeaponX *= -1;
        }
        float WeaponY = Chara.position.y + WeaponCount * 0.5f;

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + WeaponX, WeaponY, 0);
        if (WeaponCount % 2 == 0) Object.transform.Rotate(0, 0, 180);
        ObjectRectTransform.transform.localScale = new Vector3(Mathf.Sign(Chara.localScale.x) * -1, 1, 0);

        Object.AddComponent<AttackWeapon1_2>();
        return Object;
    }

    private GameObject CreateWeaponID3_4(GameObject Object)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);

        AttackWeapon3_4 ObjectAttackWeapon3_4 = Object.AddComponent<AttackWeapon3_4>();
        ObjectAttackWeapon3_4.Chara = Chara;
        ObjectAttackWeapon3_4.WeaponParn = WeaponParn;
        return Object;
    }

    private GameObject CreateWeaponID5_6(GameObject Object)
    {
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = GetRandom(ID5_6_Images);

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x + Random.Range(-0.25f, 0.25f), Chara.position.y + Random.Range(-0.25f, 0.25f), 0);
        ObjectRectTransform.Rotate(0, 0, ID5_6_Radian);

        AttackWeapon5_6 ObjectAttackWeapon5_6 = Object.AddComponent<AttackWeapon5_6>();
        ObjectAttackWeapon5_6.Chara = Chara;
        ObjectAttackWeapon5_6.WeaponParn = WeaponParn;
        ObjectAttackWeapon5_6.Radian = ID5_6_Radian;
        return Object;
    }

    private void UpdateWeaponID5_6()
    {
        if (Chara.transform.position.x == BeforePosition.x && Chara.transform.position.y == BeforePosition.y) return;

        ID5_6_Radian = GetRadian(BeforePosition.x, BeforePosition.y, Chara.transform.position.x, Chara.transform.position.y) * (180 / Mathf.PI);
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

    private GameObject CreateWeaponID8(GameObject Object, int WeaponCount)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);

        float OneRadian = 360 / WeaponParn.atk_count;
        AttackWeapon8 ObjectAttackWeapon8 = Object.AddComponent<AttackWeapon8>();
        ObjectAttackWeapon8.Chara = Chara;
        ObjectAttackWeapon8.Radian = 90 - OneRadian * (WeaponCount - 1);
        ObjectAttackWeapon8.WeaponParn = WeaponParn;
        return Object;
    }

    private GameObject CreateWeaponID9_10(GameObject Object)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);

        AttackWeapon9_10 ObjectAttackWeapon9_10 = Object.AddComponent<AttackWeapon9_10>();
        ObjectAttackWeapon9_10.Chara = Chara;
        ObjectAttackWeapon9_10.ObjectRectTransform = ObjectRectTransform;
        ObjectAttackWeapon9_10.acceleration = new Vector3(-10, -10f, 0);
        ObjectAttackWeapon9_10.initialVelocity = new Vector3(8, 8, 0);
        ObjectAttackWeapon9_10.WeaponParn = WeaponParn;
        return Object;
    }

    private GameObject CreateWeaponID11_12(GameObject Object, int WeaponCount)
    {
        if (WeaponID == 12)
        {
            Image ObjectImage = Object.GetComponent<Image>();
            ObjectImage.sprite = ID12_Images[WeaponCount % 3];
        }

        float OneRadian = 360f / WeaponParn.atk_count;
        AttackWeapon11_12 ObjectAttackWeapon11_12 = Object.AddComponent<AttackWeapon11_12>();
        ObjectAttackWeapon11_12.Chara = Chara;
        ObjectAttackWeapon11_12.Radian = OneRadian * (WeaponCount - 1) + ID11_12_Radian;
        ObjectAttackWeapon11_12.WeaponParn = WeaponParn;
        return Object;
    }

    private void UpdateWeaponID11_12()
    {
        ID11_12_Radian -= 3.75f;
    }

    private GameObject CreateWeaponID13_14(GameObject Object)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);

        AttackWeapon13_14 ObjectAttackWeapon13_14 = Object.AddComponent<AttackWeapon13_14>();
        ObjectAttackWeapon13_14.Chara = Chara;
        ObjectAttackWeapon13_14.Radian = ID13_14_Radian;
        return Object;
    }

    private GameObject CreateWeaponID17_18(GameObject Object)
    {
        float DropPointX;
        float DropPointY;
        float WeaponSpawnX = Random.Range(Chara.transform.position.x - 10f, Chara.transform.position.x + 10f);
        float WeaponSpawnY = Chara.transform.position.y + 6.15f;

        if (WeaponID == 17)
        {
            DropPointX = Random.Range(Chara.transform.position.x - 10f, Chara.transform.position.x + 10f);
            DropPointY = Random.Range(Chara.transform.position.y - 5.5f, Chara.transform.position.y + 5.5f);
        } else
        {
            float Radian = AttackCount * 30;

            float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
            float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

            DropPointX = Chara.transform.position.x + cos * 7.5f;
            DropPointY = Chara.transform.position.y + sin * 3.75f;

            AttackCount++;
        }

        float ID17_18_Radian = GetRadian(WeaponSpawnX, WeaponSpawnY, DropPointX, DropPointY) * (180 / Mathf.PI);

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(WeaponSpawnX, WeaponSpawnY, 0);

        AttackWeapon17_18 ObjectAttackWeapon17_18 = Object.AddComponent<AttackWeapon17_18>();
        ObjectAttackWeapon17_18.Chara = Chara;
        ObjectAttackWeapon17_18.weapon = weapon;
        ObjectAttackWeapon17_18.Radian = ID17_18_Radian;
        ObjectAttackWeapon17_18.DropPoint = new Vector2(DropPointX, DropPointY);
        ObjectAttackWeapon17_18.WeaponParn = WeaponParn;
        return Object;
    }

    private GameObject CreateWeaponID19_20(GameObject Object)
    {
        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.position = new Vector3(Chara.position.x, Chara.position.y, 0);

        AttackWeapon19_20 ObjectAttackWeapon19_20 = Object.AddComponent<AttackWeapon19_20>();
        ObjectAttackWeapon19_20.weapon = weapon;
        ObjectAttackWeapon19_20.Chara = Chara;
        ObjectAttackWeapon19_20.Radian = ID19_20_Radian;
        ObjectAttackWeapon19_20.WeaponParn = WeaponParn;

        if (WeaponID == 19)
        {
            TrailRenderer ObjectTrailRenderer = Object.GetComponent<TrailRenderer>();
            float RandomColor = Random.Range(0f, 1f);
            ObjectTrailRenderer.startColor = Color.HSVToRGB(RandomColor, 0.5f, 1f);
            ObjectTrailRenderer.endColor = Color.HSVToRGB(RandomColor, 0.3f, 0);
        }
        return Object;
    }

    private GameObject CreateWeaponID21_22(GameObject Object)
    {
        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;

        List<EnemyData> GetEnemyDataList = EnemyStatus.instance.GetEnemyDataList().FindAll(e => {
            Vector3 p = e.Object.transform.position;
            return CameraLeftX <= p.x && p.x <= CameraRightX && CameraBottomY <= p.y && p.y <= CameraTopY;
        });

        if (GetEnemyDataList.Count == 0) return null;

        AttackWeapon21_22 ObjectAttackWeapon21_22 = Object.AddComponent<AttackWeapon21_22>();
        ObjectAttackWeapon21_22.Chara = Chara;
        ObjectAttackWeapon21_22.weapon = weapon;
        ObjectAttackWeapon21_22.EnemyData = GetRandom(GetEnemyDataList);
        ObjectAttackWeapon21_22.WeaponParn = WeaponParn;
        return Object;
    }

    private GameObject CreateWeaponID25_26_27(GameObject Object)
    {

        AttackWeapon25_26_27 ObjectAttackWeapon25_26_27 = Object.AddComponent<AttackWeapon25_26_27>();
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
