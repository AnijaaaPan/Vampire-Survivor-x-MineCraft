using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MapParameter
{
    public float StartingSpawns; // ��ԍŏ��ɃX�|�[�����鐔
    public float MoveSpeed; // �L�����̈ړ����x
    public float EnemyMoveSpeed; // �G�̈ړ����x
    public float ProjectileSpeed; // ���ˑ��x
    public float EmeraldBonus; // �G�������h�{�[�i�X
    public float LuckBonus; // ���b�L�[�{�[�i�X
    public float EnemyMinimum; // �G�̏o���{��
}

[System.Serializable]
public class HyperMode
{
    public float StartingSpawns; // ��ԍŏ��ɃX�|�[�����鐔
    public float MoveSpeed; // �L�����̈ړ����x
    public float EnemyMoveSpeed; // �G�̈ړ����x
    public float ProjectileSpeed; // ���ˑ��x
    public float EmeraldBonus; // �G�������h�{�[�i�X
    public float LuckBonus; // ���b�L�[�{�[�i�X
    public float EnemyMinimum; // �G�̏o���{��
}

[System.Serializable]
public class Stageitems
{
    public float x; // x���W
    public float y; // y���W
    public Item item; // Item�̎w��
    public float Chance; // �A�C�e��������m��
}

[System.Serializable]
public class BossEnemys
{
    public Enemy Enemy; // �ǂ̓G���{�X�Ƃ��ēo�ꂷ�邩
    public List<string> Treasure; // ���̓G���痎����󔠂̒��g
}

[System.Serializable]
public class EventEnemy
{
    public Enemy Enemy; // �ǂ̓G���C�x���g�ŏo�����邩
    public float MoveSpeed; // ���̃C�x���g�̎��̓G�̈ړ����x
}

[System.Serializable]
public class Events
{
    public string Type; // �C�x���g�̃^�C�v
    public int AmountPer; // �G�̏o����
    public EventEnemy EventEnemy; // ���̃C�x���g�ł̓G�̏��
    public float Chance; // ���̃C�x���g����������m��
    public int MaxRepeats; // ���̃C�x���g���J��Ԃ��Ŕ��������
    public float Delay; // �J��Ԃ����܂ł̕b��    
}

[System.Serializable]
public class StageEnemys
{
    public int phase; // �t�F�[�Y(n����)
    public List<Enemy> Enemies; // ���̎��ɏo������G�f�[�^�̃��X�g
    public int EnemyCount; // �G���o�������b��
    public float SpawnInterval; // �G���o������p�x(�b��)
    public List<BossEnemys> BossEnemys; // �{�X�̏��ƕ�
    public List<Events> Events; // ���̃t�F�[�Y�ɂǂ�ȃC�x���g�����邩
}

[System.Serializable]
public class EnemySpawn
{
    public bool North;
    public bool South;
    public bool West;
    public bool East;
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

    [SerializeField]
    private float LightSourceChance; // �������X�|�[������m��

    [SerializeField]
    private EnemySpawn EnemySpawn; // �G���X�|�[������ꏊ

    [SerializeField]
    private List<Stageitems> Stageitems; // Stageitems�̐ݒ�

    [SerializeField]
    private List<StageEnemys> StageEnemys; // StageEnemys�̐ݒ�

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

    public float GetLightSourceChance()
    {
        return LightSourceChance;
    }

    public MapParameter GetParameter()
    {
        return MapParameter;
    }

    public HyperMode GetHyperParameter()
    {
        return HyperMode;
    }
    public EnemySpawn GetEnemySpawn()
    {
        return EnemySpawn;
    }

    public List<Stageitems> GetStageitems()
    {
        return Stageitems;
    }

    public List<StageEnemys> GetStageEnemys()
    {
        return StageEnemys;
    }
}