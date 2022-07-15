using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public Sprite SelectAchievement;
    public Sprite CheckBackGround;
    public Sprite Check;
    public Sprite Emerald;

    public Text Title;
    public Text SelectDescrpition;
    public Text SelectEffect;

    public Image SelectWeaponItemImage;
    public Image SelectCharaImage;
    public Image SelectMoneyWeaponImage;
    public Image SelectMapImage;

    public GameObject SelectWeaponItemObject;
    public GameObject SelectCharaObject;
    public GameObject SelectMoneyWeaponObject;
    public GameObject SelectMapObject;

    private int UnLockCount = 0;
    private readonly static float InitInt = 1 / 34.36426f;
    private GameObject BeforeGameObject;

    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MobDataBase MobDataBase = Json.instance.MobDataBase;
    private readonly WeaponDataBase WeaponDataBase = Json.instance.WeaponDataBase;
    private readonly ItemDataBase ItemDataBase = Json.instance.ItemDataBase;
    private readonly MapDataBase MapDataBase = Json.instance.MapDataBase;

    void Start()
    {
        foreach (Json.AchievementData a in player.Achievement)
        {
            if (a.use == true) UnLockCount++;

            GameObject AchievementObject = CreateAchievementObject(a);
            GameObject AchievementSelectObject = CreateAchievementSelectObject(a);
            GameObject AchievementCheckBoxObject = CreateAchievementCheckBoxObject(a);
            GameObject AchievementCheckObject = CreateAchievementCheckObject(a);
            GameObject AchievementDescriptionObject = CreateAchievementDescriptionObject(a);
            GameObject AchievementImageObject = CreateAchievementImageObject(a);

            AchievementImageObject.transform.SetParent(AchievementObject.transform);
            if (a.type == "money")
            {
                GameObject AchievementImageMoneyObject = CreateAchievementImageMoneyObject(a);
                AchievementImageMoneyObject.transform.SetParent(AchievementImageObject.transform);
            }

            AchievementCheckObject.transform.SetParent(AchievementCheckBoxObject.transform);
            AchievementCheckBoxObject.transform.SetParent(AchievementObject.transform);
            AchievementDescriptionObject.transform.SetParent(AchievementObject.transform);
            AchievementSelectObject.transform.SetParent(AchievementObject.transform);
            AchievementObject.transform.SetParent(this.gameObject.transform);
        };

        Title.text = $"進行度：{player.Achievement.Count}のうち{UnLockCount}完了";
    }

    private void UpdateSelectImage(GameObject Object)
    {
        Music.instance.ClickSound();

        GameObject BeforeObject = BeforeGameObject.transform.Find($"Select").gameObject;
        Image BeforeObjectImage = BeforeObject.GetComponent<Image>();
        BeforeObjectImage.color = new Color(0, 0, 0, 0);

        BeforeGameObject = Object;
        GameObject AfterObject = Object.transform.Find($"Select").gameObject;
        Image AfterObjectImage = AfterObject.GetComponent<Image>();
        AfterObjectImage.color = new Color(1, 1, 1, 1);
    }

    private void UpdateDescription(Json.AchievementData a)
    {
        SelectWeaponItemObject.SetActive(false);
        SelectCharaObject.SetActive(false);
        SelectMoneyWeaponObject.SetActive(false);
        SelectMapObject.SetActive(false);

        SelectDescrpition.text = a.description;
        SelectEffect.text = a.use == false ? "未達成: " : "達成済み: ";

        if (a.type == "chara")
        {
            SelectCharaObject.SetActive(true);
            Mob mob = MobDataBase.FindMobFromId(a.type_id);
            SelectCharaImage.sprite = mob.GetIcon();
            SelectEffect.text += mob.GetName();
        }
        else if (a.type == "weapon")
        {
            SelectWeaponItemObject.SetActive(true);
            Weapon weapon_data = WeaponDataBase.FindWeaponFromId(a.type_id);
            SelectWeaponItemImage.sprite = weapon_data.GetIcon();
            SelectEffect.text += weapon_data.GetName();

        }
        else if (a.type == "item")
        {
            SelectWeaponItemObject.SetActive(true);
            Item item_data = ItemDataBase.FindItemFromId(a.type_id);
            SelectWeaponItemImage.sprite = item_data.GetIcon();
            SelectEffect.text += item_data.GetName();

        }
        else if (a.type == "money")
        {
            SelectMoneyWeaponObject.SetActive(true);
            Weapon weapon_data = WeaponDataBase.FindWeaponFromId(a.type_id);
            SelectMoneyWeaponImage.sprite = weapon_data.GetIcon();
            SelectEffect.text += $"金貨500枚獲得";

        }
        else if (a.type == "map" || a.type == "hyper")
        {
            SelectMapObject.SetActive(true);
            Map map_data = MapDataBase.FindMapFromId(a.type_id);
            SelectMapImage.sprite = map_data.GetBlockIcon();
            SelectEffect.text += map_data.GetName();
            if (a.type == "hyper") SelectEffect.text += "「ハイパーモード」";
        }
    }

    private GameObject CreateAchievementObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject($"Achievement_Id_{a.id}");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);
        Object.AddComponent<RectTransform>();

        if (a.id == 1)
        {
            BeforeGameObject = Object;
            UpdateDescription(a);
        }

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() => {
            UpdateSelectImage(Object);
            UpdateDescription(a);
        });
        return Object;
    }

    private GameObject CreateAchievementSelectObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject("Select");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(210, 20);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = SelectAchievement;
        if (a.id != 1) ObjectImage.color = new Color(0, 0, 0, 0);

        RectTransform ObjectImageRectTransform = ObjectImage.GetComponent<RectTransform>();
        ObjectImageRectTransform.position = new Vector3(0.01f, 0, 0);
        return Object;
    }

    private GameObject CreateAchievementCheckBoxObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject("Check_Box");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(14, 14);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = CheckBackGround;

        RectTransform ObjectImageRectTransform = ObjectImage.GetComponent<RectTransform>();
        ObjectImageRectTransform.position = new Vector3(-2.7f, 0, 0);
        return Object;
    }

    private GameObject CreateAchievementCheckObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject("Check");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(8, 8);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = Check;
        ObjectImage.color = a.use == false ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);

        RectTransform ObjectImageRectTransform = ObjectImage.GetComponent<RectTransform>();
        ObjectImageRectTransform.position = new Vector3(-2.7f, 0, 0);
        return Object;
    }

    private GameObject CreateAchievementDescriptionObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject("Description");
        Object.transform.localScale = new Vector3(InitInt * 0.1662177f, InitInt * 0.1662177f, InitInt * 0.1662177f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(992, 116);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = a.description;
        ObjectText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        ObjectText.fontSize = 50;
        ObjectText.alignment = TextAnchor.MiddleLeft;
        return Object;
    }

    private GameObject CreateAchievementImageObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(16, 16);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        if (a.type == "chara")
        {
            Mob mob = MobDataBase.FindMobFromId(a.type_id);
            ObjectImage.sprite = mob.GetIcon();
        }
        else if (a.type == "weapon" || a.type == "money")
        {
            Weapon weapon_data = WeaponDataBase.FindWeaponFromId(a.type_id);
            ObjectImage.sprite = weapon_data.GetIcon();
        }
        else if (a.type == "item")
        {
            Item item_data = ItemDataBase.FindItemFromId(a.type_id);
            ObjectImage.sprite = item_data.GetIcon();
        }
        else if (a.type == "map" || a.type == "hyper")
        {
            Map map_data = MapDataBase.FindMapFromId(a.type_id);
            ObjectImage.sprite = map_data.GetBlockIcon();
        }

        RectTransform ObjectImageRectTransform = ObjectImage.GetComponent<RectTransform>();
        ObjectImageRectTransform.position = a.type == "money" ? new Vector3(2.6f, 0, 0) : new Vector3(2.7f, 0, 0);
        return Object;
    }

    private GameObject CreateAchievementImageMoneyObject(Json.AchievementData a)
    {
        GameObject Object = new GameObject("Money_Image");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(16, 16);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = Emerald;

        RectTransform ObjectImageRectTransform = ObjectImage.GetComponent<RectTransform>();
        ObjectImageRectTransform.position = new Vector3(2.7f, 0, 0);
        return Object;
    }
}