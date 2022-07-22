using UnityEngine;

public class AttackWeapon6 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    public float Radian;
    private int Penetrate = 0;

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = transform.position.x + cos * 0.05f;
        float MoveY = transform.position.y + sin * 0.05f;
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

        int WeaponPhase = WeaponStatus.instance.GetStatusPhase(weapon.GetId());

        int Damage = GetWeaponDamage();
        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, Damage);

        Penetrate++;
        int WeaponPenetrate = GetWeaponPenetrate();
        if (WeaponPenetrate == Penetrate) Destroy(gameObject);
    }

    private int GetWeaponDamage()
    {
        int DefaultDamage = (int)weapon.GetParameter().damage;
        return Random.Range(DefaultDamage, DefaultDamage + 10);
    }

    private int GetWeaponPenetrate()
    {
        int DefaultPenetrate = weapon.GetParameter().penetrate;
        return DefaultPenetrate;
    }
}
