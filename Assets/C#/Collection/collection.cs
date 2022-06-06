using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collection : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private WeaponDataBase WeaponDataBase;//  使用するデータベース

    [SerializeField]
    private ItemDataBase ItemDataBase;//  使用するデータベース

    [SerializeField]
    private SpecialItemDataBase SpecialItemDataBase;//  使用するデータベース

    public Sprite background;
    public Sprite specialitembackground;
    public Sprite select;
    public Sprite unknown;

    public Text Title;
    public Image Select_Image;
    public Text Select_Name;
    public Text Select_Descrpition;
    public Text Select_Effect;

    private List<GameObject> ListGameObject = new List<GameObject>();

    private float init_int = 1 / 34.35838f;
    void Start()
    {
        int unlock = 0;
        int check_item_id = 1;

        Json.PlayerData player = Json.instance.Load();
        foreach (var w in player.Weapon)
        {
            Weapon weapon = WeaponDataBase.FindWeaponFromId(w.id);

            var weapon_object = new GameObject($"List_{w.id}");
            Image img = weapon_object.AddComponent<Image>();
            img.sprite = background;
            if (w.use == true)
            {
                weapon_object.transform.localScale = new Vector3(init_int, init_int, init_int);
            }
            else
            {
                weapon_object.transform.localScale = new Vector3(init_int * 0.85f, init_int * 0.85f, init_int * 0.85f);
            };

            var select_image_object = new GameObject($"List_Select_Image_{w.id}");
            select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(27.5f, 27.5f);
            select_image_object.transform.localScale = new Vector3(init_int, init_int, init_int);

            Image select_weapon_img = select_image_object.AddComponent<Image>();
            select_weapon_img.preserveAspect = true;
            select_weapon_img.sprite = select;
            select_weapon_img.color = w.id == 1 ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);

            weapon_object.AddComponent<Button>().onClick.AddListener(() => {
                GameObject.Find($"List_Select_Image_{check_item_id}").GetComponent<Image>().color = new Color(0, 0, 0, 0);

                check_item_id = w.id;
                img.sprite = weaponDescription(weapon, w);
            });

            var image_object = new GameObject($"List_Image_{w.id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 17.5f);

            Image weapon_img = image_object.AddComponent<Image>();

            weapon_img.color = new Color(1, 1, 1, 1);
            if (w.use == true && w.id == 36)
            {
                weapon_img.color = new Color(1, 0, 0, 1);
            }
            weapon_img.preserveAspect = true;
            if (w.use == true)
            {
                unlock++;
                image_object.transform.localScale = new Vector3(init_int, init_int, init_int);
                weapon_img.sprite = weapon.GetIcon();
            }
            else
            {
                image_object.transform.localScale = new Vector3(init_int * 0.65f, init_int * 0.65f, init_int * 0.65f);
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
            int i_id = ListGameObject.Count + 1;

            var Item_object = new GameObject($"List_{i_id}");
            Image img = Item_object.AddComponent<Image>();
            img.sprite = background;
            if (i.use == true)
            {
                Item_object.transform.localScale = new Vector3(init_int, init_int, init_int);
            }
            else
            {
                Item_object.transform.localScale = new Vector3(init_int * 0.85f, init_int * 0.85f, init_int * 0.85f);
            };

            var select_image_object = new GameObject($"List_Select_Image_{i_id}");
            select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(27.5f, 27.5f);
            select_image_object.transform.localScale = new Vector3(init_int, init_int, init_int);

            Image select_Item_img = select_image_object.AddComponent<Image>();
            select_Item_img.preserveAspect = true;
            select_Item_img.sprite = select;
            select_Item_img.color = new Color(0, 0, 0, 0);

            Item_object.AddComponent<Button>().onClick.AddListener(() => {
                GameObject.Find($"List_Select_Image_{check_item_id}").GetComponent<Image>().color = new Color(0, 0, 0, 0);

                check_item_id = i_id;
                img.sprite = ItemDescription(item, i, i_id);
            });

            var image_object = new GameObject($"List_Image_{i_id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 17.5f);

            Image Item_img = image_object.AddComponent<Image>();
            Item_img.preserveAspect = true;
            if (i.use == true)
            {
                unlock++;
                image_object.transform.localScale = new Vector3(init_int, init_int, init_int);
                Item_img.sprite = item.GetIcon();
            }
            else
            {
                image_object.transform.localScale = new Vector3(init_int * 0.65f, init_int * 0.65f, init_int * 0.65f);
                Item_img.sprite = unknown;
            };

            ListGameObject.Add(Item_object);
            image_object.transform.SetParent(Item_object.transform);
            select_image_object.transform.SetParent(Item_object.transform);
            Item_object.transform.SetParent(parent_object);
        }

        foreach (var i in player.SpecialItem)
        {
            SpecialItem item = SpecialItemDataBase.FindSpecialItemFromId(i.id);
            int i_id = ListGameObject.Count + 1;

            var SpecialItem_object = new GameObject($"List_{i_id}");
            Image img = SpecialItem_object.AddComponent<Image>();
            img.sprite = specialitembackground;
            if (i.use == true)
            {
                SpecialItem_object.transform.localScale = new Vector3(init_int, init_int, init_int);
            }
            else
            {
                SpecialItem_object.transform.localScale = new Vector3(init_int * 0.85f, init_int * 0.85f, init_int * 0.85f);
            };

            var select_image_object = new GameObject($"List_Select_Image_{i_id}");
            select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(27.5f, 27.5f);
            select_image_object.transform.localScale = new Vector3(init_int, init_int, init_int);

            Image select_SpecialItem_img = select_image_object.AddComponent<Image>();
            select_SpecialItem_img.preserveAspect = true;
            select_SpecialItem_img.sprite = select;
            select_SpecialItem_img.color = new Color(0, 0, 0, 0);

            SpecialItem_object.AddComponent<Button>().onClick.AddListener(() => {
                GameObject.Find($"List_Select_Image_{check_item_id}").GetComponent<Image>().color = new Color(0, 0, 0, 0);

                check_item_id = i_id;
                img.sprite = SpecialItemDescription(item, i, i_id);
            });

            var image_object = new GameObject($"List_Image_{i_id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 17.5f);

            Image SpecialItem_img = image_object.AddComponent<Image>();
            SpecialItem_img.preserveAspect = true;
            if (i.use == true)
            {
                image_object.transform.localScale = new Vector3(init_int, init_int, init_int);
                SpecialItem_img.sprite = item.GetIcon();
            }
            else
            {
                image_object.transform.localScale = new Vector3(init_int * 0.65f, init_int * 0.65f, init_int * 0.65f);
                SpecialItem_img.sprite = unknown;
            };

            ListGameObject.Add(SpecialItem_object);
            image_object.transform.SetParent(SpecialItem_object.transform);
            select_image_object.transform.SetParent(SpecialItem_object.transform);
            SpecialItem_object.transform.SetParent(parent_object);
        }

        GameObject.Find("List_1").GetComponent<Image>().sprite = weaponDescription(WeaponDataBase.FindWeaponFromId(1), player.Weapon[0]);
        Title.text = $"コレクション：{ListGameObject.Count}のうち{unlock}完了";
    }

    public Sprite weaponDescription(Weapon weapon, Json.WeaponData w)
    {
        GameObject.Find($"List_Select_Image_{w.id}").GetComponent<Image>().color = new Color(1, 1, 1, 1);
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

        GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (w.use == true && w.id == 36)
        {
            GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(1, 0, 0, 1);
        };
        return background;
    }

    public Sprite ItemDescription(Item item, Json.ItemData i, int i_id)
    {
        GameObject.Find($"List_Select_Image_{i_id}").GetComponent<Image>().color = new Color(1, 1, 1, 1);
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

        GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        return background;
    }
    public Sprite SpecialItemDescription(SpecialItem item, Json.SpecialItemData i, int i_id)
    {
        GameObject.Find($"List_Select_Image_{i_id}").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Select_Image.preserveAspect = true;
        Select_Effect.text = "";
        if (i.use == true)
        {
            Select_Image.sprite = item.GetIcon();
            Select_Name.text = item.GetName();
            Select_Descrpition.text = item.GetDescription();
            Select_Effect.text = item.GetEffect();
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

        GameObject.Find($"Select_Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        return specialitembackground;
    }
}