using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chara : MonoBehaviour
{
    public Sprite SelectChara;
    public Sprite UnSelectChara;
    public Sprite UnLockCharaWeapon;

    public Image SelectImage;
    public Image SelectWeapon;

    public Text SelectName;
    public Text SelectDescrpition;
    public Text UnlockCheck;
    public Text UnLockCostText;

    public Button SubmitButton;

    public GameObject UnlockCost;
    public GameObject EffectList;

    private float InitInt = 1 / 34.35838f;
    private float ParameterInitInt = 1 / 1.625f;
    private List<int> ParameterList = new List<int> { 3, 4, 2, 10, 1, 7, 8, 6, 5, 9, 16, 11, 12, 13, 14, 15 };
    private Font TextFont;
    private GameObject BeforeGameObject;

    private Json.PlayerData player = Json.instance.Load();
    private MobDataBase MobDataBase = Json.instance.MobDataBase;
    private WeaponDataBase WeaponDataBase = Json.instance.WeaponDataBase;
    private ItemDataBase ItemDataBase = Json.instance.ItemDataBase;

    void Start()
    {
        TextFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        UnLockCostText.text = CountUnlockChara(player.Character).ToString();

        for (int i = 0; i < ItemDataBase.GetItemLists().Count; i++)
        {
            Item ItemData = ItemDataBase.FindItemFromId(ParameterList[i]);

            GameObject ParameterObject = CreateParameterObject(ItemData);
            GameObject ParameterImageObject = CreateParameterImageObject(ItemData);
            GameObject ParameterEffectNameObject = CreateParameterEffectNameObject(ItemData);
            GameObject ParameterEffectObject = CreateParameterEffectObject();

            ParameterImageObject.transform.SetParent(ParameterObject.transform);
            ParameterEffectNameObject.transform.SetParent(ParameterObject.transform);
            ParameterEffectObject.transform.SetParent(ParameterObject.transform);
            ParameterObject.transform.SetParent(EffectList.transform);
        };

        foreach (Json.CharacterData c in player.Character)
        {
            if (c.hidden == true) continue;

            Mob mob = MobDataBase.FindMobFromId(c.id);
            Weapon weapon = WeaponDataBase.FindWeaponFromId(mob.GetWeaponId());

            GameObject CharaObject = CreateCharaObject(mob, c, weapon);
            GameObject CharaImageObject = CreateCharaImageObject(mob, c);
            GameObject CharaWeaponImageObject = CreateCharaWeaponImageObject(weapon, c);
            GameObject CharaNameObject = CreateCharaNameObject(mob, c);

            CharaImageObject.transform.SetParent(CharaObject.transform);
            CharaWeaponImageObject.transform.SetParent(CharaObject.transform);
            CharaNameObject.transform.SetParent(CharaObject.transform);
            CharaObject.transform.SetParent(this.gameObject.transform);

            if (player.Latest_Chara == mob.GetId())
            {
                BeforeGameObject = CharaObject;
                UpdateDescription(mob, c, weapon);
            }
        };

        SubmitButton.onClick.AddListener(() => {
            Music.instance.ClickSound();
            Mob mob = MobDataBase.FindMobFromName(SelectName.text);
            Json.CharacterData c = player.Character[mob.GetId()];

            Image BeforeObjectImage = BeforeGameObject.GetComponent<Image>();
            if (c.use == false && CountUnlockChara(player.Character) <= player.Money)
            {
                player.Money -= CountUnlockChara(player.Character);
                player.Latest_Chara = mob.GetId();
                c.use = true;
                Json.instance.Save(player);
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
            else if (BeforeObjectImage.color.b == 0.6f)
            {
                player.Latest_Chara = mob.GetId();
                Json.instance.Save(player);
                SceneManager.LoadSceneAsync("MapSelect");
            }
            else if (c.use == true)
            {
                BeforeObjectImage.color = new Color(1, 1, 0.6f, 1);
            };
        });
    }

    private int CountUnlockChara(List<Json.CharacterData> Characters)
    {
        int count = Characters.FindAll(c => c.use == true).Count - 1;
        return 500 + count * 50;
    }

    private void UpdateSelectImage(GameObject Object)
    {
        Music.instance.ClickSound();

        Image BeforeObjectImage = BeforeGameObject.GetComponent<Image>();
        BeforeObjectImage.sprite = UnSelectChara;
        BeforeObjectImage.color = new Color(1, 1, 1, 1);

        BeforeGameObject = Object;
        Image AfterObjectImage = Object.GetComponent<Image>();
        AfterObjectImage.sprite = SelectChara;
    }

    private void UpdateDescription(Mob mob, Json.CharacterData c, Weapon weapon)
    {
        string ResultEffect(Item item_data, Mob mob)
        {
            string Coincidence(float result)
            {
                return result < 0 ? $"{result}" : $"+{result}";
            }

            Parameter data = mob.GetParameter().Find(par => item_data.GetId() == par.effect_id);
            Json.PowerUpList poweroup_data = player.PowerUp.poweruplist.Find(data => data.id == item_data.GetId());
            float ParameterPhase = data == null ? 0 : data.phase;
            int powerupcount = (int)ParameterPhase + item_data.GetCount() - poweroup_data.powerupcount;

            if (powerupcount == 0)
                return item_data.GetId() == 3 ? "100" : " - ";
            if (new List<int> { 1, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 }.Contains(item_data.GetId()))
                return $"{Coincidence(powerupcount * 10)}%";
            if (new List<int> { 2, 9, 16 }.Contains(item_data.GetId()))
                return Coincidence(powerupcount);
            if (item_data.GetId() == 3)
                return $"{100 + powerupcount * 10}";
            return $"{powerupcount * 0.1}";
        }

        void initParameter(Mob mob)
        {
            for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)
            {
                Item ItemData = ItemDataBase.FindItemFromId(i);
                GameObject ParameterObject = GameObject.Find($"Parameter_{ItemData.GetId()}");
                GameObject FindObject = ParameterObject.transform.Find("Effect").gameObject;

                string Effect = ResultEffect(ItemData, mob);
                Text ObjectText = FindObject.GetComponent<Text>();
                ObjectText.text = Effect;
            }
        }

        initParameter(mob);
        SelectImage.sprite = mob.GetIcon();
        SelectName.text = mob.GetName();
        SelectDescrpition.text = mob.GetDescription();

        SelectWeapon.sprite = c.use == true ? weapon.GetIcon() : UnLockCharaWeapon;
        SelectImage.color = c.use == false ? new Color(0, 0, 0, 255) : new Color(255, 255, 255, 255);
        UnlockCost.SetActive(c.use == false);
        UnlockCheck.text = c.use == false ? "アンロック" : "確認";
    }

    private GameObject CreateParameterObject(Item ItemData)
    {
        GameObject Object = new GameObject($"Parameter_{ItemData.GetId()}");
        Object.transform.localScale = new Vector3(ParameterInitInt, ParameterInitInt, ParameterInitInt);

        Object.AddComponent<RectTransform>();
        return Object;
    }

    private GameObject CreateParameterImageObject(Item ItemData)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = new Vector3(ParameterInitInt, ParameterInitInt, ParameterInitInt);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.5f, 0.5f);
        ObjectRectTransform.position = new Vector3(-1.425f, 0, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ItemData.GetIcon();
        return Object;
    }

    private GameObject CreateParameterEffectNameObject(Item ItemData)
    {
        GameObject Object = new GameObject("EffectName");
        Object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(790.6436f, 147.25f);
        ObjectRectTransform.position = new Vector3(-0.175f, 0, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = ItemData.GetName();
        ObjectText.font = TextFont;
        ObjectText.fontSize = 100;
        ObjectText.alignment = TextAnchor.MiddleLeft;
        return Object;
    }

    private GameObject CreateParameterEffectObject()
    {
        GameObject Object = new GameObject("Effect");
        Object.transform.localScale = new Vector3(1 / 400f, 1 / 400f, 1 / 400f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(318.66f, 147.25f);
        ObjectRectTransform.position = new Vector3(1.2225f, 0, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = "";
        ObjectText.font = TextFont;
        ObjectText.fontSize = 100;
        ObjectText.alignment = TextAnchor.MiddleLeft;
        return Object;
    }

    private GameObject CreateCharaObject(Mob mob, Json.CharacterData c, Weapon weapon)
    {
        GameObject Object = new GameObject($"Chara_{mob.GetId()}");
        Object.transform.localScale = new Vector3(InitInt, InitInt, InitInt);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = player.Latest_Chara == mob.GetId() ? SelectChara : UnSelectChara;

        Button ObjectButton = Object.AddComponent<Button>();
        ObjectButton.onClick.AddListener(() => {
            UpdateSelectImage(Object);
            UpdateDescription(mob, c, weapon);
        });
        return Object;
    }

    private GameObject CreateCharaImageObject(Mob mob, Json.CharacterData c)
    {
        GameObject Object = new GameObject("Image");
        Object.transform.localScale = new Vector3(InitInt * 0.3f, InitInt * 0.3f, InitInt * 0.3f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(110, 110);
        ObjectRectTransform.position = new Vector3(0.305f, -0.1175f, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = mob.GetIcon();
        if (c.use == false) ObjectImage.color = new Color(0, 0, 0, 1);
        return Object;
    }

    private GameObject CreateCharaWeaponImageObject(Weapon weapon, Json.CharacterData c)
    {
        GameObject Object = new GameObject("WeaponImage");
        Object.transform.localScale = new Vector3(InitInt * 0.25f, InitInt * 0.25f, InitInt * 0.25f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(65, 65);
        ObjectRectTransform.position = new Vector3(-0.23f, -0.2175f, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.preserveAspect = true;
        ObjectImage.sprite = weapon.GetIcon();
        if (c.use == false) ObjectImage.color = new Color(0, 0, 0, 1);
        return Object;
    }

    private GameObject CreateCharaNameObject(Mob mob, Json.CharacterData c)
    {
        GameObject Object = new GameObject("Name");
        Object.transform.localScale = new Vector3(InitInt * 0.09f, InitInt * 0.09f, InitInt * 0.09f);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(444, 115);
        ObjectRectTransform.position = new Vector3(0.015f, 0.45f, 0);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = mob.GetName();
        ObjectText.fontSize = 100;
        ObjectText.font = TextFont;
        if (c.use == false) ObjectText.color = new Color(0, 0, 0, 1);
        return Object;
    }
}