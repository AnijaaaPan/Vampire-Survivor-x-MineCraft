using UnityEngine;

[System.Serializable]
public class WeaponParn
{
    public float damage; //Weapon�̃_���[�W��
    public float range; //Weapon�̌��ʔ͈�
    public float atk_spd; //Weapon�̍U�����x
    public float atk_count;  //Weapon�̓��ː�
    public float atk_time; //Weapon�̎�������
    public float cooldown; //Weapon�̃N�[���_�E��
    public float penetrate; //Weapon�̓G�ђʐ�
}

[CreateAssetMenu(fileName = "Weapon", menuName = "CreateWeapon")]//  Create����CreateWeapon�Ƃ������j���[��\�����AWeapon���쐬����
public class Weapon : ScriptableObject
{
    [SerializeField]
    private string name; //Weapon�̖��O

    [SerializeField]
    private string description; //Weapon�̐�������1

    [SerializeField]
    private string effect; //Weapon�̐�������2

    [SerializeField]
    private int id; //Weapon��ID

    [SerializeField]
    private Sprite icon; //Weapon�̃A�C�R��

    [SerializeField]
    private int powerup; //�i����̕���ID

    [SerializeField]
    private bool default_waepon; //�f�t�H���g�ŕ��킪�g�p�\���ǂ���

    [SerializeField]
    private WeaponParn parameter; //Weapon�̃f�t�H���g�U����

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
    public int GetPowerup() //�i����̕���ID����͂�����A
    {
        return powerup; // powerup�ɕԂ� �i���悪�Ȃ��ꍇ��0��Ԃ�
    }
    public bool GetDefault() //�f�t�H���g�ŕ��킪�g�p�\���ǂ�������͂�����A
    {
        return default_waepon; // default_waepon�ɕԂ�
    }
    public WeaponParn GetParameter() //�f�t�H���g�p�����[�^�[����͂�����A
    {
        return parameter; // parameter�ɕԂ�
    }
}