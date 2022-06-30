using UnityEngine;

[System.Serializable]
public class MapParameter
{
    public int MoveSpeed; //�ړ����x
    public int EmeraldBonus; //�G�������h�{�[�i�X
    public int LuckBonus; //���b�L�[�{�[�i�X
}

[System.Serializable]
public class HyperMode
{
    public int MoveSpeed; //�ړ����x
    public int EmeraldBonus; //�G�������h�{�[�i�X
    public int LuckBonus; //���b�L�[�{�[�i�X
}

[CreateAssetMenu(fileName = "Map", menuName = "CreateMap")]//  Create����CreateMap�Ƃ������j���[��\�����AMap���쐬����
public class Map : ScriptableObject
{
    [SerializeField]
    private string name; //Map�̖��O

    [SerializeField]
    private string type; //Map�̃^�C�v

    [SerializeField]
    private int id; //Map��ID

    [SerializeField]
    private int sizeX; //Map��x���̑傫��

    [SerializeField]
    private int sizeY; //Map��y���̑傫��

    [SerializeField]
    private string description; //Map�̐���

    [SerializeField]
    private Sprite icon; //Map�̃A�C�R��

    [SerializeField]
    private Sprite block_icon; //Map�̃u���b�N�A�C�R��

    [SerializeField]
    private bool use; //�f�t�H���g����V�ׂ�MAP���ۂ�

    [SerializeField]
    private MapParameter MapParameter; //Map��parameter

    [SerializeField]
    private HyperMode HyperMode; //Map��HyperMode��Ԃ̒ǉ�parameter

    public string GetName() //���O����͂�����A
    {
        return name; // name�ɕԂ�
    }
    public string GetType() //�^�C�v����͂�����A
    {
        return type; // type�ɕԂ�
    }
    public int GetId() //ID����͂�����A
    {
        return id; // id�ɕԂ�
    }
    public int GetSizeX() //Map��x���̑傫������͂�����A
    {
        return sizeX; // sizeX�ɕԂ�
    }
    public int GetSizeY() //Map��y���̑傫������͂�����A
    {
        return sizeY; // sizeY�ɕԂ�
    }
    public string GetDescription() //��������͂�����A
    {
        return description; // description�ɕԂ�
    }
    public Sprite GetIcon() //�A�C�R������͂�����A
    {
        return icon; // icon�ɕԂ�
    }
    public Sprite GetBlockIcon() //�u���b�N�A�C�R������͂�����A
    {
        return block_icon; // block_icon�ɕԂ�
    }
    public bool GetUse() //�f�t�H���g����V�ׂ�MAP���ۂ�����͂�����A
    {
        return use; // use�ɕԂ�
    }
    public MapParameter GetParameter() //Map��parameter�̐ݒ�
    {
        return MapParameter; // MapParameter�ɕԂ�
    }
    public HyperMode GetHyperParameter() //Map��HyperMode��Ԃ̒ǉ�parameter�̐ݒ�
    {
        return HyperMode; // HyperMode�ɕԂ�
    }
}