using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon25_26_27_Zone : MonoBehaviour
{
    public Weapon Weapon;
    public RectTransform Chara;

    private WeaponParn WeaponParn;

    private float Radian = 0;
    private Vector3 InitLocalScale;

    void Start()
    {
        InitLocalScale = transform.localScale;
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(Weapon);
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;
        ZonePositionUpdate();
        ZoneScaleUpdate();
    }

    private void ZonePositionUpdate()
    {
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = Chara.transform.position.x + cos * 3.35f * WeaponParn.range * WeaponParn.AddSpeed;
        float MoveY = Chara.position.y - 0.475f + sin * 3.35f * WeaponParn.range * WeaponParn.AddSpeed;

        transform.position = new Vector3(MoveX, MoveY);
        Radian += gameObject.name == "AttackWeaponZoneRight" ? -1 : 1 * WeaponParn.AddSpeed;
    }

    private void ZoneScaleUpdate()
    {
        transform.localScale = InitLocalScale * WeaponParn.range;
    }
}
