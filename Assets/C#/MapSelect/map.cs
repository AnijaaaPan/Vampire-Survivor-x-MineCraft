using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class map : MonoBehaviour
{
    public GameObject MoveSpeed;
    public GameObject EmeraldBonus;
    public GameObject LuckBonus;

    public Sprite SelectMap;

    public Image Check;
    public Button SubmitButton;
    public Button HyperButton;
    public GameObject Hyper;

    private Font TextFont;
    private float InitInt = 1 / 34.35838f;
    private GameObject BeforeGameObject;

    private Json.PlayerData player = Json.instance.Load();
    private MapDataBase MapDataBase = Json.instance.MapDataBase;

    void Start()
    {
        TextFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        if (player.Latest_Map_Hyper == false) Check.color = new Color(0, 0, 0, 0);

        foreach (Json.MapData m in player.Map)
        {
            if (m.use == false) continue;

            Map map = MapDataBase.FindMapFromId(m.id);

            GameObject MapObject = CreateMapObject(map, m);
            GameObject MapImageObject = CreateMapImageObject();
            GameObject MapBackgroundObject = CreateMapBackgroundObject();
            GameObject MapNameObject = CreateNameObject(map);
            GameObject MapSelectObject = CreateSelectObject(m);
            GameObject MapDescriptionObject = CreateDescriptionObject(map);
            GameObject MapTypeObject = CreateTypeObject(map);

            MapNameObject.transform.SetParent(MapBackgroundObject.transform);
            MapBackgroundObject.transform.SetParent(MapImageObject.transform);
            MapImageObject.transform.SetParent(MapObject.transform);
            MapSelectObject.transform.SetParent(MapObject.transform);
            MapDescriptionObject.transform.SetParent(MapObject.transform);
            MapTypeObject.transform.SetParent(MapObject.transform);
            MapObject.transform.SetParent(this.gameObject.transform);

            if (player.Latest_Map == m.id)
            {
                BeforeGameObject = MapObject;
                UpdateDescription(map, m);
            }
        }

        HyperButton.onClick.AddListener(() => {
            Music.instance.ClickSound();
            Check.color = player.Latest_Map_Hyper == false ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
            player.Latest_Map_Hyper = player.Latest_Map_Hyper == false;

            Map map = MapDataBase.FindMapFromId(player.Latest_Map);
            Json.MapData m = player.Map[map.GetId() - 1];
            UpdateDescription(map, m);
        });

        SubmitButton.onClick.AddListener(() => {
            Music.instance.ClickSound();
            Map map = MapDataBase.FindMapFromId(player.Latest_Map);
            Json.MapData m = player.Map[map.GetId() - 1];

            player.Latest_Map = map.GetId();
            Json.instance.Save(player);
            SceneManager.LoadSceneAsync("Map");
        });
    }

    private void UpdateSelectImage(GameObject Object)
    {
        Image BeforeObjectImage = BeforeGameObject.GetComponent<Image>();
        BeforeObjectImage.color = new Color(0, 0, 0, 0);

        GameObject BeforeImageObject = BeforeGameObject.transform.Find($"SelectImage").gameObject;
        Image BeforeImageObjectImage = BeforeImageObject.GetComponent<Image>();
        BeforeImageObjectImage.color = new Color(0, 0, 0, 0);

        BeforeGameObject = Object;
        Image ObjectImage = Object.GetComponent<Image>();
        ObjectImage.color = new Color(0.3647059f, 0.3764706f, 0.4117647f, 1);

        GameObject ImageObject = Object.transform.Find($"SelectImage").gameObject;
        Image ImageObjectImage = ImageObject.GetComponent<Image>();
        ImageObjectImage.color = new Color(1, 1, 1, 1);
    }

    public void UpdateDescription(Map map, Json.MapData m)
    {
        void ChangeMoveSpeed(int parameter)
        {
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
        if (player.Latest_Map_Hyper == true && m.hyper == true)
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

    private GameObject CreateMapObject(Map map, Json.MapData m)
    {
        GameObject Object = new GameObject($"Map_{m.id}");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        Object.AddComponent<RectTransform>();

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.color = player.Latest_Map == m.id ? new Color(0.3647059f, 0.3764706f, 0.4117647f, 1) : new Color(0, 0, 0, 0);

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() => {
            Music.instance.ClickSound();

            player.Latest_Map = m.id;
            UpdateSelectImage(Object);
            UpdateDescription(map, m);
        });
        return Object;
    }

    private GameObject CreateMapImageObject()
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(70, 45);
        ObjectRectTransform.position = new Vector3(-InitInt * 70, 0, 0);

        Object.AddComponent<Image>();
        return Object;
    }

    private GameObject CreateMapBackgroundObject()
    {
        GameObject Object = new GameObject("Background");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(70, 15);
        ObjectRectTransform.position = new Vector3(-InitInt * 70, InitInt * 15, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.color = new Color(0, 0, 0, 0.7960784f);
        return Object;
    }

    private GameObject CreateNameObject(Map map)
    {
        GameObject Object = new GameObject("Name");
        Object.transform.localScale = new Vector3(InitInt * 0.0936128f, InitInt * 0.0936128f, InitInt * 0.0936128f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(745, 160);
        ObjectRectTransform.position = new Vector3(-InitInt * 70, InitInt * 15, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = map.GetName();
        ObjectText.font = TextFont;
        ObjectText.fontSize = 80;
        ObjectText.color = new Color(1, 1, 0, 1);
        ObjectText.alignment = TextAnchor.MiddleCenter;
        return Object;
    }

    private GameObject CreateSelectObject(Json.MapData m)
    {
        GameObject Object = new GameObject("SelectImage");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(71.5f, 46.5f);
        ObjectRectTransform.position = new Vector3(-InitInt * 70, 0, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = SelectMap;
        ObjectImage.color = player.Latest_Map == m.id ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
        return Object;
    }

    private GameObject CreateDescriptionObject(Map map)
    {
        GameObject Object = new GameObject("Description");
        Object.transform.localScale = new Vector3(InitInt * 0.09361279f, InitInt * 0.09361279f, InitInt * 0.09361279f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1370, 400);
        ObjectRectTransform.position = new Vector3(InitInt * 35, 0, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = map.GetDescription();
        ObjectText.font = TextFont;
        ObjectText.fontSize = 65;
        return Object;
    }
    private GameObject CreateTypeObject(Map map)
    {
        GameObject Object = new GameObject("Type");
        Object.transform.localScale = new Vector3(InitInt * 0.09361279f, InitInt * 0.09361279f, InitInt * 0.09361279f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(390, 134);
        ObjectRectTransform.position = new Vector3(-InitInt * 53, -InitInt * 22.5f, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = map.GetType();
        ObjectText.font = TextFont;
        ObjectText.fontSize = 100;
        ObjectText.alignment = TextAnchor.MiddleCenter;
        ObjectText.color = new Color(1, 0.75f, 0, 1);

        Outline ObjectOutline = Object.AddComponent<Outline>();
        ObjectOutline.effectColor = new Color(0, 0, 0, 1);
        ObjectOutline.effectDistance = new Vector2(3, -3);
        return Object;
    }
}