using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    public Item item; // アイテムID
    public float phase; // アイテムの段階
}

[CreateAssetMenu(fileName = "Mob", menuName = "CreateMob")]
public class Mob : ScriptableObject
{
    [SerializeField]
    private int id; // MobのID

    [SerializeField]
    private Weapon weapon; // Mobが使う武器ID

    [SerializeField]
    private string name; // Mobの名前

    [SerializeField]
    private string description; // Mobの説明

    [SerializeField]
    private bool use; // Mobが使用可能状態か否か

    [SerializeField]
    private bool hidden; // Mobが使用できる実績が解除されているか否か

    [SerializeField]
    private List<Parameter> Parameter; // Mobのパラメーター

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

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public Sprite GetIcon()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Mob/{GetName()}/");
        return image[0];
    }

    public Sprite[] GetIcons()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Mob/{GetName()}/");
        return image;
    }

    public bool GetUse()
    {
        return use;
    }

    public bool GetHidden()
    {
        return hidden;
    }

    public List<Parameter> GetParameter()
    {
        return Parameter;
    }
}