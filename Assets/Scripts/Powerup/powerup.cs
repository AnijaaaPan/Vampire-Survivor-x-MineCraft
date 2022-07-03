using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class powerup : MonoBehaviour
{
    public Sprite BackGround;
    public Sprite BackGroundSelect;
    public Sprite BackGroundItem;
    public Sprite BackGroundCheck;
    public Sprite Check;

    public GameObject SubmitButton;
    public GameObject UnlockCost;

    public Text SelectName;
    public Text SelectDescrpition;
    public Text UnlockCostText;
    public Text MoneyRemain;
    public Image SelectWeapon;
    public Button SellPowerUp;

    private float InitInt = 1 / 34.35838f;
    private GameObject BeforeGameObject;

    private Json.PlayerData player = Json.instance.Load();
    private ItemDataBase ItemDataBase = Json.instance.ItemDataBase;

    void Start()
    {
        for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)
        {
            Item ItemData = ItemDataBase.FindItemFromId(i);
            Json.PowerUpList PowerUpData = player.PowerUp.poweruplist.Find(data => data.id == ItemData.GetId());

            GameObject ItemObject = CreateItemObject(ItemData, PowerUpData);
            GameObject ItemBackGroundObject = CreateItemBackGroundObject();
            GameObject ItemImageObject = CreateItemImageObject(ItemData);
            GameObject ItemNameObject = CreateItemNameObject(ItemData);
            GameObject ItemPowerUpListObject = CreateItemPowerUpListObject(ItemData, PowerUpData);

            ItemBackGroundObject.transform.SetParent(ItemObject.transform);
            ItemImageObject.transform.SetParent(ItemBackGroundObject.transform);
            ItemNameObject.transform.SetParent(ItemObject.transform);
            ItemPowerUpListObject.transform.SetParent(ItemObject.transform);
            ItemObject.transform.SetParent(this.gameObject.transform);

            if (ItemData.GetId() == 1)
            {
                BeforeGameObject = ItemObject;
                UpdateDescription(ItemData, PowerUpData);
            }
        };

        SubmitButton.GetComponent<Button>().onClick.AddListener(() => {
            Music.instance.ClickSound();
            Item ItemData = ItemDataBase.FindItemFromName(SelectName.text);
            Json.PowerUpList PowerUpData = player.PowerUp.poweruplist.Find(data => data.id == ItemData.GetId());
            int cost = MathCost(ItemData);
            if (PowerUpData.powerupcount == 0 || cost > player.Money) return;

            player.Money -= cost;
            PowerUpData.powerupcount -= 1;
            player.PowerUp.allcount += 1;
            player.PowerUp.allcost += cost;
            Json.instance.Save(player);

            MoneyRemain.text = player.Money.ToString();
            GameObject ItemObject = GameObject.Find($"Item_{ItemData.GetId()}");            
            if (PowerUpData.powerupcount == 0)
            {
                Image ImageObject = ItemObject.GetComponent<Image>();
                ImageObject.color = new Color(1, 1, 0.6f, 1);
            }

            int PowerUpId = ItemData.GetCount() - PowerUpData.powerupcount;
            GameObject PowerUpListObject = ItemObject.transform.Find("PowerUpList").gameObject;
            GameObject PowerUpObject = PowerUpListObject.transform.Find($"PowerUp_{PowerUpId}").gameObject;
            GameObject FindObject = PowerUpObject.transform.Find("PowerUpCheck").gameObject;
            Image FindImageObject = FindObject.GetComponent<Image>();
            FindImageObject.color = new Color(1, 1, 1, 1);

            UpdateDescription(ItemData, PowerUpData);
        });

        SellPowerUp.onClick.AddListener(() => {
            Music.instance.ClickSound();
            if (player.PowerUp.allcount == 0) return;

            for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)
            {
                Item ItemData = ItemDataBase.FindItemFromId(i);
                Json.PowerUpList initPowerUp = new Json.PowerUpList();
                initPowerUp.id = ItemData.GetId();
                initPowerUp.powerupcount = ItemData.GetCount();
                player.PowerUp.poweruplist[i - 1] = initPowerUp;
            };

            player.Money += player.PowerUp.allcost;
            player.PowerUp.allcount = 0;
            player.PowerUp.allcost = 0;
            Json.instance.Save(player);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        });
    }
    private int MathCost(Item ItemData)
    {
        int Cost = ItemData.GetCost();
        Json.PowerUpList PowerUpData = player.PowerUp.poweruplist.Find(data => data.id == ItemData.GetId());
        int default_cost = Cost * (ItemData.GetCount() - PowerUpData.powerupcount + 1);
        return (int)(default_cost + default_cost * 0.1f * player.PowerUp.allcount);
    }

    private void UpdateSelectImage(GameObject Object)
    {
        Music.instance.ClickSound();

        Image BeforeObjectImage = BeforeGameObject.GetComponent<Image>();
        BeforeObjectImage.sprite = BackGround;

        BeforeGameObject = Object;
        Image AfterObjectImage = Object.GetComponent<Image>();
        AfterObjectImage.sprite = BackGroundSelect;
    }

    private void UpdateDescription(Item ItemData, Json.PowerUpList PowerUpData)
    {
        SelectName.text = ItemData.GetName();
        SelectDescrpition.text = ItemData.GetDescription();
        SelectWeapon.sprite = ItemData.GetIcon();
        UnlockCostText.text = MathCost(ItemData).ToString();

        SubmitButton.SetActive(PowerUpData.powerupcount != 0);
        UnlockCost.SetActive(PowerUpData.powerupcount != 0);
    }

    private GameObject CreateItemObject(Item ItemData, Json.PowerUpList PowerUpData)
    {
        GameObject Object = new GameObject($"Item_{ItemData.GetId()}");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ItemData.GetId() == 1 ? BackGroundSelect : BackGround;
        
        if (PowerUpData.powerupcount == 0) ObjectImage.color = new Color(1, 1, 0.6f, 1);

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() => {
            UpdateSelectImage(Object);
            UpdateDescription(ItemData, PowerUpData);
        });
        return Object;
    }

    private GameObject CreateItemBackGroundObject()
    {
        GameObject Object = new GameObject("Image_BackGround");
        Object.transform.localScale = new Vector3(InitInt * 0.185f, InitInt * 0.185f, InitInt * 0.185f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = BackGroundItem;
        return Object;
    }

    private GameObject CreateItemImageObject(Item ItemData)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = new Vector3(InitInt * 0.115f, InitInt * 0.115f, InitInt * 0.115f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = ItemData.GetIcon();
        return Object;
    }

    private GameObject CreateItemNameObject(Item ItemData)
    {
        GameObject Object = new GameObject("Name");
        Object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(482.151f, 90.293f);
        ObjectRectTransform.position = new Vector3(0, 0.5f, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = ItemData.GetName();
        ObjectText.fontSize = 100;
        ObjectText.alignment = TextAnchor.MiddleCenter;
        ObjectText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        ObjectText.resizeTextMaxSize = 100;
        ObjectText.resizeTextForBestFit = true;
        ObjectText.color = new Color(0, 0, 0, 255);
        return Object;
    }

    private GameObject CreateItemPowerUpListObject(Item ItemData, Json.PowerUpList PowerUpData)
    {
        GameObject Object = new GameObject("PowerUpList");
        Object.transform.localScale = new Vector3(InitInt * 1.16f, InitInt * 1.16f, InitInt * 1.16f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(ItemData.GetCount() * 5, 5);
        ObjectRectTransform.position = new Vector3(0, -0.45f, 0);
        ObjectRectTransform.position = new Vector3(0, -0.45f, 0);

        GridLayoutGroup ObjectGridLayoutGroup = Object.AddComponent<GridLayoutGroup>();
        ObjectGridLayoutGroup.cellSize = new Vector2(5, 5);
        ObjectGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        ObjectGridLayoutGroup.constraintCount = ItemData.GetCount();

        int PowerUpId = ItemData.GetCount() - PowerUpData.powerupcount + 1;
        for (int n = 1; n < ItemData.GetCount() + 1; n++)
        {
            GameObject PowerUpObject = CreatePowerUpObject(n);
            GameObject PowerUpCheckObject = CreatePowerUpCheckObject(n, PowerUpId);

            PowerUpCheckObject.transform.SetParent(PowerUpObject.transform);
            PowerUpObject.transform.SetParent(Object.transform);
        }
        return Object;
    }

    private GameObject CreatePowerUpObject(int n)
    {
        GameObject Object = new GameObject($"PowerUp_{n}");
        Object.transform.localScale = new Vector3(1 / 29.61929f, 1 / 29.61929f, 1 / 29.61929f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = BackGroundCheck;
        return Object;
    }

    private GameObject CreatePowerUpCheckObject(int n, int PowerUpId)
    {
        GameObject Object = new GameObject("PowerUpCheck");
        Object.transform.localScale = new Vector3(1 / 29.61929f, 1 / 29.61929f, 1 / 29.61929f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(3, 3);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = Check;
        ObjectImage.color = n < PowerUpId ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
        return Object;
    }
}
