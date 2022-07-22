using UnityEngine;

[System.Serializable]
public class WeaponParn
{
    public float damage; // Weaponのダメージ量
    public float range; // Weaponの効果範囲
    public float atk_spd; // Weaponの攻撃速度
    public float atk_count;  // Weaponの投射数
    public float atk_time; // Weaponの持続時間
    public float cooldown; // Weaponのクールダウン
    public int penetrate; // Weaponの敵貫通数
}

[CreateAssetMenu(fileName = "Weapon", menuName = "CreateWeapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    private string name; // Weaponの名前

    [SerializeField]
    private string description; // Weaponの説明その1

    [SerializeField]
    private string effect; // Weaponの説明その2

    [SerializeField]
    private int id; // WeaponのID

    [SerializeField]
    private Sprite icon; // Weaponのアイコン

    [SerializeField]
    private Weapon weapon; // 進化先の武器ID

    [SerializeField]
    private bool default_waepon; // デフォルトで武器が使用可能かどうか

    [SerializeField]
    private int play_count; // プレイ中にパワーアップ出来る回数

    [SerializeField]
    private WeaponParn parameter; // Weaponのデフォルト攻撃力

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetEffect()
    {
        return effect;
    }

    public int GetId()
    {
        return id;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public bool GetDefault()
    {
        return default_waepon;
    }

    public int GetPlayCount()
    {
        return play_count;
    }

    public WeaponParn GetParameter()
    {
        return parameter;
    }
}