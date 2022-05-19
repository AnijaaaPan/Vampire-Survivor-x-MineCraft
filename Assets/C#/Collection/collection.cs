using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// TestScrollScene管理クラス
/// </summary>
public class collection : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private WeaponDataBase WeaponDataBase;//  使用するデータベース

    [SerializeField]
    private ItemDataBase ItemDataBase;//  使用するデータベース

    public Sprite background;
    public Sprite select;
    public Sprite unknown;

    public Text Title;
    public Image Select_Image;
    public Text Select_Name;
    public Text Select_Descrpition;
    public Text Select_Effect;

    private List<GameObject> ListGameObject = new List<GameObject>();

    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        foreach (var w in player.Weapon)
        {
            Weapon weapon = WeaponDataBase.FindWeaponFromId(w.id);

            var weapon_object = new GameObject($"List_{w.id}");
            Image img = weapon_object.AddComponent<Image>();
            img.sprite = background;
            if (w.use == true)
            {
                weapon_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);
            }
            else
            {
                weapon_object.transform.localScale = new Vector3(1 / 34.35838f * 0.85f, 1 / 34.35838f * 0.85f, 1 / 34.35838f * 0.85f);
            };

            var select_image_object = new GameObject($"List_Select_Image_{w.id}");
            select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(27.5f, 27.5f);
            select_image_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);

            Image select_weapon_img = select_image_object.AddComponent<Image>();
            select_weapon_img.preserveAspect = true;
            select_weapon_img.sprite = select;
            select_weapon_img.color = w.id == 1 ? new Color(255, 255, 255, 255) : new Color(0, 0, 0, 0);

            weapon_object.AddComponent<Button>().onClick.AddListener(() => {
                for (int i = 1; i < ListGameObject.Count + 1; i++)
                {
                    ListGameObject[i - 1].GetComponent<Image>().sprite = background;
                    GameObject.Find($"List_Select_Image_{i}").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                };
                img.sprite = weaponDescription(weapon, w);
            });

            var image_object = new GameObject($"List_Image_{w.id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 17.5f);

            Image weapon_img = image_object.AddComponent<Image>();

            weapon_img.color = new Color(255, 255, 255, 255);
            if (w.id == 36)
            {
                weapon_img.color = new Color(255, 0, 0, 255);
            }
            weapon_img.preserveAspect = true;
            if (w.use == true)
            {
                image_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);
                weapon_img.sprite = weapon.GetIcon();
            }
            else
            {
                image_object.transform.localScale = new Vector3(1 / 34.35838f * 0.65f, 1 / 34.35838f * 0.65f, 1 / 34.35838f * 0.65f);
                weapon_img.sprite = unknown;
            };

            ListGameObject.Add(weapon_object);
            image_object.transform.SetParent(weapon_object.transform);
            select_image_object.transform.SetParent(weapon_object.transform);
            weapon_object.transform.SetParent(parent_object);
        };

        foreach (var i in player.Item)
        {
            Item item = ItemDataBase.FindItemFromId(i.id);
            int i_id = i.id + WeaponDataBase.GetWeaponLists().Count;

            var Item_object = new GameObject($"List_{i_id}");
            Image img = Item_object.AddComponent<Image>();
            img.sprite = background;
            if (i.use == true)
            {
                Item_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);
            }
            else
            {
                Item_object.transform.localScale = new Vector3(1 / 34.35838f * 0.85f, 1 / 34.35838f * 0.85f, 1 / 34.35838f * 0.85f);
            };

            var select_image_object = new GameObject($"List_Select_Image_{i_id}");
            select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(27.5f, 27.5f);
            select_image_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);

            Image select_Item_img = select_image_object.AddComponent<Image>();
            select_Item_img.preserveAspect = true;
            select_Item_img.sprite = select;
            select_Item_img.color = new Color(0, 0, 0, 0);

            Item_object.AddComponent<Button>().onClick.AddListener(() => {
                for (int i = 1; i < ListGameObject.Count + 1; i++)
                {
                    ListGameObject[i - 1].GetComponent<Image>().sprite = background;
                    GameObject.Find($"List_Select_Image_{i}").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                };
                img.sprite = ItemDescription(item, i, i_id);
            });

            var image_object = new GameObject($"List_Image_{i_id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 17.5f);

            Image Item_img = image_object.AddComponent<Image>();
            Item_img.preserveAspect = true;
            if (i.use == true)
            {
                image_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);
                Item_img.sprite = item.GetIcon();
            }
            else
            {
                image_object.transform.localScale = new Vector3(1 / 34.35838f * 0.65f, 1 / 34.35838f * 0.65f, 1 / 34.35838f * 0.65f);
                Item_img.sprite = unknown;
            };

            ListGameObject.Add(Item_object);
            image_object.transform.SetParent(Item_object.transform);
            select_image_object.transform.SetParent(Item_object.transform);
            Item_object.transform.SetParent(parent_object);
        }

        GameObject.Find("List_1").GetComponent<Image>().sprite = weaponDescription(WeaponDataBase.FindWeaponFromId(1), player.Weapon[0]);

        int unlock = 0;
        for (int i = 1; i < ListGameObject.Count + 1; i++)
        {
            if (GameObject.Find($"List_Image_{i}").GetComponent<Image>().sprite != unknown)
            {
                unlock++;
            };
        };
        Title.text = $"コレクション：{ListGameObject.Count}のうち{unlock}完了";
    }

    public Sprite weaponDescription(Weapon weapon, Json.WeaponData w)
    {
        GameObject.Find($"List_Select_Image_{w.id}").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        Select_Image.preserveAspect = true;
        if (w.use == true)
        {
            Select_Image.sprite = weapon.GetIcon();
            Select_Name.text = weapon.GetName();
            Select_Descrpition.text = weapon.GetDescription();
            Select_Effect.text = weapon.GetEffect();
            GameObject.Find("Select_Image").GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);
        }
        else
        {
            Select_Image.sprite = unknown;
            Select_Name.text = "???";
            Select_Descrpition.text = "まだ未発見です";
            Select_Effect.text = "";
            GameObject.Find("Select_Image").GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        };

        GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        if (w.id == 36)
        {
            GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(255, 0, 0, 255);
        };
        return background;
    }

    public Sprite ItemDescription(Item item, Json.ItemData i, int i_id)
    {
        GameObject.Find($"List_Select_Image_{i_id}").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        Select_Image.preserveAspect = true;
        Select_Effect.text = "";
        if (i.use == true)
        {
            Select_Image.sprite = item.GetIcon();
            Select_Name.text = item.GetName();
            Select_Descrpition.text = item.GetDescription();
            GameObject.Find("Select_Image").GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);
        }
        else
        {
            Select_Image.sprite = unknown;
            Select_Name.text = "???";
            Select_Descrpition.text = "まだ未発見です";
            GameObject.Find("Select_Image").GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        };

        GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        return background;
    }
}