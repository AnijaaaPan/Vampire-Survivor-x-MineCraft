using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItem", menuName = "CreateSpecialItem")]
public class SpecialItem : ScriptableObject
{
    [SerializeField]
    private string name; // SpecialItemの名前

    [SerializeField]
    private string description; // SpecialItemの説明その1

    [SerializeField]
    private string effect; // SpecialItemの説明その2

    [SerializeField]
    private int id; // SpecialItemのID

    [SerializeField]
    private Sprite icon; // SpecialItemのアイコン

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
}