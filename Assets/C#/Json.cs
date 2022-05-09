using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Json : MonoBehaviour
{
    static public Json instance;
    string datapath;

    [System.Serializable] //定義したクラスをJSONデータに変換できるようにする
    public class PlayerData
    {
        public bool FirstTime;
        public int Money;
        public int Latest_Chara;
        public int Sound;
        public int Music;
        public bool Flash;
        public bool JoyStick;
        public bool Damage;
        public List<CharacterData> Character;
    }

    [System.Serializable]
    public class CharacterData
    {
        public int id;
        public bool use;
        public bool hidden;
    }

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    private void Awake()
    {
        datapath = Application.dataPath + "/json/Player.json";
        //初めに保存先を計算する　Application.dataPathで今開いているUnityプロジェクトのAssetsフォルダ直下を指定して、後ろに保存名を書く
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        PlayerData player = Load();
        //Debug.Log(JsonUtility.ToJson(player, true));
        if (player.FirstTime == false)
        {
            player.FirstTime = true;
            player.Money = 0;
            player.Money = 0;
            player.Sound = 100;
            player.Music = 100;
            player.Flash = true;
            player.JoyStick = false;
            player.Damage = true;
            for (int i = 0; i < MobDataBase.GetMobLists().Count; i++)//  Mobのリストの数の分だけ繰り返す処理
            {
                CharacterData character = new CharacterData();
                character.id = MobDataBase.GetMobLists()[i].GetId();
                character.use = MobDataBase.GetMobLists()[i].GetUse();
                character.hidden = MobDataBase.GetMobLists()[i].GetHidden();
                player.Character.Add(character);
            };
            Save(player);
        };
    }

    public PlayerData Load()
    {
        StreamReader reader = new StreamReader(datapath); //受け取ったパスのファイルを読み込む
        string datastr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        reader.Close();//ファイルを閉じる
        return JsonUtility.FromJson<PlayerData>(datastr);
    }
    public void Save(PlayerData player)
    {
        string jsonstr = JsonUtility.ToJson(player, true);//受け取ったPlayerDataをJSONに変換
        StreamWriter writer = new StreamWriter(datapath, false);//初めに指定したデータの保存先を開く
        writer.WriteLine(jsonstr);//JSONデータを書き込み
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする
    }
}