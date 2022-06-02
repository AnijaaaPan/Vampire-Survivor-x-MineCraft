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

    public Sprite check_background;
    public Sprite check;

    // Start is called before the first frame update
    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        foreach (var a in player.Achievement)
        {
            var achievement_object = new GameObject($"Achievement_Id_{a.id}");
            achievement_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);
            achievement_object.AddComponent<RectTransform>();


            var check_box_object = new GameObject($"Check_Box_{a.id}");
            check_box_object.AddComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
            check_box_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);

            Image check_box_img = check_box_object.AddComponent<Image>();
            check_box_img.preserveAspect = true;
            check_box_img.sprite = check_background;
            check_box_img.GetComponent<RectTransform>().position = new Vector3(-2.8f, 0, 0);


            var check_object = new GameObject($"Check_{a.id}");
            check_object.AddComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
            check_object.transform.localScale = new Vector3(1 / 34.36426f, 1 / 34.36426f, 1 / 34.36426f);

            Image check_img = check_object.AddComponent<Image>();
            check_img.preserveAspect = true;
            check_img.sprite = check;
            check_img.color = a.use == false ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
            check_img.GetComponent<RectTransform>().position = new Vector3(-2.8f, 0, 0);

            check_object.transform.SetParent(check_box_object.transform);
            check_box_object.transform.SetParent(achievement_object.transform);
            achievement_object.transform.SetParent(parent_object);
        };
    }
}
