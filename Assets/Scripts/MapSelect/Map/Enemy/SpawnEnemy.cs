using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnRange
{
    public string type;
    public int LeftX;
    public int RightX;
    public int DownY;
    public int UpY;
}

public class SpawnEnemy : MonoBehaviour
{
    static public SpawnEnemy instance;

    public GameObject Chara;
    public GameObject MoveWithChara;

    private readonly Json.PlayerData player = Json.instance.Load();
    private readonly MapDataBase MapDataBase = Json.instance.MapDataBase;

    private int phase = 0;
    private int EnemyCount = 1;

    private Map Map;
    private StageEnemys StageEnemys;
    private List<SpawnRange> SpawnRanges = new List<SpawnRange>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Map = MapDataBase.FindMapFromId(player.Latest_Map);
        StageEnemys = Map.GetStageEnemys().Find(m => m.phase == phase);

        InitSpawnSetActive();
        Spawn(StageEnemys.EnemyCount);
        StartCoroutine("SpawnInterval");
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        Timer Timer = CountTimer.instance.GetTimer();
        if (Timer.Minute != phase)
        {
            phase = Timer.Minute;
            StageEnemys = Map.GetStageEnemys().Find(m => m.phase == phase);
            Spawn(StageEnemys.EnemyCount);
        }
    }

    private void InitSpawnSetActive()
    {
        EnemySpawn EnemySpawn = Map.GetEnemySpawn();

        if (EnemySpawn.North) InitSpawnRange("North", -13, 13, 6, 8);
        if (EnemySpawn.South) InitSpawnRange("South", -13, 13, -8, -6);
        if (EnemySpawn.West) InitSpawnRange("West", -13, -11, -8, 8);
        if (EnemySpawn.East) InitSpawnRange("East", 11, 13, -8, 8);
    }

    private void InitSpawnRange(string type, int LeftX, int RightX, int DownY, int UpY)
    {
        SpawnRange SpawnRange = new SpawnRange
        {
            type = type,
            LeftX = LeftX,
            RightX = RightX,
            DownY = DownY,
            UpY = UpY
        };
        SpawnRanges.Add(SpawnRange);
    }

    private void Spawn(int SpawnCountLimit = 1)
    {
        if (!IsPlaying.instance.isPlay()) return;

        for (int i = 0; i < SpawnCountLimit; i++)
        {
            CreateEnemy();
        }
    }

    public SpawnRange GetSpawn(string type)
    {
        return SpawnRanges.Find(s => s.type == type);
    }

    private void CreateEnemy()
    {
        Enemy SelectEnemy = GetRandom(StageEnemys.Enemies);

        SpawnRange GetSpawn = GetRandom(SpawnRanges);
        float SpawnX = MoveWithChara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float SpawnY = MoveWithChara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);

        GameObject Object = new GameObject($"Enemy_{EnemyCount}");

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1.25f, 1.25f);
        ObjectRectTransform.position = new Vector3(SpawnX, SpawnY, 0);

        EnemyMoveToChara EnemyMoveToChara = Object.AddComponent<EnemyMoveToChara>();
        EnemyMoveToChara.Chara = Chara;
        EnemyMoveToChara.Enemy = SelectEnemy;

        Image ImageObject = Object.AddComponent<Image>();
        ImageObject.color = SelectEnemy.GetColor();
        ImageObject.sprite = SelectEnemy.GetIcon();
        ImageObject.preserveAspect = true;

        Rigidbody2D Rigidbody2DObject = Object.AddComponent<Rigidbody2D>();
        Rigidbody2DObject.mass = 0;
        Rigidbody2DObject.gravityScale = 0;

        CircleCollider2D CircleCollider2DObject = Object.AddComponent<CircleCollider2D>();
        CircleCollider2DObject.radius = 0.5f;

        Object.transform.SetParent(this.gameObject.transform);

        EnemyStatus.instance.AddEenmyDataList(EnemyCount, SelectEnemy, Object);
        EnemyCount++;
    }

    IEnumerator SpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(StageEnemys.SpawnInterval);

            int NowMonsterCount = StageEnemys.EnemyCount - EnemyStatus.instance.GetEnemyDataList().Count;
            if (NowMonsterCount > 0)
            {
                Spawn(Mathf.Abs(NowMonsterCount));
            } else
            {
                Spawn();
            }
        }
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
