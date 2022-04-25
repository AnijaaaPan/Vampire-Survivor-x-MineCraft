using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "CreatePlayer")]//  Create����CreatePlayer�Ƃ������j���[��\�����APlayer���쐬����
public class Player : ScriptableObject
{
    [SerializeField]
    private int money; //�������z

    [SerializeField]
    private int sound; //�T�E���h(SE)�̉���

    [SerializeField]
    private int music; //���y�̉���

    [SerializeField]
    private bool flash; //�_�ł̗L��

    [SerializeField]
    private bool joystick; //�W���C�X�e�B�b�N�̗L��

    [SerializeField]
    private bool damage; //�_���[�W�\�L�̗L��

    public int GetMoney() //�������z����͂�����A
    {
        return money; // money�ɕԂ�
    }
    public int GetSound() //�T�E���h(SE)�̉��ʂ���͂�����A
    {
        return sound; // sound�ɕԂ�
    }
    public int GetMusic() //���y�̉��ʂ���͂�����A
    {
        return music; // music�ɕԂ�
    }
    public bool GetFlash() //�_�ł̗L������͂�����A
    {
        return flash; // flash�ɕԂ�
    }
    public bool GetJoystick() //�W���C�X�e�B�b�N�̗L������͂�����A
    {
        return joystick; // joystick�ɕԂ�
    }
    public bool GetDamage() //�_���[�W�\�L�̗L������͂�����A
    {
        return damage; // damage�ɕԂ�
    }

    public void SetMoney(int value) //�������z����͂�����A
    {
        money = value; // money�ɕԂ�
    }

}