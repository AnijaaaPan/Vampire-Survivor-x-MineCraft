using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string name; // Item�̖��O

    [SerializeField]
    private string description; // Item�̐�������1

    [SerializeField]
    private int id; // Item��ID

    [SerializeField]
    private Sprite icon; // Item�̃A�C�R��

    [SerializeField]
    private int powerup; // �i����̕���ID

    [SerializeField]
    private int cost; // �p���[�A�b�v���邽�߂ɕK�v�ȃR�X�g

    [SerializeField]
    private int count; // �p���[�A�b�v�o�����

    [SerializeField]
    private bool default_item; // �f�t�H���g�ŃA�C�e�����g�p�\���ǂ���

    [SerializeField]
    private List<int> cant_use_waepon; // ��������

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

    public int GetPowerup()
    {
        return powerup;
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
}