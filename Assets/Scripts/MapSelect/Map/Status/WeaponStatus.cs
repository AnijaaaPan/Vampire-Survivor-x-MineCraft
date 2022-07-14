using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponData
{
    public int id;
    public Weapon weapon;
    public int phase;
}

public class WeaponStatus : MonoBehaviour
{
    static public WeaponStatus instance;

    public GameObject WeaponBar;
    public GameObject InitWeaponObject;

    private Json.PlayerData player = Json.instance.Load();
    private MobDataBase MobDataBase = Json.instance.MobDataBase;
    private List<WeaponData> WeaponDataList = new List<WeaponData>();

    private Mob mob;

    private void Awake()
    {
        instance = this;
        mob = MobDataBase.FindMobFromId(player.Latest_Chara);
    }

    void Start()
    {
        AddWeaponDataList(mob.GetWeapon());
    }

    public List<WeaponData> GetStatusList()
    {
        return WeaponDataList;
    }

    public int GetStatusPhase(int id)
    {
        WeaponData GetStatus = WeaponDataList.Find(s => s.id == id);
        return GetStatus == null ? 0 : GetStatus.phase;
    }

    public void AddWeaponDataList(Weapon weapon)
    {
        WeaponData WeaponData = new WeaponData();
        WeaponData.id = WeaponDataList.Count;
        WeaponData.phase = 1;
        WeaponData.weapon = weapon;

        WeaponDataList.Add(WeaponData);

        GameObject Object = GetWeaponObject(WeaponData.id);
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = weapon.GetIcon();
        ObjectImage.color = new Color(1, 1, 1, 1);
    }

    public void UpdateWeaponPhase(int id)
    {

        WeaponData WeaponData = WeaponDataList.Find(i => i.id == id);
        WeaponData.phase++;
    }

    private GameObject GetWeaponObject(int id)
    {
        return WeaponBar.transform.Find($"Weapon_{id}").transform.Find("weapon").gameObject;
    }
}
