using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Appearances
{
    public Map Stage; // �ǂ̃X�e�[�W�ŏo�����邩
    public int Minute; // n���ڂɏo�����邩
    public string Type; // �o�������̃^�C�v
}

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private int ID; // Enemy��ID

    [SerializeField]
    private string Name; // Enemy�̖��O

    [SerializeField]
    private int MaxHealth; // Enemy��Max�̗�

    [SerializeField]
    private string Type; // Enemy�̃^�C�v

    [SerializeField]
    private int DropXP; // Enemy���h���b�v����o���l��

    [SerializeField]
    private int DefaultDamage; // Enemy�̊�b�U����

    [SerializeField]
    private int MoveSpeed; // Enemy�̈ړ����x(�L�����̊�{��100)

    [SerializeField]
    private int MaxKnockback; // Enemy���m�b�N�o�b�N�����

    [SerializeField]
    private List<Appearances> Appearances; // Enemy�̃X�e�[�W���

    public string GetName()
    {
        return Name;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public string GetType()
    {
        return Type;
    }

    public int GetDropXP()
    {
        return DropXP;
    }

    public int GetDefaultDamage()
    {
        return DefaultDamage;
    }

    public int GetMoveSpeed()
    {
        return MoveSpeed;
    }

    public int GetMaxKnockback()
    {
        return MaxKnockback;
    }

    public Sprite GetIcon()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Enemy/{GetName()}/");
        return image[0];
    }

    public Sprite[] GetIcons()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Enemy/{GetName()}/");
        return image;
    }

    public List<Appearances> GetAppearances()
    {
        return Appearances;
    }
}