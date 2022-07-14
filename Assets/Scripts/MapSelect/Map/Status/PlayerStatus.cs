using UnityEngine;
using UnityEngine.UI;

public class PlayerData
{
    public int MaxHP = 100;
    public int HP = 100;
    public int Emerald = 0;
    public int Defeat = 0;
    public int ALLEXP = 0;
    public int EXP = 0;
    public int Lv = 1;
}

public class PlayerStatus : MonoBehaviour
{
    static public PlayerStatus instance;

    public Text DefeatCount;
    public Text EmeraldCount;
    public Text LvCount;
    public RectTransform ExpBar;
    public GameObject HeartsBar;

    public Sprite FullHp;
    public Sprite HalfHp;
    public Sprite NoHp;

    public AudioClip LvUp;

    private static float MaxPercentEXP = 21.469f;
    private PlayerData PlayerData = new PlayerData();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitStatus();
    }

    private void InitStatus()
    {
        PlayerData.MaxHP = 100 + 10 * ItemStatus.instance.GetAllStatusPhase(3);
        PlayerData.HP = PlayerData.MaxHP;
    }

    public PlayerData GetStatus()
    {
        return PlayerData;
    }

    public void UpdateEmeraldCount(int AddInt)
    {
        PlayerData.Emerald += AddInt;
        EmeraldCount.text = PlayerData.Emerald.ToString();
    }

    public void UpdateDefeatCount()
    {
        PlayerData.Defeat++;
        DefeatCount.text = PlayerData.Defeat.ToString();
    }

    public void UpdateLvCount()
    {
        PlayerData.Lv++;
        LvCount.text = PlayerData.Lv.ToString();
    }

    public void UpdateHpStatus(int GrantHP)
    {
        PlayerData.HP += GrantHP;
        if (PlayerData.HP >= PlayerData.MaxHP) PlayerData.HP = PlayerData.MaxHP;

        if (PlayerData.HP <= 0)
        {

        }
        UpdateHpBar();
    }

    private GameObject GetHeartObject(int id)
    {
        return HeartsBar.transform.Find($"Heart_{id}").transform.Find("Heart").gameObject;
    }

    private Sprite ReturnHeartImage(int OnesPlace, int TensPlace, int i)
    {
        if (TensPlace == 10) return FullHp;
        if (TensPlace != i || OnesPlace == 0) return NoHp;
        return OnesPlace <= 5 ? HalfHp : FullHp;
    }

    private void UpdateHpBar()
    {
        int HpPercentage = 100 * PlayerData.HP / PlayerData.MaxHP;
        int TensPlace = Mathf.FloorToInt(HpPercentage / 10);
        int OnesPlace = HpPercentage % 10;
        for (int i = 9; i >= 1; i--)
        {
            GameObject HeartObject = GetHeartObject(i);
            Image ObjectImage = HeartObject.GetComponent<Image>();
            Sprite sprite = ReturnHeartImage(OnesPlace, TensPlace, i);
            ObjectImage.sprite = sprite;

            if (sprite != NoHp) break;
            if (OnesPlace == 0 && TensPlace == i) break;
        }
    }

    private int NeedExpToNextLv()
    {
        int InitLv = 2;
        int NextLv = PlayerData.Lv + 1;
        int InitEXP = 5;

        while (NextLv > InitLv)
        {
            if (2 <= InitLv && InitLv < 20) InitEXP += 10;
            if (InitLv == 20) InitEXP += 600;
            if (21 <= InitLv && InitLv < 40) InitEXP += 13;
            if (InitLv == 40) InitEXP += 2400;
            if (41 <= InitLv) InitEXP += 16;

            InitLv++;
        }
        return InitEXP;
    }

    public void UpdateExpStatus(int GrantEXP)
    {
        PlayerData.ALLEXP += GrantEXP;
        PlayerData.EXP += GrantEXP;

        bool isLvUP = true;
        while (isLvUP)
        {
            int NeedExp = NeedExpToNextLv();
            ExpBar.anchoredPosition = new Vector3(EXPBarX(NeedExp), 0, 0);
            isLvUP = isPlayerLvUP(NeedExp);
        }
    }

    private float EXPBarX(int NeedExp)
    {
        if (PlayerData.EXP > NeedExp) return 0;

        float Parsentage = (float) PlayerData.EXP / NeedExp;
        return -MaxPercentEXP + Parsentage * MaxPercentEXP;
    }

    private bool isPlayerLvUP(int NeedExp)
    {
        if (PlayerData.EXP < NeedExp) return false;

        Music.instance.SoundEffect(LvUp);
        UpdateLvCount();
        ExpBar.anchoredPosition = new Vector3(-MaxPercentEXP, 0, 0);
        PlayerData.EXP -= NeedExp;
        return true;
    }
}
