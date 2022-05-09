using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "CreateMob")]//  CreateからCreateMobというメニューを表示し、Mobを作成する
public class Mob: ScriptableObject
{
    [SerializeField]
    private string name; //Mobの名前

    [SerializeField]
    private string description; //Mobの説明

    [SerializeField]
    private int id; //MobのID

    [SerializeField]
    private Sprite icon; //Mobのアイコン

    [SerializeField]
    private int damage; //Mobのデフォルト攻撃力

    [SerializeField]
    private int hp; //Mobのデフォルト体力

    [SerializeField]
    private bool use; //現在使用できるキャラか否か

    [SerializeField]
    private bool hidden; //現在条件が満たされてなく隠されているか否か

    public string GetName() //名前を入力したら、
    {
        return name; // nameに返す
    }
    public string GetDescription() //説明を入力したら、
    {
        return description; // descriptionに返す
    }
    public int GetId() //IDを入力したら、
    {
        return id; // idに返す
    }
    public Sprite GetIcon() //アイコンを入力したら、
    {
        return icon; // iconに返す
    }
    public int GetDamage() //デフォルト攻撃力を入力したら、
    {
        return damage; // damageに返す
    }
    public int GetHp() //デフォルト体力を入力したら、
    {
        return hp; // hpに返す
    }
    public bool GetUse() //現在使用できるキャラか否かを入力したら、
    {
        return use; // useに返す
    }
    public bool GetHidden() //現在条件が満たされてなく隠されているか否かを入力したら、
    {
        return hidden; // hiddenに返す
    }
}