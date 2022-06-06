using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItem", menuName = "CreateSpecialItem")]//  CreateからCreateSpecialItemというメニューを表示し、SpecialItemを作成する
public class SpecialItem : ScriptableObject
{
    [SerializeField]
    private string name; //SpecialItemの名前

    [SerializeField]
    private string description; //SpecialItemの説明その1

    [SerializeField]
    private string effect; //SpecialItemの説明その2

    [SerializeField]
    private int id; //SpecialItemのID

    [SerializeField]
    private Sprite icon; //SpecialItemのアイコン

    public string GetName() //名前を入力したら、
    {
        return name; // nameに返す
    }
    public string GetDescription() //説明その1を入力したら、
    {
        return description; // descriptionに返す
    }
    public string GetEffect() //説明その2を入力したら、
    {
        return effect; // effectに返す
    }
    public int GetId() //IDを入力したら、
    {
        return id; // idに返す
    }
    public Sprite GetIcon() //アイコンを入力したら、
    {
        return icon; // iconに返す
    }
}