using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// TestScrollScene管理クラス
/// </summary>
public class chara : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    public Sprite select_chara;
    public Sprite no_select_chara;

    public Image Select_Image;
    public Text Select_Name;
    public Text Select_Descrpition;

    private List<GameObject> ListGameObject = new List<GameObject>();

    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        foreach ( var c in player.Character ) {
            if ( c.hidden == false) {
                Mob mob = MobDataBase.FindMobFromId(c.id);
                //Debug.Log(mob.GetMob());

                var chara_object = new GameObject($"Chara_{mob.GetMob()}");
                chara_object.transform.localScale = new Vector3(1/34.3583755f, 1/34.3583755f, 1/34.3583755f);

                Image img = chara_object.AddComponent<Image>();
                img.maskable = false;
                if (player.Latest_Chara != mob.GetId())
                {
                    img.sprite = no_select_chara;
                } else {
                    img.sprite = CharaDescription(mob);
                };                

                chara_object.AddComponent<Button>();
                chara_object.GetComponent<Button>().onClick.AddListener(() => {
                    for (int i = 0; i < ListGameObject.Count; i++) {
                        ListGameObject[i].GetComponent<Image>().sprite = no_select_chara;
                    };
                    img.sprite = CharaDescription(mob);
                });

                var image_object = new GameObject($"Image_{mob.GetMob()}");
                image_object.transform.localScale = new Vector3(1 / 34.35838f * 0.3f, 1 / 34.35838f * 0.3f, 1 / 34.35838f * 0.3f);
                image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(68.85f, 110);
                image_object.GetComponent<RectTransform>().position = new Vector3(0.305f, -0.1175f, 0);

                Image chara_img = image_object.AddComponent<Image>();
                chara_img.maskable = false;
                chara_img.preserveAspect = true;
                chara_img.sprite = mob.GetIcon();

                var name_object = new GameObject($"Name_{mob.GetMob()}");
                Text name = name_object.AddComponent<Text>();
                name.text = mob.GetMob();
                name.fontSize = 100;
                name.font = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

                name_object.transform.localScale = new Vector3(1 / 34.35838f * 0.09f, 1 / 34.35838f * 0.09f, 1 / 34.35838f * 0.09f);
                name_object.GetComponent<RectTransform>().sizeDelta = new Vector2(444, 115);
                name_object.GetComponent<RectTransform>().position = new Vector3(0.015f, 0.45f, 0);

                ListGameObject.Add(chara_object);
                image_object.transform.SetParent(chara_object.transform);
                name_object.transform.SetParent(chara_object.transform);
                chara_object.transform.SetParent(parent_object);

                //Debug.Log(c.use);
            }
        };
    }

    public Sprite CharaDescription(Mob mob)
    {
        Select_Image.maskable = false;
        Select_Image.preserveAspect = true;
        Select_Image.sprite = mob.GetIcon();
        Select_Name.text = mob.GetMob();
        return select_chara;
    }
}