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

    public GameObject OptionWeaponBar;
    public GameObject OptionInitWeaponObject;
    public GameObject OptionslotObject;

    public Sprite PowerUp;
    public Sprite UnPowerUp;

    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MobDataBase MobDataBase = Json.instance.MobDataBase;
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
        WeaponData GetStatus = WeaponDataList.Find(s => s.weapon.GetId() == id);
        return GetStatus == null ? 0 : GetStatus.phase;
    }

    public void AddWeaponDataList(Weapon weapon)
    {
        WeaponData WeaponData = new WeaponData
        {
            id = WeaponDataList.Count,
            phase = 1,
            weapon = weapon
        };

        WeaponDataList.Add(WeaponData);

        GameObject Object = GetWeaponObject(WeaponData.id);
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.sprite = weapon.GetIcon();
        ObjectImage.color = new Color(1, 1, 1, 1);
    }

    public void UpdateWeaponPhase(int id)
    {

        WeaponData WeaponData = WeaponDataList.Find(i => i.weapon.GetId() == id);
        WeaponData.phase++;
    }

    private GameObject GetWeaponObject(int id)
    {
        return WeaponBar.transform.Find($"Weapon_{id}").transform.Find("weapon").gameObject;
    }

    // Ç±Ç±Ç©ÇÁêÊÇÕïêäÌÇÃíiäKÇÃèàóù

    public void UpdateOptionWeaponBar()
    {
        if (WeaponDataList.Count == 0) return;

        for (int i = 0; i < WeaponDataList.Count; i++)
        {
            WeaponData WeaponData = WeaponDataList[i];

            if (OptionWeaponBar.transform.Find($"Weapon_{WeaponData.id}") == null)
            {
                CreateBarWeaponObject(WeaponData);
            }
            else
            {
                UpdateBarWeaponObject(WeaponData);
            }
        }
    }

    private void CreateBarWeaponObject(WeaponData WeaponData)
    {
        GameObject Object = Instantiate(OptionInitWeaponObject);
        Object.name = $"Weapon_{WeaponData.id}";
        Object.SetActive(true);
        Object.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);

        GameObject WeaponObject = Object.transform.Find("Weapon").transform.Find("weapon").gameObject;

        Image ObjectImage = WeaponObject.GetComponent<Image>();
        ObjectImage.sprite = WeaponData.weapon.GetIcon();
        ObjectImage.color = new Color(1, 1, 1, 1);

        UpdateSlotObject(WeaponData, Object);

        Object.transform.SetParent(OptionWeaponBar.transform);
    }

    private void UpdateBarWeaponObject(WeaponData WeaponData)
    {
        GameObject Object = OptionWeaponBar.transform.Find($"Weapon_{WeaponData.id}").gameObject;
        UpdateSlotObject(WeaponData, Object);
    }

    private void CreateSlotObject(GameObject SlotListObject, int i)
    {
        GameObject SlotObject = Instantiate(OptionslotObject);
        SlotObject.name = $"slot_{i}";
        SlotObject.transform.localScale = new Vector3(1 / 41.90764f, 1 / 41.90764f, 1 / 41.90764f);

        SlotObject.transform.SetParent(SlotListObject.transform);
    }

    private void UpdateSlotObject(WeaponData WeaponData, GameObject Object)
    {
        GameObject SlotListObject = Object.transform.Find("Phase").transform.Find("SlotList").gameObject;
        for (int i = 1; i < WeaponData.weapon.GetPlayCount(); i++)
        {
            if (SlotListObject.transform.Find($"slot_{i}") == null) CreateSlotObject(SlotListObject, i);

            GameObject SlotObject = SlotListObject.transform.Find($"slot_{i}").gameObject;
            Image SlotObjectImage = SlotObject.GetComponent<Image>();
            SlotObjectImage.sprite = i < WeaponData.phase ? PowerUp : UnPowerUp;
        }
    }
}
