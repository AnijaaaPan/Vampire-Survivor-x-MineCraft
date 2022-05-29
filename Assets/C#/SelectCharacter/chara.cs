using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class chara : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    [SerializeField]
    private WeaponDataBase WeaponDataBase;//  使用するデータベース

    [SerializeField]
    private ItemDataBase ItemDataBase;//  使用するデータベース

    public Sprite select_chara;
    public Sprite no_select_chara;
    public Sprite unlock_chara_weapon;

    public Image Select_Image;
    public Image Select_Weapon;
    public Text Select_Name;
    public Text Select_Descrpition;
    public Text Unlock_Check;
    public Button Submit_button;
    public GameObject Unlock_Cost;
    public Text Unlock_Cost_Text;

    public GameObject Effect_list;

    private List<GameObject> ListGameObject = new List<GameObject>();

    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        Unlock_Cost_Text.text = CountUnlockChara(player.Character).ToString();
        Unlock_Cost.SetActive(false);

        List<int> ParameterList = new List<int> { 3, 4, 2, 10, 1, 7, 8, 6, 5, 9, 16, 11, 12, 13, 14, 15};
        for (int i = 0; i < ItemDataBase.GetItemLists().Count; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Item item_data = ItemDataBase.FindItemFromId(ParameterList[i]);

            var item_object = new GameObject($"Item_id_{item_data.GetId()}");
            item_object.transform.localScale = new Vector3(1 / 1.625f, 1 / 1.625f, 1 / 1.625f);
            item_object.AddComponent<RectTransform>();

            var item_image_object = new GameObject($"Image_{item_data.GetId()}");
            item_image_object.transform.localScale = new Vector3(1 / 1.625f * 1.2f, 1 / 1.625f * 1.2f, 1 / 1.625f * 1.2f);
            item_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            item_image_object.GetComponent<RectTransform>().position = new Vector3(-1.425f, 0, 0);
            Image img = item_image_object.AddComponent<Image>();
            img.sprite = item_data.GetIcon();

            var effect_name_object = new GameObject($"Effect_Name_{item_data.GetId()}");
            Text name = effect_name_object.AddComponent<Text>();
            name.text = item_data.GetName();
            name.fontSize = 100;
            name.alignment = TextAnchor.MiddleLeft;
            name.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            effect_name_object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);
            effect_name_object.GetComponent<RectTransform>().sizeDelta = new Vector2(790.6436f, 147.25f);
            effect_name_object.GetComponent<RectTransform>().position = new Vector3(-0.175f, 0, 0);

            var effect_parameter_object = new GameObject($"Effect_Parameter_{item_data.GetId()}");
            Text parameter = effect_parameter_object.AddComponent<Text>();
            parameter.text = "";
            parameter.fontSize = 100;
            parameter.alignment = TextAnchor.MiddleRight;
            parameter.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            effect_parameter_object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);
            effect_parameter_object.GetComponent<RectTransform>().sizeDelta = new Vector2(318.66f, 147.25f);
            effect_parameter_object.GetComponent<RectTransform>().position = new Vector3(1.2225f, 0, 0);

            item_image_object.transform.SetParent(item_object.transform);
            effect_name_object.transform.SetParent(item_object.transform);
            effect_parameter_object.transform.SetParent(item_object.transform);
            item_object.transform.SetParent(Effect_list.transform);
        };

        foreach (var c in player.Character)
        {
            if (c.hidden == false)
            {
                Mob mob = MobDataBase.FindMobFromId(c.id);
                Weapon weapon = WeaponDataBase.FindWeaponFromId(mob.GetWeaponId());
                //Debug.Log(mob.GetName());

                var chara_object = new GameObject($"Chara_{mob.GetName()}");
                chara_object.transform.localScale = new Vector3(1 / 34.3583755f, 1 / 34.3583755f, 1 / 34.3583755f);

                Image img = chara_object.AddComponent<Image>();
                if (player.Latest_Chara != mob.GetId())
                {
                    img.sprite = no_select_chara;
                }
                else
                {
                    img.sprite = CharaDescription(mob, c, weapon, player);
                };

                chara_object.AddComponent<Button>().onClick.AddListener(() => {
                    for (int i = 0; i < ListGameObject.Count; i++)
                    {
                        ListGameObject[i].GetComponent<Image>().sprite = no_select_chara;
                    };
                    img.sprite = CharaDescription(mob, c, weapon, player);
                });

                var image_object = new GameObject($"Image_{mob.GetName()}");
                image_object.transform.localScale = new Vector3(1 / 34.35838f * 0.3f, 1 / 34.35838f * 0.3f, 1 / 34.35838f * 0.3f);
                image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(68.85f, 110);
                image_object.GetComponent<RectTransform>().position = new Vector3(0.305f, -0.1175f, 0);

                Image chara_img = image_object.AddComponent<Image>();
                chara_img.preserveAspect = true;
                chara_img.sprite = mob.GetIcon();

                var weapon_image_object = new GameObject($"Weapon_Image_{mob.GetName()}");
                weapon_image_object.transform.localScale = new Vector3(1 / 34.35838f * 0.25f, 1 / 34.35838f * 0.25f, 1 / 34.35838f * 0.25f);
                weapon_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(65, 65);
                weapon_image_object.GetComponent<RectTransform>().position = new Vector3(-0.23f, -0.2175f, 0);

                Image weapon_img = weapon_image_object.AddComponent<Image>();
                weapon_img.preserveAspect = true;
                weapon_img.sprite = weapon.GetIcon();

                var name_object = new GameObject($"Name_{mob.GetName()}");
                Text name = name_object.AddComponent<Text>();
                name.text = mob.GetName();
                name.fontSize = 100;
                name.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

                name_object.transform.localScale = new Vector3(1 / 34.35838f * 0.09f, 1 / 34.35838f * 0.09f, 1 / 34.35838f * 0.09f);
                name_object.GetComponent<RectTransform>().sizeDelta = new Vector2(444, 115);
                name_object.GetComponent<RectTransform>().position = new Vector3(0.015f, 0.45f, 0);

                if (c.use == false)
                {
                    name.color = new Color(0, 0, 0, 255);
                    chara_img.color = new Color(0, 0, 0, 255);
                    weapon_img.color = new Color(0, 0, 0, 255);
                };

                ListGameObject.Add(chara_object);
                image_object.transform.SetParent(chara_object.transform);
                weapon_image_object.transform.SetParent(chara_object.transform);
                name_object.transform.SetParent(chara_object.transform);
                chara_object.transform.SetParent(parent_object);
            };
        };

        Submit_button.onClick.AddListener(() => {
            Mob mob = MobDataBase.FindMobFromName(Select_Name.text);
            var c = player.Character[mob.GetId()];
            if (c.use == false && CountUnlockChara(player.Character) <= player.Money)
            {
                player.Money -= CountUnlockChara(player.Character);
                player.Latest_Chara = mob.GetId();
                c.use = true;
                Json.instance.Save(player);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (c.use == true) { 
            };
        });
    }

    public Sprite CharaDescription(Mob mob, Json.CharacterData c, Weapon weapon, Json.PlayerData player)
    {
        initParameter(mob, player);
        Select_Image.preserveAspect = true;
        Select_Image.sprite = mob.GetIcon();
        Select_Name.text = mob.GetName();
        Select_Descrpition.text = mob.GetDescription();

        Select_Weapon.sprite = c.use == true ? weapon.GetIcon() : unlock_chara_weapon;
        Select_Image.color = c.use == false ? new Color(0, 0, 0, 255) : new Color(255, 255, 255, 255);
        Unlock_Cost.SetActive(c.use == false);
        Unlock_Check.text = c.use == false ? "アンロック" : "確認";
        return select_chara;
    }

    public string math_result(float result)
    {
        if (result < 0)
        {
            return $"{result}";
        }
        return $"+{result}";
    }

    public string result_effect(Item item_data, Mob mob, Json.PlayerData player)
    {
        var data = mob.GetParameter().Find(par => item_data.GetId() == par.effect_id);
        Json.PowerUpList poweroup_data = player.PowerUp.poweruplist.Find(data => data.id == item_data.GetId());
        int powerupcount = (int)(data == null ? 0 : data.phase) + item_data.GetCount() - poweroup_data.powerupcount;

        if (powerupcount == 0)
        {
            if (item_data.GetId() == 3)
            {
                return $"100";
            }
            return " - ";
        }

        if (new List<int> { 1, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 }.Contains(item_data.GetId()))
        {
            return $"{math_result(powerupcount * 10)}%";
        }
        else if (new List<int> { 2, 9, 16 }.Contains(item_data.GetId()))
        {
            return math_result(powerupcount);
        }
        else if (item_data.GetId() == 3)
        {
            return $"{100 + powerupcount * 10}";
        }
        else
        {
            return $"{powerupcount * 0.1}";
        }
    }
    public void initParameter(Mob mob, Json.PlayerData player)
    {
        for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Item item_data = ItemDataBase.FindItemFromId(i);
            GameObject get_object = GameObject.Find($"Effect_Parameter_{item_data.GetId()}");
            string effect_parameter = result_effect(item_data, mob, player);
            get_object.GetComponent<Text>().text = effect_parameter;
        }
    }

    public int CountUnlockChara(List<Json.CharacterData> Characters)
    {
        var count = -1;
        foreach (var c in Characters)
        {
            if (c.use == true)
            {
                count++;
            };
        };
        return 500 + count*50;
    }
}