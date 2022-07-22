using UnityEngine;
using System.Collections.Generic;

public class AttackWeapon4 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    private float Radian;

    void Start()
    {
        CheckMostNearEnemy();
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = transform.position.x + cos * 0.035f;
        float MoveY = transform.position.y + sin * 0.035f;
        transform.position = new Vector3(MoveX, MoveY, 0);

        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;

        if (MoveX <= CameraLeftX || CameraRightX <= MoveX ||
            MoveY <= CameraBottomY || CameraTopY <= MoveY) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;

        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == collision.gameObject);
        if (EnemyData == null) return;

        int Damage = GetWeaponDamage();
        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, Damage);
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
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private int GetWeaponDamage()
    {
        int DefaultDamage = (int)weapon.GetParameter().damage;
        return Random.Range(DefaultDamage, DefaultDamage + 10);
    }
}
