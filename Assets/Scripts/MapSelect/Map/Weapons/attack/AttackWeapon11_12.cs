using UnityEngine;
using System.Collections;

public class AttackWeapon11_12: MonoBehaviour
{
    public RectTransform Chara;

    public float Radian;

    public WeaponParn WeaponParn;

    IEnumerator Start()
    {
        int Count = 0;
        yield return new WaitForSeconds(WeaponParn.atk_time);

        while (Count < 5)
        {
            if (IsPlaying.instance.isPlay())
            {
                transform.localScale = new Vector3(transform.localScale.x * 0.9f, transform.localScale.y * 0.9f);
                Count++;
            }
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
        yield break;
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = Chara.transform.position.x + cos * 2.5f * WeaponParn.range * WeaponParn.AddSpeed;
        float MoveY = Chara.position.y - 0.475f + sin * 2.5f * WeaponParn.range * WeaponParn.AddSpeed;

        transform.position = new Vector3(MoveX, MoveY);

        Radian -= 3.75f;
    }
}
