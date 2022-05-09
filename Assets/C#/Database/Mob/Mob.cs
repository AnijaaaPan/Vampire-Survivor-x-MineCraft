using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "CreateMob")]//  Create����CreateMob�Ƃ������j���[��\�����AMob���쐬����
public class Mob: ScriptableObject
{
    [SerializeField]
    private string name; //Mob�̖��O

    [SerializeField]
    private string description; //Mob�̐���

    [SerializeField]
    private int id; //Mob��ID

    [SerializeField]
    private Sprite icon; //Mob�̃A�C�R��

    [SerializeField]
    private int damage; //Mob�̃f�t�H���g�U����

    [SerializeField]
    private int hp; //Mob�̃f�t�H���g�̗�

    [SerializeField]
    private bool use; //���ݎg�p�ł���L�������ۂ�

    [SerializeField]
    private bool hidden; //���ݏ�������������ĂȂ��B����Ă��邩�ۂ�

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
    public Sprite GetIcon() //�A�C�R������͂�����A
    {
        return icon; // icon�ɕԂ�
    }
    public int GetDamage() //�f�t�H���g�U���͂���͂�����A
    {
        return damage; // damage�ɕԂ�
    }
    public int GetHp() //�f�t�H���g�̗͂���͂�����A
    {
        return hp; // hp�ɕԂ�
    }
    public bool GetUse() //���ݎg�p�ł���L�������ۂ�����͂�����A
    {
        return use; // use�ɕԂ�
    }
    public bool GetHidden() //���ݏ�������������ĂȂ��B����Ă��邩�ۂ�����͂�����A
    {
        return hidden; // hidden�ɕԂ�
    }
}