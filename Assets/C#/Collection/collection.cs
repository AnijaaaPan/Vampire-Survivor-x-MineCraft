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

            var weapon_object = new GameObject($"Weapon_{w.id}");
            Image img = weapon_object.AddComponent<Image>();
            img.maskable = false;
            img.sprite = background;
            if (w.use == true)
            {
                weapon_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);
            }
            else
            {
                weapon_object.transform.localScale = new Vector3(1 / 34.35838f * 0.85f, 1 / 34.35838f * 0.85f, 1 / 34.35838f * 0.85f);
            };

            var select_image_object = new GameObject($"Select_Image_{w.id}");
            select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(27.5f, 27.5f);
            select_image_object.transform.localScale = new Vector3(1 / 34.35838f, 1 / 34.35838f, 1 / 34.35838f);

            Image select_weapon_img = select_image_object.AddComponent<Image>();
            select_weapon_img.maskable = false;
            select_weapon_img.preserveAspect = true;
            select_weapon_img.sprite = select;
            select_weapon_img.color = w.id == 1 ? new Color(255, 255, 255, 255) : new Color(0, 0, 0, 0);

            weapon_object.AddComponent<Button>().onClick.AddListener(() => {
                for (int i = 0; i < ListGameObject.Count; i++)
                {
                    ListGameObject[i].GetComponent<Image>().sprite = background;
                    GameObject.Find($"Select_Image_{i+1}").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                };
                img.sprite = weaponDescription(weapon, w);
            });

            var image_object = new GameObject($"Image_{w.id}");
            image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(17.5f, 17.5f);

            Image weapon_img = image_object.AddComponent<Image>();

            weapon_img.color = new Color(255, 255, 255, 255);
            if ( w.id == 36 )
            {
                weapon_img.color = new Color(255, 0, 0, 255);
            }
            weapon_img.maskable = false;
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

        GameObject.Find("Weapon_1").GetComponent<Image>().sprite = weaponDescription(WeaponDataBase.FindWeaponFromId(1), player.Weapon[0]);

        int unlock = 0;
        for (int i = 0; i < ListGameObject.Count; i++)
        {
            if (GameObject.Find($"Image_{i+1}").GetComponent<Image>().sprite != unknown)
            {
                unlock++;
            };
        };
        Title.text = $"コレクション：{ListGameObject.Count}のうち{unlock}完了";
    }

    public Sprite weaponDescription(Weapon weapon, Json.WeaponData w)
    {
        GameObject.Find($"Select_Image_{w.id}").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        Select_Image.maskable = false;
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
}