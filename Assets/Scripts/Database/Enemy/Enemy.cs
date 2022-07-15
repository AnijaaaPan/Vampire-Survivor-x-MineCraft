using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private int id; // Enemy��ID

    [SerializeField]
    private string name; // Enemy�̖��O

    [SerializeField]
    private int max_health; // Enemy��Max�̗�

    [SerializeField]
    private bool playerlevel; // �G�̗̑͂̓v���C���[�̃��x���ɂ���ĕω����邩�ۂ�

    [SerializeField]
    private float exp; // Enemy���h���b�v����o���l��

    [SerializeField]
    private float damage; // Enemy�̊�b�U����

    [SerializeField]
    private float speed; // Enemy�̈ړ����x(�L�����̊�{��100)

    [SerializeField]
    private float KnockBack; // Enemy���m�b�N�o�b�N�����

    [SerializeField]
    private float MaxKnockBack; // Enemy���m�b�N�o�b�N����MAX��
    
    [SerializeField]
    private int UpdateImagePage; // ���t���[�����Ƃɉ摜��ύX���邩

    [SerializeField]
    private Color Color = new Color(1, 1, 1); // �G�̐F�̕ύX

    [SerializeField]
    private Vector3 localScale = new Vector3(1, 1, 1); // �G�̃T�C�Y�̕ύX

    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public int GetMaxHealth(int PlayerLv)
    {
        return playerlevel == true ? max_health * PlayerLv : max_health;
    }

    public float GetExp()
    {
        return exp;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetKnockBack()
    {
        return KnockBack;
    }

    public float GetMaxKnockBack()
    {
        return MaxKnockBack;
    }

    public Color GetColor()
    {
        return Color;
    }

    public Vector3 GetLocalScale()
    {
        return localScale;
    }

    public int GetUpdateImagePage()
    {
        return UpdateImagePage;
    }

    public Sprite GetIcon()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Enemy/{GetName()}/");
        return image[0];
    }

    public Sprite[] GetIcons()
    {
        Sprite[] image = Resources.LoadAll<Sprite>($"Enemy/{GetName()}/");
        return image;
    }
}