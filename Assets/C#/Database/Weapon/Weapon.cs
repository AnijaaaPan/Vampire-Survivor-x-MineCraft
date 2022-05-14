using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float damage; //Weaponのデフォルト攻撃力

    [SerializeField]
    private int powerup; //進化先の武器ID

    [SerializeField]
    private bool default_waepon; //デフォルトで武器が使用可能かどうか

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
    public float GetDamage() //デフォルト攻撃力を入力したら、
    {
        return damage; // damageに返す
    }
    public int GetPowerup() //進化先の武器IDを入力したら、
    {
        return powerup; // powerupに返す 進化先がない場合は0を返す
    }
    public bool GetDefault() //デフォルトで武器が使用可能かどうかを入力したら、
    {
        return default_waepon; // default_waeponに返す
    }
}