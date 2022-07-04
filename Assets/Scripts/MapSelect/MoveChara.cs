using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoveChara : MonoBehaviour
{
    public GameObject CharaFront;
    public GameObject CharaBack;
    public GameObject isMap4;

    private Transform CharaTransform;
    private Transform FrontCharaTransform;
    private Transform BackCharaTransform;

    private float MoveSpeed = 0.014f;

    private Image CharaImage;
    private Image CharaBackImageFront;
    private Image CharaBackImageBack;
    private Sprite[] CharaImageList;

    private int CharaImagePageIndex = 0;
    private int CharaImageListCount;

    private bool isClickScreen = false;
    private Vector3 ClickStartPosition;
    private Vector3 ClickEndPosition;

    private List<bool?> LatestPlayerVector = new List<bool?>() { null, null };

    private Json.PlayerData player = Json.instance.Load();
    private MobDataBase MobDataBase = Json.instance.MobDataBase;

    void Start()
    {
        Mob mob = MobDataBase.FindMobFromId(player.Latest_Chara);
        if (player.Latest_Map != 4) Destroy(isMap4);

        CharaTransform = this.gameObject.transform;
        FrontCharaTransform = CharaFront.transform;
        BackCharaTransform = CharaBack.transform;

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

        if (LatestPlayerVector[0] == null && LatestPlayerVector[1] == null && isPushMouseButton())
        {
            List<float> PushMouseButtonSinCos = PushMouseButton();
            MathMoveDistanceX = PushMouseButtonSinCos[0];
            MathMoveDistanceY = PushMouseButtonSinCos[1];
        }
        else
        {
            GetLatestPlayerVector();
        }

        UpdateCharaImagePage(MathMoveDistanceX, MathMoveDistanceY);
        UpdateObjectCoordinate(MathMoveDistanceX, MathMoveDistanceY);
    }

    private List<float> PushMouseButton()
    {
        if (!isClickScreen)
        {
            isClickScreen = true;
            ClickStartPosition = Input.mousePosition;
        }

        ClickEndPosition = Input.mousePosition;
        if (ClickStartPosition.x == ClickEndPosition.x && ClickStartPosition.y == ClickEndPosition.y) return new List<float>() { 0, 0 };

        float Radian = GetRadian(ClickStartPosition.x, ClickStartPosition.y, ClickEndPosition.x, ClickEndPosition.y) * (180 / Mathf.PI);
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        return new List<float>() { MoveSpeed * cos, MoveSpeed * sin };
    }

    private bool isPushMouseButton()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private void GetLatestPlayerVector()
    {
        isClickScreen = false;
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

    private void UpdateCharaImagePage(float MathMoveDistanceX, float MathMoveDistanceY)
    {
        if (!isPushMouseButton() && LatestPlayerVector[0] == null && LatestPlayerVector[1] == null || 
            isPushMouseButton() && MathMoveDistanceX == 0 && MathMoveDistanceY == 0)
        {
            CharaImagePageIndex = 0;
        }
        else
            CharaImagePageIndex++;
            if (Mathf.FloorToInt(CharaImagePageIndex / 4) > CharaImageListCount)
            {
                CharaImagePageIndex = 0;
            }
        CharaImage.sprite = CharaImageList[Mathf.FloorToInt(CharaImagePageIndex / 4)];
        CharaBackImageFront.GetComponent<Image>().sprite = CharaImage.sprite;
        CharaBackImageBack.GetComponent<Image>().sprite = CharaImage.sprite;
    }

    private void UpdateObjectCoordinate(float MathMoveDistanceX, float MathMoveDistanceY)
    {
        if (LatestPlayerVector[0] == null && LatestPlayerVector[1] == null && !isPushMouseButton())
        {
            FrontCharaTransform.position = new Vector3(CharaTransform.position.x, CharaTransform.position.y);
            BackCharaTransform.position = new Vector3(CharaTransform.position.x, CharaTransform.position.y);
        };

        if (LatestPlayerVector[0] != null || isPushMouseButton() && MathMoveDistanceX != 0 && MathMoveDistanceY != 0)
        {
            int PlayerVectorLeftRight = LatestPlayerVector[0] == true || MathMoveDistanceX > 0 ? -1 : 1;
            CharaTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);
            FrontCharaTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);
            BackCharaTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);
        }

        if (LatestPlayerVector[0] != null)
        {
            MathMoveDistanceX = LatestPlayerVector[0] == true ? MoveSpeed : -MoveSpeed;
        }

        if (LatestPlayerVector[1] != null)
        {
            MathMoveDistanceY = LatestPlayerVector[1] == true ? MoveSpeed : -MoveSpeed;
        }

        if (LatestPlayerVector[0] != null && LatestPlayerVector[1] != null)
        {
            float Root2 = Mathf.Sqrt(2);
            MathMoveDistanceX /= Root2;
            MathMoveDistanceY /= Root2;
        }

        float MoveX = CharaTransform.position.x + MathMoveDistanceX;
        float MoveY = CharaTransform.position.y + MathMoveDistanceY;

        CharaTransform.position = new Vector3(MoveX, MoveY, 0);
        if (player.Latest_Map == 4)
        {
            isMap4.transform.position = new Vector3(MoveX, MoveY, 0);
        }
        FrontCharaTransform.position = new Vector3(MoveX - MathMoveDistanceX * 5, MoveY - MathMoveDistanceY * 5);
        BackCharaTransform.position = new Vector3(MoveX - MathMoveDistanceX * 10, MoveY - MathMoveDistanceY * 10);
    }
}