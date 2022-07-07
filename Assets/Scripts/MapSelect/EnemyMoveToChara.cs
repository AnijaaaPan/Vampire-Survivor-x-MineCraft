using UnityEngine;
using UnityEngine.UI;

public class EnemyMoveToChara : MonoBehaviour
{
    public GameObject Chara;
    public Enemy Enemy;

    private int EnemyImagePageIndex = 0;
    private int EnemyImageListCount;

    private static float MoveSpeed = 0.014f;

    private Transform CharaTransform;
    private Transform EnemyTransform;

    private Image EnemyImage;
    private Sprite[] EnemyImageList;

    void Start()
    {
        CharaTransform = Chara.transform;
        EnemyTransform = this.gameObject.transform;

        EnemyImage = GetComponent<Image>();
        EnemyImageList = Enemy.GetIcons();
        EnemyImageListCount = EnemyImageList.Length - 1;
    }

    void Update()
    {
        UpdateCharaImagePage();
        UpdateObjectCoordinate();
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private void UpdateCharaImagePage()
    {
        EnemyImagePageIndex++;
        if (Mathf.FloorToInt(EnemyImagePageIndex / Enemy.GetUpdateCharaImagePage()) > EnemyImageListCount)
        {
            EnemyImagePageIndex = 0;
        }

        EnemyImage.sprite = EnemyImageList[Mathf.FloorToInt(EnemyImagePageIndex / Enemy.GetUpdateCharaImagePage())];
    }

    private void UpdateObjectCoordinate()
    {
        Vector3 EnmeyPosition = EnemyTransform.position;
        Vector3 CharaPosition = CharaTransform.position;

        float Radian = GetRadian(CharaPosition.x, CharaPosition.y, EnmeyPosition.x, EnmeyPosition.y) * (180 / Mathf.PI);
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MathMoveDistanceX = MoveSpeed * cos;
        float MathMoveDistanceY = MoveSpeed * sin;

        int PlayerVectorLeftRight = MathMoveDistanceX < 0 ? -1 : 1;
        EnemyTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);

        float MoveX = EnemyTransform.position.x - MathMoveDistanceX;
        float MoveY = EnemyTransform.position.y - MathMoveDistanceY;

        EnemyTransform.position = new Vector3(MoveX, MoveY, 1);
    }
}
