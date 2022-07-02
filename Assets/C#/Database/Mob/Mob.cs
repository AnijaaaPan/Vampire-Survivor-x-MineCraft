using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    public int effect_id; // ����ID
    public float phase; // ���ʂ̒i�K
}

[CreateAssetMenu(fileName = "Mob", menuName = "CreateMob")]
public class Mob: ScriptableObject
{
    [SerializeField]
    private int id; // Mob��ID

    [SerializeField]
    private int weapon_id; // Mob���g������ID

    [SerializeField]
    private string name; // Mob�̖��O

    [SerializeField]
    private string description; // Mob�̐���

    [SerializeField]
    private bool use; // Mob���g�p�\��Ԃ��ۂ�

    [SerializeField]
    private bool hidden; // Mob���g�p�ł�����т���������Ă��邩�ۂ�

    [SerializeField]
    private List<Parameter> Parameter; // Mob�̃p�����[�^�[

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

    public int GetWeaponId()
    {
        return weapon_id;
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