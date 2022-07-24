using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon17_18 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;
    public float Radian;
    public Vector2 DropPoint;
    public WeaponParn WeaponParn;

    private Sprite[] Images;
    private int ImageCount;

    private GameObject AttackZoneObject;
    private GameObject SpreadPotionObject;
    private Image SpreadPotionImage;

    private float ScaleValue = 1f;
    private Vector3 InitLocalScale;

    private int ImageIndex = 0;
    private float CountTime = 0;
    private float WaitTime = 0.015f;
    private bool isFin = false;

    private bool isDropToPoint = true;

    IEnumerator Start()
    {
        AttackZoneObject = transform.Find("AttackWeaponZone").gameObject;
        AttackZoneObject.GetComponent<AttackWeaponOnTrigger>().Weapon = weapon;

        SpreadPotionObject = AttackZoneObject.transform.Find("SpreadPotion").gameObject;
        SpreadPotionImage = SpreadPotionObject.GetComponent<Image>();

        InitLocalScale = AttackZoneObject.transform.localScale;

        Images = Resources.LoadAll<Sprite>($"Particle/potion/");
        ImageCount = Images.Length - 1;
        

        while (true)
        {
            yield return new WaitForSeconds(WaitTime);

            if (IsPlaying.instance.isPlay())
            {
                if (isDropToPoint)
                {
                    MoveToDropPoint();
                } else
                {
                    if (weapon.GetId() == 18) ZoneMoveToChara();
                    SpreadPotion();
                }
            }
        }
    }

    private void MoveToDropPoint()
    {
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180)) * 0.25f * WeaponParn.AddSpeed;
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180)) * 0.25f * WeaponParn.AddSpeed;

        transform.position += new Vector3(cos, sin);

        if (DropPoint.x - 1 <= transform.position.x && transform.position.x <= DropPoint.x + 1 &&
            DropPoint.y - 1 <= transform.position.y && transform.position.y <= DropPoint.y + 1)
        {
            Destroy(GetComponent<RotAmimation>());
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Image>().color = new Color(1, 1, 1, 0);

            isDropToPoint = false;
            AttackZoneObject.SetActive(true);
        }
    }

    private void ZoneMoveToChara()
    {
        float MoveToCharaRadian = GetRadian(transform.position.x, transform.position.y, Chara.position.x, Chara.position.y) * (180 / Mathf.PI);

        float sin = Mathf.Sin(MoveToCharaRadian * (Mathf.PI / 180));
        float cos = Mathf.Cos(MoveToCharaRadian * (Mathf.PI / 180));

        transform.position += new Vector3(cos, sin, 0) * 2 * Time.deltaTime * WeaponParn.AddSpeed;

        AttackZoneObject.transform.localScale = InitLocalScale * ScaleValue;
        ScaleValue += 0.0015f;
    }

    private void SpreadPotion()
    {
        CountTime += WaitTime;
        if (CountTime >= WeaponParn.atk_time && isFin == false)
        {
            isFin = true;
            ImageIndex = 50;

        }
        else if (ImageIndex >= 50 && isFin == false)
        {
            ImageIndex = 10;
        }

        SpreadPotionImage.sprite = Images[ImageIndex];
        ImageIndex++;

        if (ImageCount == ImageIndex) Destroy(gameObject);
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }
}