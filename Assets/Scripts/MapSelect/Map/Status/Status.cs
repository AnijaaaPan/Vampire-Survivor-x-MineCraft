using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WeaponStatus
{
    public int ID;
    public Weapon weapon;
    public int phase;
}

[System.Serializable]
public class ItemStatus
{
    public int ID;
    public Item item;
    public int phase;
}

[System.Serializable]
public class EnemyStatus
{
    public int ID;
    public int HP;
    public Enemy enemy;
    public GameObject Object;
}

public class GameStatus : MonoBehaviour
{
    static public GameStatus instance;

    public GameObject Chara;
    public GameObject SpawnEnemy;
    public GameObject WeaponBar;
    public GameObject ItemBar;
    public GameObject DefeatCount;
    public GameObject EmeraldCount;
    public GameObject ExpBar;
    public GameObject HeartsBar;

    private PlayerStatus PlayerStatus = new PlayerStatus();
    private List<WeaponStatus> WeaponStatusList = new List<WeaponStatus>();
    private List<ItemStatus> ItemStatusList = new List<ItemStatus>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void InitPlayerStatus(int HP, GameObject Object)
    {
        PlayerStatus = new PlayerStatus();
        PlayerStatus.HP = HP;
        PlayerStatus.Emerald = 0;
        PlayerStatus.Defeat = 0;
        PlayerStatus.EXP = 0;
        PlayerStatus.Lv = 0;
    }

    public PlayerStatus GetPlayerStatus()
    {
        return PlayerStatus;
    }

    public void UpdatePlayerHpStatus(int GrantHP)
    {
        PlayerStatus.HP += GrantHP;
        if (GrantHP < 0)
        {

        }

        if (PlayerStatus.HP <= 0)
        {

        }
    }
}
