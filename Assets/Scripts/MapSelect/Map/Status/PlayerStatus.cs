using UnityEngine;

public class PlayerData
{
    public int MaxHP;
    public int HP;
    public int Emerald;
    public int Defeat;
    public int EXP;
    public int Lv;
}

public class PlayerStatus : MonoBehaviour
{
    static public PlayerStatus instance;

    public GameObject Chara;
    public GameObject DefeatCount;
    public GameObject EmeraldCount;
    public GameObject ExpBar;
    public GameObject HeartsBar;

    private Json.PlayerData player = Json.instance.Load();
    private PlayerData PlayerData = new PlayerData();

    private void Awake()
    {
        instance = this;
    }

    public void InitStatus()
    {
        PlayerData = new PlayerData();
        PlayerData.MaxHP = 100 * ItemStatus.instance.GetAllStatusPhase(3);
        PlayerData.HP = PlayerData.MaxHP;
        PlayerData.Emerald = 0;
        PlayerData.Defeat = 0;
        PlayerData.EXP = 0;
        PlayerData.Lv = 0;
    }

    public PlayerData GetStatus()
    {
        return PlayerData;
    }

    public void UpdateHpStatus(int GrantHP)
    {
        PlayerData.HP += GrantHP;
        if (PlayerData.HP >= PlayerData.MaxHP) PlayerData.HP = PlayerData.MaxHP;

        if (PlayerData.HP <= 0)
        {

        }
    }
}
