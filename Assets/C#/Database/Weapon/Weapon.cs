using UnityEngine;

[System.Serializable]
public class WeaponParn
{
    public float damage; //Weaponのダメージ量
    public float range; //Weaponの効果範囲
    public float atk_spd; //Weaponの攻撃速度
    public float atk_count;  //Weaponの投射数
    public float atk_time; //Weaponの持続時間
    public float cooldown; //Weaponのクールダウン
    public float penetrate; //Weaponの敵貫通数
}

[CreateAssetMenu(fileName = "Weapon", menuName = "CreateWeapon")]//  CreateからCreateWeaponというメニューを表示し、Weaponを作成する
public class Weapon : ScriptableObject
{
    [SerializeField]
    private string name; //Weaponの名前

    [SerializeField]
    private string description; //Weaponの説明その1

    [SerializeField]
    private string effect; //Weaponの説明その2

    [SerializeField]
    private int id; //WeaponのID

    [SerializeField]
    private Sprite icon; //Weaponのアイコン

    [SerializeField]
    private int powerup; //進化先の武器ID

    [SerializeField]
    private bool default_waepon; //デフォルトで武器が使用可能かどうか

    [SerializeField]
    private WeaponParn parameter; //Weaponのデフォルト攻撃力

    public string GetName() //名前を入力したら、
    {
        return name; // nameに返す
    }
    public string GetDescription() //説明その1を入力したら、
    {
        return description; // descriptionに返す
    }
    public string GetEffect() //説明その2を入力したら、
    {
        return effect; // effectに返す
    }
    public int GetId() //IDを入力したら、
    {
        return id; // idに返す
    }
    public Sprite GetIcon() //アイコンを入力したら、
    {
        return icon; // iconに返す
    }
    public int GetPowerup() //進化先の武器IDを入力したら、
    {
        return powerup; // powerupに返す 進化先がない場合は0を返す
    }
    public bool GetDefault() //デフォルトで武器が使用可能かどうかを入力したら、
    {
        return default_waepon; // default_waeponに返す
    }
    public WeaponParn GetParameter() //デフォルトパラメーターを入力したら、
    {
        return parameter; // parameterに返す
    }
}