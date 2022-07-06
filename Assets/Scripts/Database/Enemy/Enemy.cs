using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Appearances
{
    public Map Stage; // どのステージで出現するか
    public int Minute; // n分目に出現するか
    public string Type; // 出現条件のタイプ
}

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private int ID; // EnemyのID

    [SerializeField]
    private string Name; // Enemyの名前

    [SerializeField]
    private int MaxHealth; // EnemyのMax体力

    [SerializeField]
    private string Type; // Enemyのタイプ

    [SerializeField]
    private int DropXP; // Enemyがドロップする経験値量

    [SerializeField]
    private int DefaultDamage; // Enemyの基礎攻撃力

    [SerializeField]
    private int MoveSpeed; // Enemyの移動速度(キャラの基本は100)

    [SerializeField]
    private int MaxKnockback; // Enemyがノックバックする回数

    [SerializeField]
    private List<Appearances> Appearances; // Enemyのステージ情報

    public string GetName()
    {
        return Name;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public string GetType()
    {
        return Type;
    }

    public int GetDropXP()
    {
        return DropXP;
    }

    public int GetDefaultDamage()
    {
        return DefaultDamage;
    }

    public int GetMoveSpeed()
    {
        return MoveSpeed;
    }

    public int GetMaxKnockback()
    {
        return MaxKnockback;
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

    public List<Appearances> GetAppearances()
    {
        return Appearances;
    }
}