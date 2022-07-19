using UnityEngine;
using UnityEngine.UI;

public class RotateToDropItem : MonoBehaviour
{
    public GameObject Chara;
    public GameObject DropItem;

    public Sprite ItemImage;

    private GameObject DropItemImageObject;

    private float DisplayLeftX;
    private float DisplayRightX;
    private float DisplayBottomY;
    private float DisplayTopY;

    private Image ArrowImage;
    private Image DropItemImage;

    void Start()
    {
        ArrowImage = GetComponent<Image>();

        if (ItemImage)
        {
            CreateImageObject();
        }
    }

    void Update()
    {
        UpdateArrouPotision();
        DisplayArrow();
    }

    private void CreateImageObject()
    {
        GameObject Object = new GameObject("DropItemImage");
        Object.transform.SetParent(this.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.5f, 0.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ItemImage;
        ObjectImage.preserveAspect = true;

        DropItemImageObject = Object;
        DropItemImage = ObjectImage;
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private void UpdateArrouPotision()
    {
        float Radian = GetRadian(Chara.transform.position.x, Chara.transform.position.y, DropItem.transform.position.x, DropItem.transform.position.y) * (180 / Mathf.PI);
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MoveX = Chara.transform.position.x + cos * 10f;
        float MoveY = Chara.transform.position.y + sin * 5.5f;
        transform.position = new Vector3(MoveX, MoveY, 0);
        transform.localEulerAngles = new Vector3(0, 0, Radian);
        if (DropItemImageObject)
        {
            DropItemImageObject.transform.localEulerAngles = new Vector3(0, 0, -Radian);
        }
    }

    private void DisplayArrow()
    {
        DisplayLeftX = Chara.transform.position.x - 11.5f;
        DisplayRightX = Chara.transform.position.x + 11.5f;
        DisplayBottomY = Chara.transform.position.y - 7;
        DisplayTopY = Chara.transform.position.y + 7;

        float DropItemX = DropItem.transform.position.x;
        float DropItemY = DropItem.transform.position.y;

        bool isObjectInRange = DisplayLeftX <= DropItemX && DropItemX <= DisplayRightX && DisplayBottomY <= DropItemY && DropItemY <= DisplayTopY;
        ArrowImage.color = isObjectInRange ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
        if (DropItemImageObject)
        {
            DropItemImage.color = ArrowImage.color;
        }
    }
}
