using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItem", menuName = "CreateSpecialItem")]//  Create����CreateSpecialItem�Ƃ������j���[��\�����ASpecialItem���쐬����
public class SpecialItem : ScriptableObject
{
    [SerializeField]
    private string name; //SpecialItem�̖��O

    [SerializeField]
    private string description; //SpecialItem�̐�������1

    [SerializeField]
    private string effect; //SpecialItem�̐�������2

    [SerializeField]
    private int id; //SpecialItem��ID

    [SerializeField]
    private Sprite icon; //SpecialItem�̃A�C�R��

    public string GetName() //���O����͂�����A
    {
        return name; // name�ɕԂ�
    }
    public string GetDescription() //��������1����͂�����A
    {
        return description; // description�ɕԂ�
    }
    public string GetEffect() //��������2����͂�����A
    {
        return effect; // effect�ɕԂ�
    }
    public int GetId() //ID����͂�����A
    {
        return id; // id�ɕԂ�
    }
    public Sprite GetIcon() //�A�C�R������͂�����A
    {
        return icon; // icon�ɕԂ�
    }
}