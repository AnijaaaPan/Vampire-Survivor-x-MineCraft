using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    public Text Unlock_Check;
    public Button Submit_button;
    public GameObject Unlock_Cost;
    public Text Unlock_Cost_Text;

    private List<GameObject> ListGameObject = new List<GameObject>();

    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        Unlock_Cost_Text.text = CountUnlockChara(player.Character).ToString();
        Unlock_Cost.SetActive(false);

        foreach (var c in player.Character)
        {
            if (c.hidden == false)
            {
                Mob mob = MobDataBase.FindMobFromId(c.id);
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
                    img.sprite = CharaDescription(player, mob, c);
                };

                chara_object.AddComponent<Button>().onClick.AddListener(() => {
                    for (int i = 0; i < ListGameObject.Count; i++)
                    {
                        ListGameObject[i].GetComponent<Image>().sprite = no_select_chara;
                    };
                    img.sprite = CharaDescription(player, mob, c);
                });

                var image_object = new GameObject($"Image_{mob.GetName()}");
                image_object.transform.localScale = new Vector3(1 / 34.35838f * 0.3f, 1 / 34.35838f * 0.3f, 1 / 34.35838f * 0.3f);
                image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(68.85f, 110);
                image_object.GetComponent<RectTransform>().position = new Vector3(0.305f, -0.1175f, 0);

                Image chara_img = image_object.AddComponent<Image>();
                chara_img.preserveAspect = true;
                chara_img.sprite = mob.GetIcon();

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
                };

                ListGameObject.Add(chara_object);
                image_object.transform.SetParent(chara_object.transform);
                name_object.transform.SetParent(chara_object.transform);
                chara_object.transform.SetParent(parent_object);

                //Debug.Log(c.use);
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

    public Sprite CharaDescription(Json.PlayerData player, Mob mob, Json.CharacterData c)
    {
        Select_Image.preserveAspect = true;
        Select_Image.sprite = mob.GetIcon();
        Select_Name.text = mob.GetName();
        Select_Descrpition.text = mob.GetDescription();

        Select_Image.color = c.use == false ? new Color(0, 0, 0, 255) : new Color(255, 255, 255, 255);
        Unlock_Cost.SetActive(c.use == false);
        Unlock_Check.text = c.use == false ? "アンロック" : "確認";
        return select_chara;
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