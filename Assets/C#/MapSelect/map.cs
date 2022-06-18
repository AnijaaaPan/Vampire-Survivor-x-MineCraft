using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class map : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private MapDataBase MapDataBase;//  使用するデータベース

    public GameObject MoveSpeed;
    public GameObject EmeraldBonus;
    public GameObject LuckBonus;

    public Sprite select_map;

    public GameObject Hyper;
    public Image Check;
    public Button Submit_button;
    public Button HyperButton;

    private int select_map_id;
    private bool select_map_hyper_mode;

    private float init_int = 1 / 34.35838f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(chara.select_chara_id);
        Json.PlayerData player = Json.instance.Load();
        select_map_id = player.Latest_Map;
        select_map_hyper_mode = player.Latest_Map_Hyper;

        if (player.Latest_Map_Hyper == false)
        {
            Check.color = new Color(0, 0, 0, 0);
        }

        foreach (var m in player.Map)
        {
            if (m.use == false) continue;

            Map map = MapDataBase.FindMapFromId(m.id);

            var map_object = new GameObject($"Map_{m.id}");
            map_object.AddComponent<RectTransform>();
            map_object.transform.localScale = new Vector3(init_int, init_int, init_int);

            Image map_background_img = map_object.AddComponent<Image>();
            map_background_img.color = new Color(0, 0, 0, 0);
            
            map_object.AddComponent<Button>().onClick.AddListener(() => {
                GameObject.Find($"Map_{select_map_id}").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                GameObject.Find($"Map_Select_Image_{select_map_id}").GetComponent<Image>().color = new Color(0, 0, 0, 0);

                select_map_id = m.id;
                MapDescription(map, m);
            });

            var map_image_object = new GameObject($"Map_Image_{m.id}");
            map_image_object.transform.localScale = new Vector3(init_int, init_int, init_int);
            map_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(70, 45);
            map_image_object.GetComponent<RectTransform>().position = new Vector3(-init_int * 70, 0, 0);

            Image map_img = map_image_object.AddComponent<Image>();

            var map_name_background_object = new GameObject($"Map_Name_Background_{m.id}");
            map_name_background_object.transform.localScale = new Vector3(init_int, init_int, init_int);
            map_name_background_object.AddComponent<RectTransform>().sizeDelta = new Vector2(70, 15);
            map_name_background_object.GetComponent<RectTransform>().position = new Vector3(-init_int * 70, init_int * 15, 0);

            Image map_name_background_img = map_name_background_object.AddComponent<Image>();
            map_name_background_img.color = new Color(0, 0, 0, 0.7960784f);

            var map_name_object = new GameObject($"Map_Name_{m.id}");
            map_name_object.transform.localScale = new Vector3(init_int * 0.0936128f, init_int * 0.0936128f, init_int * 0.0936128f);
            map_name_object.AddComponent<RectTransform>().sizeDelta = new Vector2(745, 160);
            map_name_object.GetComponent<RectTransform>().position = new Vector3(-init_int * 70, init_int * 15, 0);

            Text name = map_name_object.AddComponent<Text>();
            name.text = map.GetName();
            name.fontSize = 80;
            name.color = new Color(1, 1, 0, 1);
            name.alignment = TextAnchor.MiddleCenter;
            name.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            var map_select_image_object = new GameObject($"Map_Select_Image_{m.id}");
            map_select_image_object.transform.localScale = new Vector3(init_int, init_int, init_int);
            map_select_image_object.AddComponent<RectTransform>().sizeDelta = new Vector2(71.5f, 46.5f);
            map_select_image_object.GetComponent<RectTransform>().position = new Vector3(-init_int * 70, 0, 0);

            Image map_select_img = map_select_image_object.AddComponent<Image>();
            map_select_img.sprite = select_map;
            map_select_img.color = player.Latest_Map == m.id ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);

            var map_description_object = new GameObject($"Map_Description_{m.id}");
            map_description_object.transform.localScale = new Vector3(init_int * 0.09361279f, init_int * 0.09361279f, init_int * 0.09361279f);
            map_description_object.AddComponent<RectTransform>().sizeDelta = new Vector2(1370, 400);
            map_description_object.GetComponent<RectTransform>().position = new Vector3(init_int * 35, 0, 0);

            Text description = map_description_object.AddComponent<Text>();
            description.text = map.GetDescription();
            description.fontSize = 65;
            description.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            var map_type_object = new GameObject($"Map_Type_{m.id}");
            map_type_object.transform.localScale = new Vector3(init_int * 0.09361279f, init_int * 0.09361279f, init_int * 0.09361279f);
            map_type_object.AddComponent<RectTransform>().sizeDelta = new Vector2(390, 134);
            map_type_object.GetComponent<RectTransform>().position = new Vector3(-init_int * 53, -init_int * 22.5f, 0);

            Text type = map_type_object.AddComponent<Text>();
            type.text = map.GetType();
            type.fontSize = 100;
            type.alignment = TextAnchor.MiddleCenter;
            type.color = new Color(1, 0.75f, 0, 1);
            type.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            Outline outline = map_type_object.AddComponent<Outline>();
            outline.effectColor = new Color(0, 0, 0, 1);
            outline.effectDistance = new Vector2(3, -3);

            if (player.Latest_Map == m.id)
            {
                MapDescription(map, m);
            }

            map_name_object.transform.SetParent(map_name_background_object.transform);
            map_name_background_object.transform.SetParent(map_image_object.transform);
            map_image_object.transform.SetParent(map_object.transform);
            map_select_image_object.transform.SetParent(map_object.transform);
            map_description_object.transform.SetParent(map_object.transform);
            map_type_object.transform.SetParent(map_object.transform);
            map_object.transform.SetParent(parent_object);
        }

        HyperButton.onClick.AddListener(() => {
            Check.color = select_map_hyper_mode == false ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
            select_map_hyper_mode = select_map_hyper_mode == false;

            Map map = MapDataBase.FindMapFromId(select_map_id);
            var m = player.Map[map.GetId()-1];
            MapDescription(map, m);
        });

        Submit_button.onClick.AddListener(() => {
            Map map = MapDataBase.FindMapFromId(select_map_id);
            var m = player.Map[map.GetId() - 1];

            player.Latest_Map = select_map_id;
            if (select_map_hyper_mode == true && m.hyper == true)
            {
                player.Latest_Map_Hyper = true;
            } else
            {
                player.Latest_Map_Hyper = false;
            }
            Json.instance.Save(player);
            SceneManager.LoadSceneAsync("Map");
        });
    }

    public void MapDescription(Map map, Json.MapData m)
    {
        void ChangeMoveSpeed(int parameter)
        {
            if (parameter == 0) return;
            Text value = MoveSpeed.transform.Find("Value").gameObject.GetComponent<Text>();
            value.text = $"{parameter}%";
        }

        void ChangeEmeraldBonus(int parameter)
        {
            Text title = EmeraldBonus.transform.Find("Title").gameObject.GetComponent<Text>();
            Text value = EmeraldBonus.transform.Find("Value").gameObject.GetComponent<Text>();
            title.color = new Color(1, 1, 1, 1);
            value.color = new Color(1, 1, 1, 1);
            value.text = "--";

            if (parameter != 0)
            {
                title.color = new Color(1, 1, 0, 1);
                value.color = new Color(1, 1, 0, 1);
                value.text = $"{parameter}%";
            };
        }

        void ChangeLuckBonus(int parameter)
        {
            Text title = LuckBonus.transform.Find("Title").gameObject.GetComponent<Text>();
            Text value = LuckBonus.transform.Find("Value").gameObject.GetComponent<Text>();
            title.color = new Color(1, 1, 1, 1);
            value.color = new Color(1, 1, 1, 1);
            value.text = "--";

            if (parameter != 0)
            {
                title.color = new Color(1, 1, 0, 1);
                value.color = new Color(1, 1, 0, 1);
                value.text = $"{parameter}%";
            };
        }

        Hyper.SetActive(m.hyper);
        GameObject.Find($"Map_{map.GetId()}").GetComponent<Image>().color = new Color(0.3647059f, 0.3764706f, 0.4117647f, 1);
        GameObject.Find($"Map_Select_Image_{map.GetId()}").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (select_map_hyper_mode == true && m.hyper == true)
        {
            HyperMode Parameter = map.GetHyperParameter();
            ChangeMoveSpeed(Parameter.MoveSpeed);
            ChangeEmeraldBonus(Parameter.EmeraldBonus);
            ChangeLuckBonus(Parameter.LuckBonus);
        }
        else
        {
            MapParameter Parameter = map.GetParameter();
            ChangeMoveSpeed(Parameter.MoveSpeed);
            ChangeEmeraldBonus(Parameter.EmeraldBonus);
            ChangeLuckBonus(Parameter.LuckBonus);
        }
    }
}
