using UnityEngine;
using System.Collections;

public class AttackWeapon15_16 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private Vector3 InitLocalScale;
    private WeaponParn WeaponParn;

    IEnumerator Start()
    {
        InitLocalScale = transform.localScale;
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);
        GetComponent<AttackWeaponOnTrigger>().Weapon = weapon;

        while (true)
        {
            yield return new WaitForSeconds(WeaponParn.cooldown);

            WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);
        }
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        transform.position = new Vector3(Chara.position.x, Chara.position.y - 0.475f);
        transform.localScale = InitLocalScale * WeaponParn.range;
    }
}