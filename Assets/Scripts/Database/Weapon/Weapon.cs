using UnityEngine;

[System.Serializable]
public class WeaponParn
{
    public float damage; // Weapon�̃_���[�W��
    public float range; // Weapon�̌��ʔ͈�
    public float atk_spd; // Weapon�̍U�����x
    public float atk_count;  // Weapon�̓��ː�
    public float atk_time; // Weapon�̎�������
    public float cooldown; // Weapon�̃N�[���_�E��
    public int penetrate; // Weapon�̓G�ђʐ�
}

[CreateAssetMenu(fileName = "Weapon", menuName = "CreateWeapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    private string name; // Weapon�̖��O

    [SerializeField]
    private string description; // Weapon�̐�������1

    [SerializeField]
    private string effect; // Weapon�̐�������2

    [SerializeField]
    private int id; // Weapon��ID

    [SerializeField]
    private Sprite icon; // Weapon�̃A�C�R��

    [SerializeField]
    private Weapon weapon; // �i����̕���ID

    [SerializeField]
    private bool default_waepon; // �f�t�H���g�ŕ��킪�g�p�\���ǂ���

    [SerializeField]
    private int play_count; // �v���C���Ƀp���[�A�b�v�o�����

    [SerializeField]
    private WeaponParn parameter; // Weapon�̃f�t�H���g�U����

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetEffect()
    {
        return effect;
    }

    public int GetId()
    {
        return id;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public bool GetDefault()
    {
        return default_waepon;
    }

    public int GetPlayCount()
    {
        return play_count;
    }

    public WeaponParn GetParameter()
    {
        return parameter;
    }
}