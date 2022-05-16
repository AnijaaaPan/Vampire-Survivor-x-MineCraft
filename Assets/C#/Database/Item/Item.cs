using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]//  CreateからCreateItemというメニューを表示し、Itemを作成する
public class Item : ScriptableObject
{
    [SerializeField]
    private string name; //Itemの名前

    [SerializeField]
    private string description; //Itemの説明その1

    [SerializeField]
    private int id; //ItemのID

    [SerializeField]
    private Sprite icon; //Itemのアイコン

    [SerializeField]
    private int powerup; //進化先の武器ID

    [SerializeField]
    private bool default_item; //デフォルトでアイテムが使用可能かどうか

    [SerializeField]
    private List<int> cant_use_waepon; //無効武器

    public string GetName() //名前を入力したら、
    {
        return name; // nameに返す
    }
    public string GetDescription() //説明その1を入力したら、
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
    public int GetPowerup() //進化先の武器IDを入力したら、
    {
        return powerup; // powerupに返す 進化先がない場合は0を返す
    }
    public bool GetDefault() //デフォルトでアイテムが使用可能かどうかを入力したら、
    {
        return default_item; // default_itemに返す
    }
}