using UnityEngine;
using UnityEngine.UI;

public class EnemyMoveToChara : MonoBehaviour
{
    public GameObject Chara;
    public Enemy Enemy;

    private int EnemyImagePageIndex;
    private int EnemyImageListCount;

    private static float MoveSpeed = 0.007f;

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

        EnemyImagePageIndex = Random.Range(0, EnemyImageListCount);
    }

    void Update()
    {
        UpdateCharaImagePage();
        UpdateObjectCoordinate();
        TeleportToDestination();
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private void TeleportToDestination()
    {
        string SpawnPlace = "";
        float DistanceX = EnemyTransform.position.x - CharaTransform.position.x;
        float DistanceY = EnemyTransform.position.y - CharaTransform.position.y;

        if (DistanceY > 8) SpawnPlace = "South";
        if (DistanceY < -8) SpawnPlace = "North";
        if (DistanceX > 13) SpawnPlace = "West";
        if (DistanceX < -13) SpawnPlace = "East";
        if (SpawnPlace == "") return;

        SpawnRange GetSpawn = SpawnEnemy.instance.GetSpawn(SpawnPlace);

        float ReSpawnX = Chara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float ReSpawnY = Chara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);

        EnemyTransform.position = new Vector3(ReSpawnX, ReSpawnY, 0);
    }

    private void UpdateCharaImagePage()
    {
        EnemyImagePageIndex++;
        if (Mathf.FloorToInt(EnemyImagePageIndex / Enemy.GetUpdateImagePage()) > EnemyImageListCount)
        {
            EnemyImagePageIndex = 0;
        }

        EnemyImage.sprite = EnemyImageList[Mathf.FloorToInt(EnemyImagePageIndex / Enemy.GetUpdateImagePage())];
    }

    private void UpdateObjectCoordinate()
    {
        Vector3 EnmeyPosition = EnemyTransform.position;
        Vector3 CharaPosition = CharaTransform.position;

        float Radian = GetRadian(CharaPosition.x, CharaPosition.y, EnmeyPosition.x, EnmeyPosition.y) * (180 / Mathf.PI);
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MathMoveDistanceX = MoveSpeed * cos * Enemy.GetSpeed();
        float MathMoveDistanceY = MoveSpeed * sin * Enemy.GetSpeed();

        int PlayerVectorLeftRight = MathMoveDistanceX < 0 ? -1 : 1;
        EnemyTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);

        float MoveX = EnemyTransform.position.x - MathMoveDistanceX;
        float MoveY = EnemyTransform.position.y - MathMoveDistanceY;

        EnemyTransform.position = new Vector3(MoveX, MoveY, 0);
    }
}
