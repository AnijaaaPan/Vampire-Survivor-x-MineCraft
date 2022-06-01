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
        public List<WeaponData> Weapon;
        public List<ItemData> Item;
        public PowerUpData PowerUp;
        public List<SpecialItemData> SpecialItem;
        public List<AchievementData> Achievement;
    }

    [System.Serializable]
    public class CharacterData
    {
        public int id;
        public bool use;
        public bool hidden;
    }

    [System.Serializable]
    public class WeaponData
    {
        public int id;
        public bool use;
    }

    [System.Serializable]
    public class ItemData
    {
        public int id;
        public bool use;
    }

    [System.Serializable]
    public class SpecialItemData
    {
        public int id;
        public bool use;
    }

    [System.Serializable]
    public class PowerUpData
    {
        public int allcost;
        public int allcount;
        public List<PowerUpList> poweruplist;
    }

    [System.Serializable]
    public class PowerUpList
    {
        public int id;
        public int powerupcount;
    }


    [System.Serializable]
    public class AchievementData
    {
        public int id;
        public string type;
        public string description;
        public int type_id;
        public bool use;
    }

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    [SerializeField]
    private WeaponDataBase WeaponDataBase;//  使用するデータベース

    [SerializeField]
    private ItemDataBase ItemDataBase;//  使用するデータベース

    [SerializeField]
    private SpecialItemDataBase SpecialItemDataBase;//  使用するデータベース

    private void Awake()
    {
        datapath = Application.dataPath + "/json/Player.json";
        //初めに保存先を計算する　Application.dataPathで今開いているUnityプロジェクトのAssetsフォルダ直下を指定して、後ろに保存名を書く
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

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
            player.Sound = 100;
            player.Music = 100;
            player.Flash = true;
            player.JoyStick = false;
            player.Damage = true;
            player.PowerUp.allcost = 0;
            player.PowerUp.allcount = 0;
            player.PowerUp.poweruplist = new List<PowerUpList>();
        };
        player = SaveCharacters(player);
        player = SaveWeapons(player);
        player = SaveItems(player);
        player = SaveSpecialItems(player);
        player = SaveAchievement(player);
        Save(player);
    }

    public PlayerData SaveCharacters(PlayerData player)
    {
        void SaveFunction(Mob mob)
        {
            CharacterData character = new CharacterData();
            character.id = mob.GetId();
            character.use = mob.GetUse();
            character.hidden = mob.GetHidden();
            player.Character.Add(character);
        };

        for (int i = 0; i < MobDataBase.GetMobLists().Count; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Mob mob = MobDataBase.FindMobFromId(i);
            if (player.Character.Count == i)
            {
                SaveFunction(mob);
            };
        };
        return player;
    }

    public PlayerData SaveWeapons(PlayerData player)
    {
        void SaveFunction(Weapon weapon_data)
        {
            WeaponData weapon = new WeaponData();
            weapon.id = weapon_data.GetId();
            weapon.use = weapon_data.GetDefault();
            player.Weapon.Add(weapon);
        };

        for (int i = 1; i < WeaponDataBase.GetWeaponLists().Count + 1; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Weapon weapon_data = WeaponDataBase.FindWeaponFromId(i);
            if (player.Weapon.Count == i - 1)
            {
                SaveFunction(weapon_data);
            };

        };
        return player;
    }

    public PlayerData SaveItems(PlayerData player)
    {
        void SaveFunction(Item item_data)
        {
            ItemData item = new ItemData();
            item.id = item_data.GetId();
            item.use = item_data.GetDefault();
            player.Item.Add(item);

            PowerUpList special_item = new PowerUpList();
            special_item.id = item_data.GetId();
            special_item.powerupcount = item_data.GetCount();
            player.PowerUp.poweruplist.Add(special_item);
        };

        for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Item item_data = ItemDataBase.FindItemFromId(i);
            if (player.Item.Count == i - 1)
            {
                SaveFunction(item_data);
            };
        };
        return player;
    }

    public PlayerData SaveSpecialItems(PlayerData player)
    {
        void SaveFunction(SpecialItem item_data)
        {
            SpecialItemData item = new SpecialItemData();
            item.id = item_data.GetId();
            item.use = false;
            player.SpecialItem.Add(item);
        };

        for (int i = 1; i < SpecialItemDataBase.GetSpecialItemLists().Count + 1; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            SpecialItem item_data = SpecialItemDataBase.FindSpecialItemFromId(i);
            if (player.SpecialItem.Count == i - 1)
            {
                SaveFunction(item_data);
            };

        };
        return player;
    }

    public PlayerData SaveAchievement(PlayerData player)
    {
        void SaveFunction(int id, string description, string type, int type_id)
        {
            if (player.Achievement.Count == id - 1)
            {
                AchievementData achievement = new AchievementData();
                achievement.id = id;
                achievement.description = description;
                achievement.type = type;
                achievement.type_id = type_id;
                achievement.use = false;
                player.Achievement.Add(achievement);
            }
        };

        SaveFunction(1, "レベル5に到達する。", "item", 10);
        SaveFunction(2, "レベル10に到達する。", "item", 13);
        SaveFunction(3, "狂乱の森でレベル20に到達する。", "map", 2);
        SaveFunction(4, "象眼の図書館でレベル40に到達する。", "map", 3);
        SaveFunction(5, "酪農場でレベル60に到達する。", "map", 4);
        SaveFunction(6, "どのキャラクターでもいいので、1分間生存する。", "item", 3);
        SaveFunction(7, "Pasqualinaで5分間生存する。", "weapon", 20);
        SaveFunction(8, "Gennaroで5分間生存する。", "item", 4);
        SaveFunction(9, "どのキャラクターでもいいので、10分間生存する。", "weapon", 25);
        SaveFunction(10, "Pungnalaで10分間生存する。", "weapon", 28);
        SaveFunction(11, "Pungnalaで15分間生存する。", "weapon", 29);
        SaveFunction(12, "Poppeaを使って、15分間生存する。", "weapon", 33);
        SaveFunction(13, "Giovannaを使って、15分間生存する。", "weapon", 31);
        SaveFunction(14, "Concettaを使って、15分間生存する。", "weapon", 35);
        SaveFunction(15, "どのキャラクターでもいいので、20分間生存する。", "weapon", 23);
        SaveFunction(16, "Krochiで20分間生存する。", "item", 16);
        SaveFunction(17, "呪い10％以上で、20分間以上生存する。", "chara", 6);
        SaveFunction(18, "Lamaで、30分間生存する。", "item", 15);
        SaveFunction(19, "本のLvを4にする。", "item", 7);
        SaveFunction(20, "ポーションのLvを4にする。", "item", 6);
        SaveFunction(21, "ブレイズ・ロッドのLvを4にする。", "chara", 4);
        SaveFunction(22, "雷の斧のLvを4にする。", "chara", 5);
        SaveFunction(23, "松明のLvを7にする。", "item", 9);
        SaveFunction(24, "鶏のLvを7にする。", "weapon", 26);
        SaveFunction(25, "ビーコンのLvを7にする。", "item", 8);
        SaveFunction(26, "石炭のLvを7にする。", "chara", 7);
        SaveFunction(27, "エンド・ロッドのLvを7にする。", "chara", 11);
        SaveFunction(28, "1度に6種類の武器を所持する。", "item", 5);
        SaveFunction(29, "合計1000HP回復する。", "chara", 8);
        SaveFunction(30, "一回のゲームで5000コイン入手する。", "chara", 9);
        SaveFunction(31, "20個の光源を壊す。", "weapon", 13);
        SaveFunction(32, "ステーキを5個入手する。", "weapon", 15);
        SaveFunction(33, "小さな・クローバーを入手する。", "item", 12);
        SaveFunction(34, "エンダー・アイを入手する。", "item", 11);
        SaveFunction(35, "懐中時計を入手する。", "weapon", 37);
        SaveFunction(36, "コマンド・ブロックを入手する。", "weapon", 9);
        SaveFunction(37, "強欲を入手する。", "item", 14);
        SaveFunction(38, "狂乱の森にて、棺を見つけて開ける。", "chara", 12);
        SaveFunction(39, "象眼の図書館にて、棺を見つけて開ける。", "chara", 13);
        SaveFunction(40, "酪農場にて、棺を見つけて開ける。", "chara", 14);
        SaveFunction(41, "Gallo Towerにて、棺を見つけて開ける。", "chara", 15);
        SaveFunction(42, "TESTを合計3000体倒す。", "chara", 16);
        SaveFunction(43, "TESTを合計3000体倒す。", "chara", 17);
        SaveFunction(44, "TESTを合計3000体倒す。", "chara", 18);
        SaveFunction(45, "TESTを合計3000体倒す。", "chara", 19);
        SaveFunction(46, "敵を合計5,000体倒す", "weapon", 21);
        SaveFunction(47, "敵を合計10,000体倒す", "chara", 10);
        SaveFunction(48, "狂乱の森に出現するTESTを撃破する。", "map", 1);
        SaveFunction(49, "象眼の図書館に出現するTESTを撃破する。", "map", 2);
        SaveFunction(50, "酪農場に出現するTESTを撃破する。", "map", 3);
        SaveFunction(51, "Gallo Towerに出現するTESTを撃破する。", "map", 4);
        SaveFunction(52, "木の剣を進化させる", "money", 0);
        SaveFunction(53, "松明を進化させる", "money", 0);
        SaveFunction(54, "矢を進化させる", "money", 0);
        SaveFunction(55, "ポーションを進化させる", "money", 0);
        SaveFunction(56, "雷の斧を進化させる", "money", 0);
        SaveFunction(57, "本を進化させる", "money", 0);
        SaveFunction(58, "十字架を進化させる", "money", 0);
        SaveFunction(59, "ブレイズ・ロッドを進化させる", "money", 0);
        SaveFunction(60, "石炭を進化させる", "money", 0);
        SaveFunction(61, "ビーコンを進化させる", "money", 0);
        SaveFunction(62, "エンド・ロッドを進化させる", "money", 0);
        SaveFunction(63, "鶏とダーク・バットを合体させる", "money", 0);
        SaveFunction(64, "マジック・ワンドとクレイジー・ワンドを合体させる", "money", 0);
        SaveFunction(65, "猫を進化させる", "money", 0);
        SaveFunction(66, "額縁を進化させる", "money", 0);
        SaveFunction(67, "サボテンを進化させる", "money", 0);
        return player;
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