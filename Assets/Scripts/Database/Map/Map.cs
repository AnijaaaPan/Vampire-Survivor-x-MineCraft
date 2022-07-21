using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MapParameter
{
    public float StartingSpawns; // 一番最初にスポーンする数
    public float MoveSpeed; // キャラの移動速度
    public float EnemyMoveSpeed; // 敵の移動速度
    public float ProjectileSpeed; // 投射速度
    public float EmeraldBonus; // エメラルドボーナス
    public float LuckBonus; // ラッキーボーナス
    public float EnemyMinimum; // 敵の出現倍率
}

[System.Serializable]
public class HyperMode
{
    public float StartingSpawns; // 一番最初にスポーンする数
    public float MoveSpeed; // キャラの移動速度
    public float EnemyMoveSpeed; // 敵の移動速度
    public float ProjectileSpeed; // 投射速度
    public float EmeraldBonus; // エメラルドボーナス
    public float LuckBonus; // ラッキーボーナス
    public float EnemyMinimum; // 敵の出現倍率
}

[System.Serializable]
public class Stageitems
{
    public float x; // x座標
    public float y; // y座標
    public Item item; // Itemの指定
    public float Chance; // アイテムがある確率
}

[System.Serializable]
public class BossEnemys
{
    public Enemy Enemy; // どの敵がボスとして登場するか
    public List<string> Treasure; // その敵から落ちる宝箱の中身
}

[System.Serializable]
public class EventEnemy
{
    public Enemy Enemy; // どの敵がイベントで出現するか
    public float MoveSpeed; // そのイベントの時の敵の移動速度
}

[System.Serializable]
public class Events
{
    public string Type; // イベントのタイプ
    public int AmountPer; // 敵の出現数
    public EventEnemy EventEnemy; // そのイベントでの敵の情報
    public float Chance; // そのイベントが発生する確率
    public int MaxRepeats; // そのイベントが繰り返しで発生する回数
    public float Delay; // 繰り返されるまでの秒数    
}

[System.Serializable]
public class StageEnemys
{
    public int phase; // フェーズ(n分目)
    public List<Enemy> Enemies; // その時に出現する敵データのリスト
    public int EnemyCount; // 敵が出現する基礎数
    public float SpawnInterval; // 敵が出現する頻度(秒数)
    public List<BossEnemys> BossEnemys; // ボスの情報と宝箱
    public List<Events> Events; // そのフェーズにどんなイベントがあるか
}

[System.Serializable]
public class EnemySpawn
{
    public bool North;
    public bool South;
    public bool West;
    public bool East;
}

[CreateAssetMenu(fileName = "Map", menuName = "CreateMap")]
public class Map : ScriptableObject
{
    [SerializeField]
    private string name; // Mapの名前

    [SerializeField]
    private string type; // Mapのタイプ

    [SerializeField]
    private int id; // MapのID

    [SerializeField]
    private string description; // Mapの説明

    [SerializeField]
    private Sprite icon; // Mapのアイコン

    [SerializeField]
    private Sprite block_icon; // Mapのブロックアイコン

    [SerializeField]
    private bool use; // 遊べるMAPか否か

    [SerializeField]
    private AudioClip music; // MAPで流れる音楽

    [SerializeField]
    private MapParameter MapParameter; // Mapのparameter

    [SerializeField]
    private HyperMode HyperMode; // MapのHyperMode状態の追加parameter

    [SerializeField]
    private float LightSourceChance; // 光源がスポーンする確率

    [SerializeField]
    private EnemySpawn EnemySpawn; // 敵がスポーンする場所

    [SerializeField]
    private List<Stageitems> Stageitems; // Stageitemsの設定

    [SerializeField]
    private List<StageEnemys> StageEnemys; // StageEnemysの設定

    public string GetName()
    {
        return name;
    }

    public string GetType()
    {
        return type;
    }

    public int GetId()
    {
        return id;
    }

    public string GetDescription()
    {
        return description;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Sprite GetBlockIcon()
    {
        return block_icon;
    }

    public bool GetUse()
    {
        return use;
    }

    public AudioClip GetMusic()
    {
        return music;
    }

    public float GetLightSourceChance()
    {
        return LightSourceChance;
    }

    public MapParameter GetParameter()
    {
        return MapParameter;
    }

    public HyperMode GetHyperParameter()
    {
        return HyperMode;
    }
    public EnemySpawn GetEnemySpawn()
    {
        return EnemySpawn;
    }

    public List<Stageitems> GetStageitems()
    {
        return Stageitems;
    }

    public List<StageEnemys> GetStageEnemys()
    {
        return StageEnemys;
    }
}