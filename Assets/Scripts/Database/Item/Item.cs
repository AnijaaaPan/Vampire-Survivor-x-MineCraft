using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string name; // Itemの名前

    [SerializeField]
    private string description; // Itemの説明その1

    [SerializeField]
    private int id; // ItemのID

    [SerializeField]
    private Sprite icon; // Itemのアイコン

    [SerializeField]
    private List<Weapon> weapon; // 進化先の武器ID

    [SerializeField]
    private int cost; // パワーアップするために必要なコスト

    [SerializeField]
    private int count; // パワーアップ出来る回数

    [SerializeField]
    private bool default_item; // デフォルトでアイテムが使用可能かどうか

    [SerializeField]
    private List<Weapon> CantUse; // 無効武器

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public int GetId()
    {
        return id;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public List<Weapon> GetPowerup()
    {
        return weapon;
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetCount()
    {
        return count;
    }

    public bool GetDefault()
    {
        return default_item;
    }

    public List<Weapon> GetCantUse()
    {
        return CantUse;
    }
}