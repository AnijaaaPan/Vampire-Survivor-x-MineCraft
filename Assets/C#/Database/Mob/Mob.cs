using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    public int effect_id; //効果id
    public float phase; //n段階上昇させるか
}

[CreateAssetMenu(fileName = "Mob", menuName = "CreateMob")]//  CreateからCreateMobというメニューを表示し、Mobを作成する
public class Mob: ScriptableObject
{
    [SerializeField]
    private int id; //MobのID

    [SerializeField]
    private int weapon_id; //キャラのWeaponのID

    [SerializeField]
    private string name; //Mobの名前

    [SerializeField]
    private string description; //Mobの説明

    [SerializeField]
    private bool use; //現在使用できるキャラか否か

    [SerializeField]
    private bool hidden; //現在条件が満たされてなく隠されているか否か

    [SerializeField]
    private List<Parameter> Parameter; //Mobのparameter

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
    public int GetWeaponId() //WeaponIDを入力したら、
    {
        return weapon_id; // weapon_idに返す
    }
    public Sprite GetIcon() //アイコンを入力したら、
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Mob/{GetName()}/");
        return image[0]; // IconList[0]に返す
    }
    public Sprite[] GetIcons() //アイコンリストを入力したら、
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Mob/{GetName()}/");
        return image; // imageに返す
    }
    public bool GetUse() //現在使用できるキャラか否かを入力したら、
    {
        return use; // useに返す
    }
    public bool GetHidden() //現在条件が満たされてなく隠されているか否かを入力したら、
    {
        return hidden; // hiddenに返す
    }
    public List<Parameter> GetParameter() //MOBのステータスパラメータの設定
    {
        return Parameter; // Parameterに返す
    }
}