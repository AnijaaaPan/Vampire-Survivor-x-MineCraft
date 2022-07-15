using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private int id; // EnemyのID

    [SerializeField]
    private string name; // Enemyの名前

    [SerializeField]
    private int max_health; // EnemyのMax体力

    [SerializeField]
    private bool playerlevel; // 敵の体力はプレイヤーのレベルによって変化するか否か

    [SerializeField]
    private float exp; // Enemyがドロップする経験値量

    [SerializeField]
    private float damage; // Enemyの基礎攻撃力

    [SerializeField]
    private float speed; // Enemyの移動速度(キャラの基本は100)

    [SerializeField]
    private float KnockBack; // Enemyがノックバックする回数

    [SerializeField]
    private float MaxKnockBack; // EnemyがノックバックするMAX回数
    
    [SerializeField]
    private int UpdateImagePage; // 何フレームごとに画像を変更するか

    [SerializeField]
    private Color Color = new Color(1, 1, 1); // 敵の色の変更

    [SerializeField]
    private Vector3 localScale = new Vector3(1, 1, 1); // 敵のサイズの変更

    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public int GetMaxHealth(int PlayerLv)
    {
        return playerlevel == true ? max_health * PlayerLv : max_health;
    }

    public float GetExp()
    {
        return exp;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetKnockBack()
    {
        return KnockBack;
    }

    public float GetMaxKnockBack()
    {
        return MaxKnockBack;
    }

    public Color GetColor()
    {
        return Color;
    }

    public Vector3 GetLocalScale()
    {
        return localScale;
    }

    public int GetUpdateImagePage()
    {
        return UpdateImagePage;
    }

    public Sprite GetIcon()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Enemy/{GetName()}/");
        return image[0];
    }

    public Sprite[] GetIcons()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Enemy/{GetName()}/");
        return image;
    }
}