using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    public int effect_id; //����id
    public float phase; //n�i�K�㏸�����邩
}

[CreateAssetMenu(fileName = "Mob", menuName = "CreateMob")]//  Create����CreateMob�Ƃ������j���[��\�����AMob���쐬����
public class Mob: ScriptableObject
{
    [SerializeField]
    private int id; //Mob��ID

    [SerializeField]
    private int weapon_id; //�L������Weapon��ID

    [SerializeField]
    private string name; //Mob�̖��O

    [SerializeField]
    private string description; //Mob�̐���

    [SerializeField]
    private bool use; //���ݎg�p�ł���L�������ۂ�

    [SerializeField]
    private bool hidden; //���ݏ�������������ĂȂ��B����Ă��邩�ۂ�

    [SerializeField]
    private List<Parameter> Parameter; //Mob��parameter

    public string GetName() //���O����͂�����A
    {
        return name; // name�ɕԂ�
    }
    public string GetDescription() //��������͂�����A
    {
        return description; // description�ɕԂ�
    }
    public int GetId() //ID����͂�����A
    {
        return id; // id�ɕԂ�
    }
    public int GetWeaponId() //WeaponID����͂�����A
    {
        return weapon_id; // weapon_id�ɕԂ�
    }
    public Sprite GetIcon() //�A�C�R������͂�����A
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Mob/{GetName()}/");
        return image[0]; // IconList[0]�ɕԂ�
    }
    public Sprite[] GetIcons() //�A�C�R�����X�g����͂�����A
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Mob/{GetName()}/");
        return image; // image�ɕԂ�
    }
    public bool GetUse() //���ݎg�p�ł���L�������ۂ�����͂�����A
    {
        return use; // use�ɕԂ�
    }
    public bool GetHidden() //���ݏ�������������ĂȂ��B����Ă��邩�ۂ�����͂�����A
    {
        return hidden; // hidden�ɕԂ�
    }
    public List<Parameter> GetParameter() //MOB�̃X�e�[�^�X�p�����[�^�̐ݒ�
    {
        return Parameter; // Parameter�ɕԂ�
    }
}