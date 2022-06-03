using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class achievement : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    [SerializeField]
    private WeaponDataBase WeaponDataBase;//  使用するデータベース

    [SerializeField]
    private ItemDataBase ItemDataBase;//  使用するデータベース

    [SerializeField]
    private SpecialItemDataBase SpecialItemDataBase;//  使用するデータベース

    public Sprite select_achievement;
    public Sprite check_background;
    public Sprite check;
    public Sprite money;

    public Image Select_Weapon_Item_Image;
    public Image Select_Chara_Image;
    public Image Select_Monery_Weapon_Image;
    public GameObject Select_Weapon_Item_Object;
    public GameObject Select_Chara_Object;
    public GameObject Select_Monery_Weapon_Object;
    public Text Select_Descrpition;
    public Text Select_Effect;

    // Start is called before the first frame update
    void Start()
    {
        var check_item_id = 1;

        Json.PlayerData player = Json.instance.Load();
        foreach (var a in player.Achievement)
        {
            var achievement_object = new GameObject($"Achievement_Id_{a.id}");
            achievement_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);
            achievement_object.AddComponent<RectTransform>();

            var achievement_select_object = new GameObject($"Achievement_Select_Id_{a.id}");
            achievement_select_object.AddComponent<RectTransform>().sizeDelta = new Vector2(210, 20);
            achievement_select_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);
            achievement_select_object.AddComponent<RectTransform>();

            Image achievement_select_img = achievement_select_object.AddComponent<Image>();
            achievement_select_img.sprite = select_achievement;
            achievement_select_img.GetComponent<RectTransform>().position = new Vector3(0.01f, 0, 0);
            if ( a.id != 1 )
            {
                achievement_select_img.color = new Color(0, 0, 0, 0);
            } else
            {
                achievementDescription(a);
            }

            achievement_object.AddComponent<Button>().onClick.AddListener(() => {
                GameObject.Find($"Achievement_Select_Id_{check_item_id}").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                check_item_id = a.id;
                achievement_select_img.color = new Color(1, 1, 1, 1);
                achievementDescription(a);
            });

            var check_box_object = new GameObject($"Check_Box_{a.id}");
            check_box_object.AddComponent<RectTransform>().sizeDelta = new Vector2(14, 14);
            check_box_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);

            Image check_box_img = check_box_object.AddComponent<Image>();
            check_box_img.preserveAspect = true;
            check_box_img.sprite = check_background;
            check_box_img.GetComponent<RectTransform>().position = new Vector3(-2.7f, 0, 0);

            var check_object = new GameObject($"Check_{a.id}");
            check_object.AddComponent<RectTransform>().sizeDelta = new Vector2(8, 8);
            check_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);

            Image check_img = check_object.AddComponent<Image>();
            check_img.preserveAspect = true;
            check_img.sprite = check;
            check_img.color = a.use == false ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
            check_img.GetComponent<RectTransform>().position = new Vector3(-2.7f, 0, 0);

            var description_object = new GameObject($"Description_{a.id}");
            Text descript = description_object.AddComponent<Text>();
            descript.text = a.description;
            descript.fontSize = 50;
            descript.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            descript.alignment = TextAnchor.MiddleLeft;
            description_object.GetComponent<RectTransform>().sizeDelta = new Vector2(992, 116);
            description_object.transform.localScale = new Vector3(1 / 34.36426f * 0.1662177f, 1 / 34.36426f * 0.1662177f, 1 / 34.36426f * 0.1662177f);

            var image_object = new GameObject($"Image_{a.id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
            image_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);

            Image img = image_object.AddComponent<Image>();
            img.preserveAspect = true;
            img.GetComponent<RectTransform>().position = new Vector3(2.7f, 0, 0);
            if (a.type == "chara")
            {
                Mob mob = MobDataBase.FindMobFromId(a.type_id);
                img.sprite = mob.GetIcon();

            } else if (a.type == "weapon" || a.type == "money")
            {
                Weapon weapon_data = WeaponDataBase.FindWeaponFromId(a.type_id);
                img.sprite = weapon_data.GetIcon();
                
            } else if (a.type == "item")
            {
                Item item_data = ItemDataBase.FindItemFromId(a.type_id);
                img.sprite = item_data.GetIcon();

            }            

            image_object.transform.SetParent(achievement_object.transform);
            if (a.type == "money")
            {
                img.GetComponent<RectTransform>().position = new Vector3(2.6f, 0, 0);

                var money_image_object = new GameObject($"Money_Image_{a.id}");
                money_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
                money_image_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);

                Image money_img = money_image_object.AddComponent<Image>();
                money_img.preserveAspect = true;
                money_img.sprite = money;
                money_img.GetComponent<RectTransform>().position = new Vector3(2.7f, 0, 0);
                money_image_object.transform.SetParent(achievement_object.transform);
            }

            check_object.transform.SetParent(check_box_object.transform);
            check_box_object.transform.SetParent(achievement_object.transform);
            description_object.transform.SetParent(achievement_object.transform);
            achievement_select_object.transform.SetParent(achievement_object.transform);
            achievement_object.transform.SetParent(parent_object);
        };
    }
    
    public void achievementDescription(Json.AchievementData a)
    {
        Select_Weapon_Item_Object.SetActive(false);
        Select_Chara_Object.SetActive(false);
        Select_Monery_Weapon_Object.SetActive(false);

        Select_Descrpition.text = a.description;

        Select_Effect.text = a.use == false ? "未達成: " : "達成済み: ";

        if (a.type == "chara")
        {
            Select_Chara_Object.SetActive(true);
            Mob mob = MobDataBase.FindMobFromId(a.type_id);
            Select_Chara_Image.sprite = mob.GetIcon();
            Select_Chara_Image.preserveAspect = true;
            Select_Effect.text += mob.GetName();
        }
        else if (a.type == "weapon")
        {
            Select_Weapon_Item_Object.SetActive(true);
            Weapon weapon_data = WeaponDataBase.FindWeaponFromId(a.type_id);
            Select_Weapon_Item_Image.sprite = weapon_data.GetIcon();
            Select_Weapon_Item_Image.preserveAspect = true;
            Select_Effect.text += weapon_data.GetName();

        }
        else if (a.type == "item")
        {
            Select_Weapon_Item_Object.SetActive(true);
            Item item_data = ItemDataBase.FindItemFromId(a.type_id);
            Select_Weapon_Item_Image.sprite = item_data.GetIcon();
            Select_Weapon_Item_Image.preserveAspect = true;
            Select_Effect.text += item_data.GetName();

        } else if (a.type == "money")
        {
            Select_Monery_Weapon_Object.SetActive(true);
            Weapon weapon_data = WeaponDataBase.FindWeaponFromId(a.type_id);
            Select_Monery_Weapon_Image.sprite = weapon_data.GetIcon();
            Select_Monery_Weapon_Image.preserveAspect = true;
            Select_Effect.text += $"金貨500枚獲得";

        }
    }
}
