using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]//  Create����CreateItem�Ƃ������j���[��\�����AItem���쐬����
public class Item : ScriptableObject
{
    [SerializeField]
    private string name; //Item�̖��O

    [SerializeField]
    private string description; //Item�̐�������1

    [SerializeField]
    private int id; //Item��ID

    [SerializeField]
    private Sprite icon; //Item�̃A�C�R��

    [SerializeField]
    private int powerup; //�i����̕���ID

    [SerializeField]
    private bool default_item; //�f�t�H���g�ŃA�C�e�����g�p�\���ǂ���

    [SerializeField]
    private List<int> cant_use_waepon; //��������

    public string GetName() //���O����͂�����A
    {
        return name; // name�ɕԂ�
    }
    public string GetDescription() //��������1����͂�����A
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
    public int GetPowerup() //�i����̕���ID����͂�����A
    {
        return powerup; // powerup�ɕԂ� �i���悪�Ȃ��ꍇ��0��Ԃ�
    }
    public bool GetDefault() //�f�t�H���g�ŃA�C�e�����g�p�\���ǂ�������͂�����A
    {
        return default_item; // default_item�ɕԂ�
    }
}