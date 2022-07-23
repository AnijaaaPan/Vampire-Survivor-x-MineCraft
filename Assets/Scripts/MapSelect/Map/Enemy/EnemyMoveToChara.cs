using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyMoveToChara : MonoBehaviour
{
    public GameObject Chara;
    public Enemy Enemy;

    public string Type="";
    public float Speed=1f;

    private int EnemyImagePageIndex;
    private int EnemyImageListCount;

    private float MathMoveDistanceX = 0;
    private float MathMoveDistanceY = 0;

    private static float MoveSpeed = 0.028f;

    private Transform CharaTransform;
    private Transform EnemyTransform;

    private Image EnemyImage;
    private Sprite[] EnemyImageList;

    private bool isFireFritta = false;
    private bool isTheWorld = false;

    private Map Map;
    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MapDataBase MapDataBase = Json.instance.MapDataBase;

    private void Awake()
    {
        Map = MapDataBase.FindMapFromId(player.Latest_Map);
    }

    void Start()
    {
        CharaTransform = Chara.transform;
        EnemyTransform = this.gameObject.transform;

        EnemyImage = GetComponent<Image>();
        EnemyImageList = Enemy.GetIcons();
        EnemyImageListCount = EnemyImageList.Length - 1;

        EnemyImagePageIndex = Random.Range(0, EnemyImageListCount);

        MathDistance();
        StartCoroutine(OnFireFritta());
    }

    void Update()
    {
        isTheWorld = PlayerStatus.instance.GetStatus().StopClockTime != 0;
        if (!IsPlaying.instance.isPlay()) return;

        UpdateCharaImagePage();
        if (Type != "Swarm") MathDistance();
        MoveToCharaVector();
        if (Type == "") TeleportToDestination();
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

        EnemySpawn EnemySpawn = Map.GetEnemySpawn();
        if (DistanceY > 8 && EnemySpawn.North) SpawnPlace = "South";
        if (DistanceY < -8 && EnemySpawn.South) SpawnPlace = "North";
        if (DistanceX > 13 && EnemySpawn.East) SpawnPlace = "West";
        if (DistanceX < -13 && EnemySpawn.West) SpawnPlace = "East";
        if (SpawnPlace == "") return;

        SpawnRange GetSpawn = SpawnEnemy.instance.GetSpawn(SpawnPlace);
        float ReSpawnX = Chara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float ReSpawnY = Chara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);
        EnemyTransform.position = new Vector3(ReSpawnX, ReSpawnY, 0);
    }

    private void UpdateCharaImagePage()
    {
        EnemyImage.color = isTheWorld ? Color.blue : Color.white;
        if (isTheWorld) return;

        EnemyImagePageIndex++;
        if (Mathf.FloorToInt(EnemyImagePageIndex / Enemy.GetUpdateImagePage()) > EnemyImageListCount)
        {
            EnemyImagePageIndex = 0;
        }
        EnemyImage.sprite = EnemyImageList[Mathf.FloorToInt(EnemyImagePageIndex / Enemy.GetUpdateImagePage())];
    }

    private void MathDistance()
    {
        Vector3 EnmeyPosition = EnemyTransform.position;
        Vector3 CharaPosition = CharaTransform.position;

        float Radian = GetRadian(CharaPosition.x, CharaPosition.y, EnmeyPosition.x, EnmeyPosition.y) * (180 / Mathf.PI);
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        MathMoveDistanceX = MoveSpeed * Speed * cos * Enemy.GetSpeed();
        MathMoveDistanceY = MoveSpeed * Speed * sin * Enemy.GetSpeed();
    }

    private void MoveToCharaVector()
    {
        if (isTheWorld) return;

        int PlayerVectorLeftRight = MathMoveDistanceX < 0 ? -1 : 1;
        EnemyTransform.localScale = new Vector3(PlayerVectorLeftRight, 1, 1);

        float MoveX = EnemyTransform.position.x - MathMoveDistanceX;
        float MoveY = EnemyTransform.position.y - MathMoveDistanceY;
        EnemyTransform.position = new Vector3(MoveX, MoveY, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "FireFromPlayer") return;
        isFireFritta = true;
        DamageFromFireFritta();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "FireFromPlayer") return;
        isFireFritta = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "FireFromPlayer") return;
        isFireFritta = false;
    }

    private IEnumerator OnFireFritta()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (isFireFritta && IsPlaying.instance.isPlay()) DamageFromFireFritta();
            isFireFritta = false;
        }
    }
    
    private void DamageFromFireFritta()
    {
        EnemyData EnemyData = EnemyStatus.instance.GetEnemyDataList().Find(e => e.Object == gameObject);
        if (EnemyData == null) return;

        int value = Random.Range(20, 40);
        EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, value);
    }
}