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

        StartCoroutine(nameof(SpawnInterval));
    }

    private IEnumerator SpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(StageEnemys.SpawnInterval);

            int NowMonsterCount = StageEnemys.EnemyCount - EnemyStatus.instance.GetEnemyDataList().Count;
            if (NowMonsterCount > 0)
            {
                Spawn(Mathf.Abs(NowMonsterCount));
            }
            else
            {
                Spawn();
            }
        }
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

            List<BossEnemys> BossEnemys = StageEnemys.BossEnemys;
            if (BossEnemys.Count != 0) CreateBossEnemys(BossEnemys);
            if (StageEnemys.Events.Count != 0) StartEvent(StageEnemys.Events);

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

    private GameObject CreateEnemyObject(Enemy Enemy, float SpawnX, float SpawnY)
    {
        float LocalScale = Enemy.GetLocalScale();

        GameObject Object = new GameObject($"Enemy_{EnemyCount}");

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1.25f * LocalScale, 1.25f * LocalScale);
        ObjectRectTransform.position = new Vector3(SpawnX, SpawnY, 0);

        Image ImageObject = Object.AddComponent<Image>();
        ImageObject.color = Enemy.GetColor();
        ImageObject.sprite = Enemy.GetIcon();
        ImageObject.preserveAspect = true;

        Rigidbody2D Rigidbody2DObject = Object.AddComponent<Rigidbody2D>();
        Rigidbody2DObject.mass = 0;
        Rigidbody2DObject.gravityScale = 0;

        CircleCollider2D CircleCollider2DObject = Object.AddComponent<CircleCollider2D>();
        CircleCollider2DObject.radius = 0.5f * LocalScale;

        Object.transform.SetParent(this.gameObject.transform);
        return Object;
    }

    private void CreateEnemy()
    {
        SpawnRange GetSpawn = GetRandom(SpawnRanges);
        float SpawnX = MoveWithChara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float SpawnY = MoveWithChara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);

        Enemy Enemy = GetRandom(StageEnemys.Enemies);
        GameObject Object = CreateEnemyObject(Enemy, SpawnX, SpawnY);

        EnemyMoveToChara EnemyMoveToChara = Object.AddComponent<EnemyMoveToChara>();
        EnemyMoveToChara.Chara = Chara;
        EnemyMoveToChara.Enemy = Enemy;

        EnemyStatus.instance.AddEenmyDataList(EnemyCount, Enemy, Object);
        EnemyCount++;
    }

    private void CreateBossEnemys(List<BossEnemys> BossEnemys)
    {
        for (int i = 0; i < BossEnemys.Count; i++)
        {
            SpawnRange GetSpawn = GetRandom(SpawnRanges);
            float SpawnX = MoveWithChara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
            float SpawnY = MoveWithChara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);
            BossEnemys BossEnemy = BossEnemys[i];

            Enemy Enemy = BossEnemy.Enemy;
            GameObject Object = CreateEnemyObject(Enemy, SpawnX, SpawnY);

            EnemyMoveToChara EnemyMoveToChara = Object.AddComponent<EnemyMoveToChara>();
            EnemyMoveToChara.Chara = Chara;
            EnemyMoveToChara.Enemy = Enemy;

            EnemyStatus.instance.AddEenmyDataList(EnemyCount, Enemy, Object, BossEnemy.Treasure);
            EnemyCount++;
        }
    }

    private void StartEvent(List<Events> EventsList)
    {
        for (int i = 0; i < EventsList.Count; i++)
        {
            StartCoroutine(EventInterval(EventsList[i]));
        }
    }

    private IEnumerator EventInterval(Events Events)
    {
        int Repeat = 0;
        while (true)
        {
            List<GameObject> ObjectList = new List<GameObject>();
            if (Repeat > Events.MaxRepeats) yield break;

            bool Probability = ExpStatus.instance.Probability(Events.Chance);
            if (!Probability) yield break;

            if (Events.Type == "Swarm")
            {
                ObjectList = CreateEventSwarm(Events, ObjectList);
            }
            else if (Events.Type == "Wall")
            {
                ObjectList = CreateEventWall(Events, ObjectList);
            }

            Repeat++;
            yield return new WaitForSeconds(Events.Delay);
            DestroyObject(ObjectList);
        }
    }

    private List<GameObject> CreateEventSwarm(Events Events, List<GameObject> ObjectList)
    {
        Enemy Enemy = Events.EventEnemy.Enemy;

        SpawnRange GetSpawn = GetRandom(SpawnRanges);
        float SpawnX = MoveWithChara.transform.position.x + Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float SpawnY = MoveWithChara.transform.position.y + Random.Range(GetSpawn.DownY, GetSpawn.UpY);

        for (int i = 0; i < Events.AmountPer; i++)
        {
            GameObject Object = CreateEnemyObject(Enemy, SpawnX + Random.Range(0f, 1f), SpawnY + Random.Range(0f, 1f));

            EnemyMoveToChara EnemyMoveToChara = Object.AddComponent<EnemyMoveToChara>();
            EnemyMoveToChara.Chara = Chara;
            EnemyMoveToChara.Enemy = Enemy;
            EnemyMoveToChara.Speed = Events.EventEnemy.MoveSpeed;
            EnemyMoveToChara.Type = "Swarm";

            EnemyStatus.instance.AddEenmyDataList(EnemyCount, Enemy, Object);
            EnemyCount++;

            ObjectList.Add(Object);
        }

        return ObjectList;
    }
    private List<GameObject> CreateEventWall(Events Events, List<GameObject> ObjectList)
    {
        Enemy Enemy = Events.EventEnemy.Enemy;
        float OneEnemyRadian = 360f / Events.AmountPer;
        float InitRadian = 180f;        

        for (int i = 0; i < Events.AmountPer; i++)
        {
            float sin = Mathf.Sin(InitRadian * (Mathf.PI / 180));
            float cos = Mathf.Cos(InitRadian * (Mathf.PI / 180));

            float SpawnX = MoveWithChara.transform.position.x + cos * 25;
            float SpawnY = MoveWithChara.transform.position.y + sin * 15;

            GameObject Object = CreateEnemyObject(Enemy, SpawnX, SpawnY);

            EnemyMoveToChara EnemyMoveToChara = Object.AddComponent<EnemyMoveToChara>();
            EnemyMoveToChara.Chara = Chara;
            EnemyMoveToChara.Enemy = Enemy;
            EnemyMoveToChara.Speed = Events.EventEnemy.MoveSpeed;
            EnemyMoveToChara.Type = "Wall";

            EnemyStatus.instance.AddEenmyDataList(EnemyCount, Enemy, Object);
            EnemyCount++;

            ObjectList.Add(Object);
            InitRadian -= OneEnemyRadian;
        }

        return ObjectList;
    }

    private void DestroyObject(List<GameObject> ObjectList)
    {
        for (int i = 0; i < ObjectList.Count; i++)
        {
            GameObject Object = ObjectList[i];
            EnemyStatus.instance.RemoveObjectData(Object);
        }
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
