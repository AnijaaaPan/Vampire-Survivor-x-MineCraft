using UnityEngine;

[System.Serializable]
public class MapParameter
{
    public int MoveSpeed; //移動速度
    public int EmeraldBonus; //エメラルドボーナス
    public int LuckBonus; //ラッキーボーナス
}

[System.Serializable]
public class HyperMode
{
    public int MoveSpeed; //移動速度
    public int EmeraldBonus; //エメラルドボーナス
    public int LuckBonus; //ラッキーボーナス
}

[CreateAssetMenu(fileName = "Map", menuName = "CreateMap")]//  CreateからCreateMapというメニューを表示し、Mapを作成する
public class Map : ScriptableObject
{
    [SerializeField]
    private string name; //Mapの名前

    [SerializeField]
    private string type; //Mapのタイプ

    [SerializeField]
    private int id; //MapのID

    [SerializeField]
    private int sizeX; //Mapのx軸の大きさ

    [SerializeField]
    private int sizeY; //Mapのy軸の大きさ

    [SerializeField]
    private string description; //Mapの説明

    [SerializeField]
    private Sprite icon; //Mapのアイコン

    [SerializeField]
    private Sprite block_icon; //Mapのブロックアイコン

    [SerializeField]
    private bool use; //デフォルトから遊べるMAPか否か

    [SerializeField]
    private MapParameter MapParameter; //Mapのparameter

    [SerializeField]
    private HyperMode HyperMode; //MapのHyperMode状態の追加parameter

    public string GetName() //名前を入力したら、
    {
        return name; // nameに返す
    }
    public string GetType() //タイプを入力したら、
    {
        return type; // typeに返す
    }
    public int GetId() //IDを入力したら、
    {
        return id; // idに返す
    }
    public int GetSizeX() //Mapのx軸の大きさを入力したら、
    {
        return sizeX; // sizeXに返す
    }
    public int GetSizeY() //Mapのy軸の大きさを入力したら、
    {
        return sizeY; // sizeYに返す
    }
    public string GetDescription() //説明を入力したら、
    {
        return description; // descriptionに返す
    }
    public Sprite GetIcon() //アイコンを入力したら、
    {
        return icon; // iconに返す
    }
    public Sprite GetBlockIcon() //ブロックアイコンを入力したら、
    {
        return block_icon; // block_iconに返す
    }
    public bool GetUse() //デフォルトから遊べるMAPか否かを入力したら、
    {
        return use; // useに返す
    }
    public MapParameter GetParameter() //Mapのparameterの設定
    {
        return MapParameter; // MapParameterに返す
    }
    public HyperMode GetHyperParameter() //MapのHyperMode状態の追加parameterの設定
    {
        return HyperMode; // HyperModeに返す
    }
}