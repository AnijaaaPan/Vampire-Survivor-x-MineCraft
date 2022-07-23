using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoveChara : MonoBehaviour
{
    public GameObject isMap4;
    public GameObject JoyStick;
    public GameObject MoveWithChara;
    public GameObject FireFrittaList;

    public Sprite FireFromPlayerImage;

    private Transform CharaTransform;
    private Transform MoveWithCharaTransform;

    private readonly static float MoveSpeed = 0.056f;

    private SpriteRenderer CharaImage;
    private Sprite[] CharaImageList;

    private int CharaImagePageIndex = 0;
    private int CharaImageListCount;
    private const float JoiStickRadiusOfMovement = 1.5f;
    private float LatestRadian = 0;

    private bool isClickScreen = false;
    private Vector3 ClickStartPosition;
    private Vector3 ClickEndPosition;

    private readonly List<bool?> LatestPlayerVector = new List<bool?>() { null, null };

    private Enemy enemy;
    private bool isOnCollision = false;
    private int DamageIndex = 0;
    private Json.PlayerData player = Json.instance.Load();
    private readonly MobDataBase MobDataBase = Json.instance.MobDataBase;

    void Start()
    {
        Mob mob = MobDataBase.FindMobFromId(player.Latest_Chara);
        if (player.Latest_Map != 4) Destroy(isMap4);

        CharaTransform = this.gameObject.transform;
        MoveWithCharaTransform = MoveWithChara.transform;

        CharaImage = GetComponent<SpriteRenderer>();

        CharaImageList = mob.GetIcons();
        CharaImageListCount = CharaImageList.Length - 1;
        StartCoroutine(ChangeisOnCollision());
        StartCoroutine(OnFireFritta());
    }

    private IEnumerator ChangeisOnCollision()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            isOnCollision = false;
        }
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

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
            JoyStick.SetActive(false);
            GetLatestPlayerVector();
        }

        UpdateCharaImagePage(MathMoveDistanceX, MathMoveDistanceY);
        UpdateObjectCoordinate(MathMoveDistanceX, MathMoveDistanceY);
        DamageCharaFromEnemy();
    }

    private List<float> PushMouseButton()
    {
        if (!isClickScreen)
        {
            isClickScreen = true;
            ClickStartPosition = Input.mousePosition;

            player = Json.instance.Load();
            JoyStick.SetActive(player.JoyStick);

            UpdateJoiStick();
        }

        ClickEndPosition = Input.mousePosition;
        if (ClickStartPosition.x == ClickEndPosition.x && ClickStartPosition.y == ClickEndPosition.y) return new List<float>() { 0, 0 };

        float Radian = GetRadian(ClickStartPosition.x, ClickStartPosition.y, ClickEndPosition.x, ClickEndPosition.y) * (180 / Mathf.PI);
        LatestRadian = Radian;
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        if (player.JoyStick)
        {
            Vector3 ClickJoyStickStartPosition = Camera.main.ScreenToWorldPoint(ClickStartPosition);
            Vector3 ClickJoyStickEndPosition = Camera.main.ScreenToWorldPoint(ClickEndPosition);
            float MoveAbsX = ClickJoyStickEndPosition.x - ClickJoyStickStartPosition.x;
            float MoveAbsY = ClickJoyStickEndPosition.y - ClickJoyStickStartPosition.y;
            if (Mathf.Abs(MoveAbsX) > JoiStickRadiusOfMovement || Mathf.Abs(MoveAbsY) > JoiStickRadiusOfMovement)
            {
                UpdateJoiStick(cos * JoiStickRadiusOfMovement, sin * JoiStickRadiusOfMovement);
            }
            else
            {
                UpdateJoiStick(MoveAbsX, MoveAbsY);
            }
        }
        return new List<float>() { MoveSpeed * cos, MoveSpeed * sin };
    }

    private void UpdateJoiStick(float cos = 0, float sin = 0)
    {
        Vector3 ClickJoyStickPosition = Camera.main.ScreenToWorldPoint(ClickStartPosition);
        JoyStick.transform.position = new Vector3(ClickJoyStickPosition.x, ClickJoyStickPosition.y);

        GameObject JoyStickHandle = JoyStick.transform.Find("Handle").gameObject;
        JoyStickHandle.transform.position = new Vector3(JoyStick.transform.position.x + cos, JoyStick.transform.position.y + sin);
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private bool isPushMouseButton()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
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
            LatestRadian = 0;
            LatestPlayerVector[0] = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            LatestRadian = 180;
            LatestPlayerVector[0] = false;
        };

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            LatestRadian = 90;
            LatestPlayerVector[1] = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            LatestRadian = -90;
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
        {
            CharaImagePageIndex++;
            if (Mathf.FloorToInt(CharaImagePageIndex) > CharaImageListCount)
            {
                CharaImagePageIndex = 0;
            }
        }

        CharaImage.sprite = CharaImageList[Mathf.FloorToInt(CharaImagePageIndex)];
    }

    private void UpdateObjectCoordinate(float MathMoveDistanceX, float MathMoveDistanceY)
    {

        if (LatestPlayerVector[0] != null || isPushMouseButton() && MathMoveDistanceX != 0 && MathMoveDistanceY != 0)
        {
            int PlayerVectorLeftRight = LatestPlayerVector[0] == true || MathMoveDistanceX > 0 ? -1 : 1;
            CharaTransform.localScale = new Vector3(PlayerVectorLeftRight * 0.325f, 1 * 0.325f, 1);
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

        CharaTransform.position = new Vector3(MoveX, MoveY);
        MoveWithCharaTransform.position = CharaTransform.position;
        if (player.Latest_Map == 4)
        {
            isMap4.transform.position = new Vector3(MoveX, MoveY, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;

        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == collision.gameObject);
        if (EnemyData == null) return;

        enemy = EnemyData.enemy;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;
        isOnCollision = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy_")) return;
        isOnCollision = false;
    }

    private void DamageCharaFromEnemy()
    {
        if (PlayerStatus.instance.GetStatus().StopClockTime != 0) return;
        if (CharaImage.color == new Color(1, 1, 0, 1)) return;

        ChangeCharaImageColor();
        if (!isOnCollision) return;

        DamageIndex++;
        if (DamageIndex < 5) return;
        DamageIndex = 0;

        int EnemyDamage = enemy.GetDamage() - ItemStatus.instance.GetAllStatusPhase(2);
        EnemyDamage = EnemyDamage <= 0 ? 0 : -EnemyDamage;
        PlayerStatus.instance.UpdateHpStatus(EnemyDamage);
    }

    private void ChangeCharaImageColor()
    {
        CharaImage.color = isOnCollision ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 1);
    }

    // ブレイズ・パウダーを取得した時の処理

    private IEnumerator OnFireFritta()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.025f);

            if (PlayerStatus.instance.GetStatus().FireFrittaTime != 0 && IsPlaying.instance.isPlay()) {
                GameObject Object = new GameObject("FireFromPlayer");
                Object.transform.position = new Vector3(CharaTransform.position.x, CharaTransform.position.y, 0);
                Object.transform.SetParent(FireFrittaList.transform);

                RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
                ObjectRectTransform.sizeDelta = new Vector2(0.25f, 0.25f);

                Image ImageObject = Object.AddComponent<Image>();
                ImageObject.sprite = FireFromPlayerImage;
                ImageObject.preserveAspect = true;

                CircleCollider2D CircleCollider2DObject = Object.AddComponent<CircleCollider2D>();
                CircleCollider2DObject.radius = 0.125f;
                CircleCollider2DObject.isTrigger = true;

                FireFritta ObjectFireFritta = Object.AddComponent<FireFritta>();
                ObjectFireFritta.LatestRadian = LatestRadian;
                ObjectFireFritta.MoveSpeed = MoveSpeed;
            };
        }
    }
}