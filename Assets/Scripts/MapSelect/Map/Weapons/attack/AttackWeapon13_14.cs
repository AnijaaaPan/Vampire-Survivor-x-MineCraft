using UnityEngine;
using System.Collections.Generic;

public class AttackWeapon13_14 : MonoBehaviour
{
    public RectTransform Chara;

    public float Radian;

    void Start()
    {
        Radian += Random.Range(-10f, 10f);
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = transform.position.x + cos * 0.12f;
        float MoveY = transform.position.y + sin * 0.12f;
        transform.position = new Vector3(MoveX, MoveY, 0);
        
        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;

        if (MoveX <= CameraLeftX || CameraRightX <= MoveX ||
            MoveY <= CameraBottomY || CameraTopY <= MoveY) Destroy(gameObject);
    }
}
