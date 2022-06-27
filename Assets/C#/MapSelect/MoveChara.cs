using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoveChara : MonoBehaviour
{
    public GameObject CharaFront;
    public GameObject CharaBack;

    [SerializeField]
    private MobDataBase MobDataBase; // 使用するデータベース

    Transform CharaTransform;
    Transform FrontCharaTransform;
    Transform BackCharaTransform;
    Transform CameraTransform;

    Image CharaImage;
    Image CharaBackImageFront;
    Image CharaBackImageBack;
    Sprite[] CharaImageList;

    int CharaImagePageIndex = 0;
    int CharaImageListCount;

    Vector3 CharaPosition;
    Vector3 FrontCharaPosition;
    Vector3 BackCharaPosition;
    Vector3 CameraPosition;

    bool ClickScreen = false;
    Vector3 ClickStartPosition;
    Vector3 ClickEndPosition;

    List<bool?> LatestPlayerVector = new List<bool?>() { null, null };

    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        Mob mob = MobDataBase.FindMobFromId(player.Latest_Chara);

        CharaTransform = this.gameObject.transform;
        FrontCharaTransform = CharaFront.transform;
        BackCharaTransform = CharaBack.transform;
        CameraTransform = Camera.main.transform;

        CharaPosition = CharaTransform.position;
        FrontCharaPosition = FrontCharaTransform.position;
        BackCharaPosition = BackCharaTransform.position;
        CameraPosition = CameraTransform.position;

        CharaImage = this.gameObject.GetComponent<Image>();
        CharaBackImageFront = CharaFront.GetComponent<Image>();
        CharaBackImageBack = CharaBack.GetComponent<Image>();
        CharaImageList = mob.GetIcons();
        CharaImageListCount = CharaImageList.Length - 1;
    }

    void Update()
    {
        float MathMoveDistanceX = 0;
        float MathMoveDistanceY = 0;

        if (Input.GetMouseButtonDown(0) && ClickScreen == false)
        {
            ClickScreen = true;
            ClickStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ClickScreen = false;
        }

        if (Input.GetMouseButton(0))
        {
            ClickEndPosition = Input.mousePosition;
            if (ClickStartPosition.x == ClickEndPosition.x && ClickStartPosition.y == ClickEndPosition.y) return;

            float Radian = GetRadian(ClickStartPosition.x, ClickStartPosition.y, ClickEndPosition.x, ClickEndPosition.y) * (180 / Mathf.PI);
            float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
            float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

            MathMoveDistanceX = 0.014f * cos;
            MathMoveDistanceY = 0.014f * sin;
        }
        else
        {
            GetLatestPlayerVector();
        }

        UpdateCharaImagePage();
        UpdateObjectCoordinate(MathMoveDistanceX, MathMoveDistanceY);
    }

    float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    void GetLatestPlayerVector()
    {
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) LatestPlayerVector[0] = null;

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) &&
            !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) LatestPlayerVector[1] = null;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            LatestPlayerVector[0] = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            LatestPlayerVector[0] = false;
        };

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            LatestPlayerVector[1] = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            LatestPlayerVector[1] = false;
        };
    }

    void UpdateCharaImagePage()
    {
        if (LatestPlayerVector[0] == null && LatestPlayerVector[1] == null && !Input.GetMouseButton(0))
        {
            CharaImagePageIndex = 0;
        }
        else
        {
            CharaImagePageIndex++;
            if (Mathf.FloorToInt(CharaImagePageIndex / 4) > CharaImageListCount)
            {
                CharaImagePageIndex = 0;
            }
        }
        CharaImage.sprite = CharaImageList[Mathf.FloorToInt(CharaImagePageIndex / 4)];
        CharaBackImageFront.GetComponent<Image>().sprite = CharaImage.sprite;
        CharaBackImageBack.GetComponent<Image>().sprite = CharaImage.sprite;
    }

    void UpdateObjectCoordinate(float MathMoveDistanceX, float MathMoveDistanceY)
    {
        if (LatestPlayerVector[0] == null && LatestPlayerVector[1] == null && !Input.GetMouseButton(0))
        {
            FrontCharaPosition.x = CharaPosition.x;
            FrontCharaPosition.y = CharaPosition.y;
            BackCharaPosition.x = CharaPosition.x;
            BackCharaPosition.y = CharaPosition.y;
        };

        if (LatestPlayerVector[0] != null || Input.GetMouseButton(0))
        {
            int PlayerVectorLeftRight = LatestPlayerVector[0] == true || MathMoveDistanceX > 0 ? -1 : 1;
            CharaTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);
            FrontCharaTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);
            BackCharaTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);
        }

        if (LatestPlayerVector[0] != null)
        {
            MathMoveDistanceX = LatestPlayerVector[0] == true ? 0.014f : -0.014f;
        }

        if (LatestPlayerVector[1] != null)
        {
            MathMoveDistanceY = LatestPlayerVector[1] == true ? 0.014f : -0.014f;
        }

        if (LatestPlayerVector[0] != null && LatestPlayerVector[1] != null)
        {
            float Root2 = Mathf.Sqrt(2);
            MathMoveDistanceX /= Root2;
            MathMoveDistanceY /= Root2;
        }

        CharaPosition.x += MathMoveDistanceX;
        CharaPosition.y += MathMoveDistanceY;
        FrontCharaPosition.x = CharaPosition.x - MathMoveDistanceX * 5;
        FrontCharaPosition.y = CharaPosition.y - MathMoveDistanceY * 5;
        BackCharaPosition.x = CharaPosition.x - MathMoveDistanceX * 10;
        BackCharaPosition.y = CharaPosition.y - MathMoveDistanceY * 10;
        CameraPosition.x += MathMoveDistanceX;
        CameraPosition.y += MathMoveDistanceY;

        CharaTransform.position = CharaPosition;
        FrontCharaTransform.position = FrontCharaPosition;
        BackCharaTransform.position = BackCharaPosition;
        CameraTransform.position = CameraPosition;
    }
}