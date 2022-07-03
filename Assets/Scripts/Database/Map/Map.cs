using UnityEngine;

[System.Serializable]
public class MapParameter
{
    public int MoveSpeed; // �L�����̈ړ����x
    public int EmeraldBonus; // �G�������h�{�[�i�X
    public int LuckBonus; // ���b�L�[�{�[�i�X
}

[System.Serializable]
public class HyperMode
{
    public int MoveSpeed; // �L�����̈ړ����x
    public int EmeraldBonus; // �G�������h�{�[�i�X
    public int LuckBonus; // ���b�L�[�{�[�i�X
}

[CreateAssetMenu(fileName = "Map", menuName = "CreateMap")]
public class Map : ScriptableObject
{
    [SerializeField]
    private string name; // Map�̖��O

    [SerializeField]
    private string type; // Map�̃^�C�v

    [SerializeField]
    private int id; // Map��ID

    [SerializeField]
    private string description; // Map�̐���

    [SerializeField]
    private Sprite icon; // Map�̃A�C�R��

    [SerializeField]
    private Sprite block_icon; // Map�̃u���b�N�A�C�R��

    [SerializeField]
    private bool use; // �V�ׂ�MAP���ۂ�

    [SerializeField]
    private AudioClip music; // MAP�ŗ���鉹�y

    [SerializeField]
    private MapParameter MapParameter; // Map��parameter

    [SerializeField]
    private HyperMode HyperMode; // Map��HyperMode��Ԃ̒ǉ�parameter

    public string GetName()
    {
        return name;
    }

    public string GetType()
    {
        return type;
    }

    public int GetId()
    {
        return id;
    }

    public string GetDescription()
    {
        return description;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Sprite GetBlockIcon()
    {
        return block_icon;
    }

    public bool GetUse()
    {
        return use;
    }

    public AudioClip GetMusic()
    {
        return music;
    }

    public MapParameter GetParameter()
    {
        return MapParameter;
    }

    public HyperMode GetHyperParameter()
    {
        return HyperMode;
    }
}