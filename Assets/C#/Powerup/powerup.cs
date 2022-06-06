using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class powerup : MonoBehaviour
{
    [SerializeField, Tooltip("親")]
    Transform parent_object = null;

    [SerializeField]
    private ItemDataBase ItemDataBase;//  使用するデータベース

    public Sprite background;
    public Sprite select_background;
    public Sprite item_background;
    public Sprite check_background;
    public Sprite check;

    public Image Select_Weapon;
    public Text Select_Name;
    public Text Select_Descrpition;
    public GameObject Submit_button;
    public GameObject Unlock_Cost;
    public Button SellPowerUp;
    public Text Unlock_Cost_Text;

    private float init_int = 1 / 34.35838f;

    // Start is called before the first frame update
    void Start()
    {

        int check_id = 1;
        Json.PlayerData player = Json.instance.Load();
        for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Item item_data = ItemDataBase.FindItemFromId(i);
            Json.PowerUpList poweroup_data = player.PowerUp.poweruplist.Find(data => data.id == item_data.GetId());

            var chara_object = new GameObject($"Item_{item_data.GetId()}");
            chara_object.transform.localScale = new Vector3(init_int, init_int, init_int);

            Image img = chara_object.AddComponent<Image>();
            img.sprite = item_data.GetId() != 1 ? background : SelectDescription(item_data, player, poweroup_data);
            if (poweroup_data.powerupcount == 0)
            {
                img.color = new Color(1, 1, 0.6f, 1);
            }

            chara_object.AddComponent<Button>().onClick.AddListener(() => {
                GameObject.Find($"Item_{check_id}").GetComponent<Image>().sprite = background;
                check_id = item_data.GetId();
                img.sprite = SelectDescription(item_data, player, poweroup_data);
            });

            var back_image_object = new GameObject($"Image_BackGround_{item_data.GetId()}");
            back_image_object.transform.localScale = new Vector3(init_int * 0.185f, init_int * 0.185f, init_int * 0.185f);

            Image back_img = back_image_object.AddComponent<Image>();
            back_img.preserveAspect = true;
            back_img.sprite = item_background;

            var image_object = new GameObject($"Image_{item_data.GetId()}");
            image_object.transform.localScale = new Vector3(init_int * 0.115f, init_int * 0.115f, init_int * 0.115f);

            Image item_img = image_object.AddComponent<Image>();
            item_img.preserveAspect = true;
            item_img.sprite = item_data.GetIcon();

            var name_object = new GameObject($"Name_{item_data.GetId()}");
            Text name = name_object.AddComponent<Text>();
            name.text = item_data.GetName();
            name.fontSize = 100;
            name.alignment = TextAnchor.MiddleCenter;
            name.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            name.resizeTextMaxSize = 100;
            name.resizeTextForBestFit = true;
            name.color = new Color(0, 0, 0, 255);

            name_object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);
            name_object.GetComponent<RectTransform>().sizeDelta = new Vector2(482.151f, 90.293f);
            name_object.GetComponent<RectTransform>().position = new Vector3(0, 0.5f, 0);

            var poweruplist_object = new GameObject($"PowerUpList_{item_data.GetId()}");
            poweruplist_object.AddComponent<RectTransform>().sizeDelta = new Vector2(item_data.GetCount() * 5, 5);
            poweruplist_object.GetComponent<RectTransform>().position = new Vector3(0, -0.45f, 0);
            poweruplist_object.transform.localScale = new Vector3(init_int * 1.16f, init_int * 1.16f, init_int * 1.16f);

            poweruplist_object.AddComponent<GridLayoutGroup>().cellSize = new Vector2(5, 5);
            poweruplist_object.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            poweruplist_object.GetComponent<GridLayoutGroup>().constraintCount = item_data.GetCount();

            int powerup_id = item_data.GetCount() - poweroup_data.powerupcount + 1;
            for (int n = 1; n < item_data.GetCount() + 1; n++)
            {
                var powerup_object = new GameObject($"PowerUp_{item_data.GetId()}_{n}");
                Image powerup_img = powerup_object.AddComponent<Image>();
                powerup_img.sprite = check_background;
                powerup_object.transform.localScale = new Vector3(1 / 29.61929f, 1 / 29.61929f, 1 / 29.61929f);
                var powerup_check_object = new GameObject($"PowerUp_Check_{item_data.GetId()}_{n}");
                powerup_check_object.AddComponent<RectTransform>().sizeDelta = new Vector2(3, 3);

                Image powerup_check_img = powerup_check_object.AddComponent<Image>();
                powerup_check_img.sprite = check;
                powerup_check_img.color = n < powerup_id ? new Color(255, 255, 255, 255) : new Color(0, 0, 0, 0);
                powerup_check_object.transform.localScale = new Vector3(1 / 29.61929f, 1 / 29.61929f, 1 / 29.61929f);

                powerup_check_object.transform.SetParent(powerup_object.transform);
                powerup_object.transform.SetParent(poweruplist_object.transform);
            }

            back_image_object.transform.SetParent(chara_object.transform);
            image_object.transform.SetParent(back_image_object.transform);
            name_object.transform.SetParent(chara_object.transform);
            poweruplist_object.transform.SetParent(chara_object.transform);
            chara_object.transform.SetParent(parent_object);
        };

        Submit_button.GetComponent<Button>().onClick.AddListener(() => {
            Item item_data = ItemDataBase.FindItemFromName(Select_Name.text);
            Json.PowerUpList poweroup_data = player.PowerUp.poweruplist.Find(data => data.id == item_data.GetId());
            int cost = MathCost(item_data, player);
            if (poweroup_data.powerupcount == 0 || cost > player.Money) return;

            player.Money -= cost;
            poweroup_data.powerupcount -= 1;
            player.PowerUp.allcount += 1;
            player.PowerUp.allcost += cost;
            Json.instance.Save(player);
            if (poweroup_data.powerupcount == 0)
            {
                GameObject.Find($"Item_{item_data.GetId()}").GetComponent<Image>().color = new Color(1, 1, 0.6f, 1);
            }

            GameObject.Find($"Money_remain").GetComponent<Text>().text = player.Money.ToString();
            int powerup_id = item_data.GetCount() - poweroup_data.powerupcount;
            GameObject.Find($"PowerUp_Check_{item_data.GetId()}_{powerup_id}").GetComponent<Image>().color = new Color(255, 255, 255, 255);
            SelectDescription(item_data, player, poweroup_data);
        });

        SellPowerUp.onClick.AddListener(() => {
            if (player.PowerUp.allcount == 0) return;

            for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)//  Mobのリストの数の分だけ繰り返す処理
            {
                Item item_data = ItemDataBase.FindItemFromId(i);
                Json.PowerUpList initPowerUp = new Json.PowerUpList();
                initPowerUp.id = item_data.GetId();
                initPowerUp.powerupcount = item_data.GetCount();
                player.PowerUp.poweruplist[i-1] = initPowerUp;
            };

            player.Money += player.PowerUp.allcost;
            player.PowerUp.allcount = 0;
            player.PowerUp.allcost = 0;
            Json.instance.Save(player);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        });
    }

    public Sprite SelectDescription(Item item_data, Json.PlayerData player, Json.PowerUpList poweroup_data)
    {
        Select_Name.text = item_data.GetName();
        Select_Descrpition.text = item_data.GetDescription();

        Select_Weapon.sprite = item_data.GetIcon();
        Unlock_Cost_Text.text = MathCost(item_data, player).ToString();

        Submit_button.SetActive(poweroup_data.powerupcount != 0);
        Unlock_Cost.SetActive(poweroup_data.powerupcount != 0);
        return select_background;
    }

    public int MathCost(Item item_data, Json.PlayerData player)
    {
        Json.PowerUpList poweroup_data = player.PowerUp.poweruplist.Find(data => data.id == item_data.GetId());
        int default_cost = item_data.GetCost() * (item_data.GetCount() - poweroup_data.powerupcount + 1);
        return (int) (default_cost + default_cost * 0.1f * player.PowerUp.allcount);
    }
}
