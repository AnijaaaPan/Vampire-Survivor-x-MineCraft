using UnityEngine;

[System.Serializable]
public class MapParameter
{
    public int MoveSpeed; // キャラの移動速度
    public int EmeraldBonus; // エメラルドボーナス
    public int LuckBonus; // ラッキーボーナス
}

[System.Serializable]
public class HyperMode
{
    public int MoveSpeed; // キャラの移動速度
    public int EmeraldBonus; // エメラルドボーナス
    public int LuckBonus; // ラッキーボーナス
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

    public MapParameter GetParameter()
    {
        return MapParameter;
    }

    public HyperMode GetHyperParameter()
    {
        return HyperMode;
    }
}