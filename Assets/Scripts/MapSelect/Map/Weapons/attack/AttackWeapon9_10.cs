using UnityEngine;
using System.Collections.Generic;

public class AttackWeapon9_10: MonoBehaviour
{
    public RectTransform Chara;
    public RectTransform ObjectRectTransform;
    public WeaponParn WeaponParn;

    // 加速度
    public Vector3 acceleration;
    // 初速度
    public Vector3 initialVelocity;

    // 現在速度
    private Vector3 _velocity;
    private float Radian;

    void Start()
    {
        CheckMostNearEnemy();

        // 初速度で初期化
        _velocity = initialVelocity;
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        _velocity += acceleration * Time.deltaTime;
        float MoveX = transform.position.x + cos * _velocity.x * Time.deltaTime * WeaponParn.AddSpeed;
        float MoveY = transform.position.y + sin * _velocity.y * Time.deltaTime * WeaponParn.AddSpeed;
        transform.position = new Vector3(MoveX, MoveY, 0);

        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;

        if (MoveX <= CameraLeftX || CameraRightX <= MoveX ||
            MoveY <= CameraBottomY || CameraTopY <= MoveY) Destroy(gameObject);
    }

    private void CheckMostNearEnemy()
    {
        Vector3 CharaPosition = Chara.transform.position;
        Vector3 EnemyPosition = new Vector3();
        float closeDist = 10.75f;

        List<EnemyData> GetEnemyDataList = EnemyStatus.instance.GetEnemyDataList();
        for (int i = 0; i < GetEnemyDataList.Count; i++)
        {
            EnemyData EnemyData = GetEnemyDataList[i];
            Vector3 p = EnemyData.Object.transform.position;
            float tDist = Vector3.Distance(CharaPosition, p);

            if (closeDist > tDist)
            {
                closeDist = tDist;
                EnemyPosition = p;
            }
        }

        Radian = GetRadian(CharaPosition.x, CharaPosition.y, EnemyPosition.x, EnemyPosition.y) * (180 / Mathf.PI);
        ObjectRectTransform.Rotate(0, 0, Radian);
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }
}
