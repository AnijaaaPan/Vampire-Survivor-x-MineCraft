using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItem", menuName = "CreateSpecialItem")]
public class SpecialItem : ScriptableObject
{
    [SerializeField]
    private string name; // SpecialItem�̖��O

    [SerializeField]
    private string description; // SpecialItem�̐�������1

    [SerializeField]
    private string effect; // SpecialItem�̐�������2

    [SerializeField]
    private int id; // SpecialItem��ID

    [SerializeField]
    private Sprite icon; // SpecialItem�̃A�C�R��

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