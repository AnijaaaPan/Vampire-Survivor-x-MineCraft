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
    public GameObject Chara;
    public GameObject MoveWithChara;

    private Json.PlayerData player = Json.instance.Load();
    private MapDataBase MapDataBase = Json.instance.MapDataBase;

    private int phase = 0;
    private Map Map;
    private StageEnemys StageEnemys;
    private List<SpawnRange> SpawnRanges = new List<SpawnRange>();

    void Start()
    {
        Map = MapDataBase.FindMapFromId(player.Latest_Map);
        StageEnemys = Map.GetStageEnemys().Find(m => m.phase == phase);

        InitSpawnSetActive();
        SetSpawn(StageEnemys.EnemyCount);

        StartCoroutine("Spawn");
    }

    void Update()
    {
        SetTimer SetTimer = CountTimer.instance.GetSetTimer();
        if (SetTimer.Minute != phase)
        {
            phase = (int) SetTimer.Minute;
            StageEnemys = Map.GetStageEnemys().Find(m => m.phase == phase);
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
        SpawnRange InitSpawnRange = new SpawnRange();
        InitSpawnRange.type = type;
        InitSpawnRange.LeftX = LeftX;
        InitSpawnRange.RightX = RightX;
        InitSpawnRange.DownY = DownY;
        InitSpawnRange.UpY = UpY;
        SpawnRanges.Add(InitSpawnRange);
    }

    private void SetSpawn(int SpawnCountLimit = 1)
    {
        int SpawnCount = 0;
        while (SpawnCount < SpawnCountLimit)
        {
            CteateEnemy();
            SpawnCount++;
        }
    }

    private void CteateEnemy()
    {
        SpawnRange GetSpawn = GetRandom(SpawnRanges);
        float x = Random.Range(GetSpawn.LeftX, GetSpawn.RightX);
        float y = Random.Range(GetSpawn.DownY, GetSpawn.UpY);

        GameObject Object = new GameObject("Enemy");

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(1.25f, 1.25f);
        ObjectRectTransform.position = new Vector3(MoveWithChara.transform.position.x + x, MoveWithChara.transform.position.y + y, 0);

        EnemyMoveToChara EnemyMoveToChara = Object.AddComponent<EnemyMoveToChara>();
        Enemy SelectEnemy = GetRandom(StageEnemys.Enemies);
        EnemyMoveToChara.Chara = Chara;
        EnemyMoveToChara.Enemy = SelectEnemy;

        Image ImageObject = Object.AddComponent<Image>();
        ImageObject.color = SelectEnemy.GetColor();
        ImageObject.preserveAspect = true;

        Rigidbody2D Rigidbody2DObject = Object.AddComponent<Rigidbody2D>();
        Rigidbody2DObject.mass = 0;
        Rigidbody2DObject.gravityScale = 0;

        CircleCollider2D CircleCollider2DObject = Object.AddComponent<CircleCollider2D>();
        CircleCollider2DObject.radius = 0.5f;

        Object.transform.SetParent(this.gameObject.transform);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(StageEnemys.SpawnInterval);
            Debug.Log(StageEnemys.SpawnInterval);
            SetSpawn();
        }
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
