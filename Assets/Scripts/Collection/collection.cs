using UnityEngine;
using UnityEngine.UI;

public class collection : MonoBehaviour
{
    public Sprite BackGround;
    public Sprite SpecialItemBackGround;
    public Sprite Select;
    public Sprite Unknown;

    public Text Title;
    public Text SelectName;
    public Text SelectDescrpition;
    public Text SelectEffect;

    public Image SelectImage;

    public RectTransform SelectImageRectTransform;

    private int UnLockCount = 0;
    private int AllCollectionCount = 0;
    private float InitInt = 1 / 34.35838f;
    private GameObject BeforeGameObject;

    private Json.PlayerData player = Json.instance.Load();
    private WeaponDataBase WeaponDataBase = Json.instance.WeaponDataBase;
    private ItemDataBase ItemDataBase = Json.instance.ItemDataBase;
    private SpecialItemDataBase SpecialItemDataBase = Json.instance.SpecialItemDataBase;

    void Start()
    {
        AllCollectionCount = player.Weapon.Count + player.Item.Count + player.SpecialItem.Count;

        foreach (Json.WeaponData w in player.Weapon)
        {
            if (w.use == true) UnLockCount++;
            Weapon weapon = WeaponDataBase.FindWeaponFromId(w.id);

            GameObject WeaponObject = CreateWeaponObject(w, weapon);
            GameObject WeaponImageObject = CreateWeaponImageObject(w, weapon);
            GameObject WeaponSelectObject = CreateWeaponSelectObject(w);

            SetParent(WeaponObject, WeaponImageObject, WeaponSelectObject);

            if (w.id == 1)
            {
                BeforeGameObject = WeaponObject;
                UpdateSelectImage(WeaponObject);
            }
        };

        foreach (Json.ItemData i in player.Item)
        {
            if (i.use == true) UnLockCount++;
            Item item = ItemDataBase.FindItemFromId(i.id);

            GameObject ItemObject = CreateItemObject(i, item);
            GameObject ItemImageObject = CreateItemImageObject(i, item);
            GameObject ItemSelectObject = CreateItemSelectObject();

            SetParent(ItemObject, ItemImageObject, ItemSelectObject);
        }

        foreach (Json.SpecialItemData i in player.SpecialItem)
        {
            if (i.use == true) UnLockCount++;
            SpecialItem item = SpecialItemDataBase.FindSpecialItemFromId(i.id);

            GameObject SpecialItemObject = CreateSpecialItemObject(i, item);
            GameObject SpecialItemImageObject = CreateSpecialItemImageObject(i, item);
            GameObject SpecialItemSelectObject = CreateItemSelectObject();

            SetParent(SpecialItemObject, SpecialItemImageObject, SpecialItemSelectObject);
        }

        Title.text = $"コレクション：{AllCollectionCount}のうち{UnLockCount}完了";
    }

    private void SetParent(GameObject Object, GameObject ImageObject, GameObject SelectObject)
    {
        ImageObject.transform.SetParent(Object.transform);
        SelectObject.transform.SetParent(Object.transform);
        Object.transform.SetParent(this.gameObject.transform);
    }

    private void UpdateSelectImage(GameObject Object)
    {
        GameObject BeforeObject = BeforeGameObject.transform.Find($"SelectImage").gameObject;
        Image BeforeObjectImage = BeforeObject.GetComponent<Image>();
        BeforeObjectImage.color = new Color(0, 0, 0, 0);

        BeforeGameObject = Object;
        GameObject AfterObject = Object.transform.Find($"SelectImage").gameObject;
        Image AfterObjectImage = AfterObject.GetComponent<Image>();
        AfterObjectImage.color = new Color(1, 1, 1, 1);
    }

    private GameObject CreateWeaponObject(Json.WeaponData w, Weapon weapon)
    {
        void UpdateDescription()
        {
            if (w.use == true)
            {
                SelectImage.sprite = weapon.GetIcon();
                SelectName.text = weapon.GetName();
                SelectDescrpition.text = weapon.GetDescription();
                SelectEffect.text = weapon.GetEffect();
                SelectImageRectTransform.sizeDelta = new Vector2(35, 35);
            }
            else
            {
                SelectImage.sprite = Unknown;
                SelectName.text = "???";
                SelectDescrpition.text = "まだ未発見です";
                SelectEffect.text = "";
                SelectImageRectTransform.sizeDelta = new Vector2(25, 25);
            };

            SelectImage.color = w.use == true && w.id == 36 ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
        }
        
        if (w.id == 1) UpdateDescription();

        GameObject Object = new GameObject($"List_WeaponId_{w.id}");
        Object.transform.localScale = w.use == true ? new Vector3(InitInt, InitInt, InitInt) : new Vector3(InitInt * 0.85f, InitInt * 0.85f, InitInt * 0.85f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = BackGround;

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() =>
        {
            Music.instance.ClickSound();
            UpdateSelectImage(Object);
            UpdateDescription();
        });
        return Object;
    }

    private GameObject CreateWeaponSelectObject(Json.WeaponData w)
    {
        GameObject Object = new GameObject("SelectImage");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(27.5f, 27.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = Select;
        ObjectImage.color = w.id == 1 ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
        return Object;
    }

    private GameObject CreateWeaponImageObject(Json.WeaponData w, Weapon weapon)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = w.use == true ? new Vector3(InitInt, InitInt, InitInt) : new Vector3(InitInt * 0.65f, InitInt * 0.65f, InitInt * 0.65f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(17.5f, 17.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = w.use == true ? weapon.GetIcon() : Unknown;
        ObjectImage.color = w.use == true && w.id == 36 ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
        return Object;
    }

    private GameObject CreateItemObject(Json.ItemData i, Item item)
    {
        void UpdateDescription()
        {
            SelectEffect.text = "";
            if (i.use == true)
            {
                SelectImage.sprite = item.GetIcon();
                SelectName.text = item.GetName();
                SelectDescrpition.text = item.GetDescription();
                SelectImageRectTransform.sizeDelta = new Vector2(35, 35);
            }
            else
            {
                SelectImage.sprite = Unknown;
                SelectName.text = "???";
                SelectDescrpition.text = "まだ未発見です";
                SelectImageRectTransform.sizeDelta = new Vector2(25, 25);
            };
        }

        GameObject Object = new GameObject($"List_ItemId_{i.id}");
        Object.transform.localScale = i.use == true ? new Vector3(InitInt, InitInt, InitInt) : new Vector3(InitInt * 0.85f, InitInt * 0.85f, InitInt * 0.85f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = BackGround;

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() =>
        {
            Music.instance.ClickSound();
            UpdateSelectImage(Object);
            UpdateDescription();
        });
        return Object;
    }

    private GameObject CreateItemSelectObject()
    {
        GameObject Object = new GameObject("SelectImage");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(27.5f, 27.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = Select;
        ObjectImage.color = new Color(0, 0, 0, 0);
        return Object;
    }

    private GameObject CreateItemImageObject(Json.ItemData i, Item item)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = i.use == true ? new Vector3(InitInt, InitInt, InitInt) : new Vector3(InitInt * 0.65f, InitInt * 0.65f, InitInt * 0.65f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(17.5f, 17.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = i.use == true ? item.GetIcon() : Unknown;
        return Object;
    }

    private GameObject CreateSpecialItemObject(Json.SpecialItemData i, SpecialItem item)
    {
        void UpdateDescription()
        {
            if (i.use == true)
            {
                SelectImage.sprite = item.GetIcon();
                SelectName.text = item.GetName();
                SelectDescrpition.text = item.GetDescription();
                SelectEffect.text = item.GetEffect();
                SelectImageRectTransform.sizeDelta = new Vector2(35, 35);
            }
            else
            {
                SelectImage.sprite = Unknown;
                SelectName.text = "???";
                SelectDescrpition.text = "まだ未発見です";
                SelectEffect.text = "";
                SelectImageRectTransform.sizeDelta = new Vector2(25, 25);
            };
        }

        GameObject Object = new GameObject($"List_SpecialItemId_{i.id}");
        Object.transform.localScale = i.use == true ? new Vector3(InitInt, InitInt, InitInt) : new Vector3(InitInt * 0.85f, InitInt * 0.85f, InitInt * 0.85f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = SpecialItemBackGround;

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() =>
        {
            Music.instance.ClickSound();
            UpdateSelectImage(Object);
            UpdateDescription();
        });
        return Object;
    }

    private GameObject CreateSpecialItemImageObject(Json.SpecialItemData i, SpecialItem item)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = i.use == true ? new Vector3(InitInt, InitInt, InitInt) : new Vector3(InitInt * 0.65f, InitInt * 0.65f, InitInt * 0.65f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(17.5f, 17.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = i.use == true ? item.GetIcon() : Unknown;
        return Object;
    }
}