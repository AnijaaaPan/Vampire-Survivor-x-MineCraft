using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AttackWeapon19_20 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;
    public WeaponParn WeaponParn;

    public float Radian;
    private bool isflag = false;

    private GameObject AttackZone;

    IEnumerator Start()
    {
        AttackZone = transform.parent.transform.Find("AttackWeaponZone").gameObject;

        Radian += Random.Range(-10f, 10f);

        yield return new WaitForSeconds(WeaponParn.atk_time);

        Destroy(gameObject);
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = transform.position.x + cos * 0.12f * WeaponParn.AddSpeed;
        float MoveY = transform.position.y + sin * 0.12f * WeaponParn.AddSpeed;
        transform.position = new Vector3(MoveX, MoveY, 0);

        float CameraLeftX = Chara.transform.position.x - 10.75f;
        if (MoveX <= CameraLeftX && isflag == false)
        {
            isflag = true;
            Radian = (0 >= Radian ? 180 : -180) + Radian * -1;
            if (weapon.GetId() == 20) CreateExplodeZone();
        }

        float CameraRightX = Chara.transform.position.x + 10.75f;
        if (CameraRightX <= MoveX && isflag == false)
        {
            isflag = true;
            Radian = (Radian >= 0 ? 180 : -180) + Radian * -1;
            if (weapon.GetId() == 20) CreateExplodeZone();
        }

        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;
        if ((CameraTopY <= MoveY || MoveY <= CameraBottomY) && isflag == false)
        {
            isflag = true;
            Radian *= -1;
            if (weapon.GetId() == 20) CreateExplodeZone();
        }

        if (MoveX >= CameraLeftX && CameraRightX >= MoveX &&
            MoveY >= CameraBottomY && CameraTopY >= MoveY && isflag == true)
        {
            isflag = false;
        }
    }

    private void CreateExplodeZone()
    {
        GameObject Object = Instantiate(AttackZone);

        Object.transform.position = transform.position;
        Object.transform.SetParent(transform.parent);
        Object.GetComponent<AttackWeaponOnTrigger>().Weapon = weapon;
        Object.GetComponent<AttackWeapon19_20_Zone>().WeaponParn = WeaponParn;
        Object.SetActive(true);
    }
}
