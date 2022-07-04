using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Json : MonoBehaviour
{
    static public Json instance;
    string datapath;

    [System.Serializable] //��`�����N���X��JSON�f�[�^�ɕϊ��ł���悤�ɂ���
    public class PlayerData
    {
        public bool FirstTime;
        public int Money;
        public int Latest_Chara;
        public int Latest_Map;
        public bool Latest_Map_Hyper;
        public float SoundEffect;
        public float SoundMusic;
        public bool Flash;
        public bool JoyStick;
        public bool Damage;
        public List<CharacterData> Character;
        public List<WeaponData> Weapon;
        public List<ItemData> Item;
        public PowerUpData PowerUp;
        public List<SpecialItemData> SpecialItem;
        public List<AchievementData> Achievement;
        public List<MapData> Map;
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

    [System.Serializable]
    public class MapData
    {
        public int id;
        public bool use;
        public bool hyper;
    }

    [SerializeField]
    public MobDataBase MobDataBase;//  �g�p����f�[�^�x�[�X

    [SerializeField]
    public WeaponDataBase WeaponDataBase;//  �g�p����f�[�^�x�[�X

    [SerializeField]
    public ItemDataBase ItemDataBase;//  �g�p����f�[�^�x�[�X

    [SerializeField]
    public SpecialItemDataBase SpecialItemDataBase;//  �g�p����f�[�^�x�[�X

    [SerializeField]
    public MapDataBase MapDataBase;//  �g�p����f�[�^�x�[�X

    private void Awake()
    {
        datapath = Application.dataPath + "/json/Player.json";
        //���߂ɕۑ�����v�Z����@Application.dataPath�ō��J���Ă���Unity�v���W�F�N�g��Assets�t�H���_�������w�肵�āA���ɕۑ���������
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
            player.Latest_Chara = 0;
            player.Latest_Map = 1;
            player.Latest_Map_Hyper = false;
            player.SoundEffect = 1f;
            player.SoundMusic = 1f;
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
        player = SaveAchievements(player);
        player = SaveMaps(player);
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

        for (int i = 0; i < MobDataBase.GetMobLists().Count; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
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

        for (int i = 1; i < WeaponDataBase.GetWeaponLists().Count + 1; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
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

        for (int i = 1; i < ItemDataBase.GetItemLists().Count + 1; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
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

        for (int i = 1; i < SpecialItemDataBase.GetSpecialItemLists().Count + 1; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
        {
            SpecialItem item_data = SpecialItemDataBase.FindSpecialItemFromId(i);
            if (player.SpecialItem.Count == i - 1)
            {
                SaveFunction(item_data);
            };

        };
        return player;
    }

    public PlayerData SaveAchievements(PlayerData player)
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

        SaveFunction(1, "���x��5�ɓ��B", "item", 10);
        SaveFunction(2, "���x��10�ɓ��B", "item", 13);
        SaveFunction(3, "�����̐X�Ń��x��20�ɓ��B", "map", 2);
        SaveFunction(4, "�ۊ�̐}���قŃ��x��40�ɓ��B", "map", 3);
        SaveFunction(5, "���_��Ń��x��60�ɓ��B", "map", 4);
        SaveFunction(6, "�����ꂩ�̃L�����N�^�[��1���Ԑ���", "item", 3);
        SaveFunction(7, "Pasqualina��5���Ԑ���", "weapon", 19);
        SaveFunction(8, "Gennaro��5���Ԑ���", "item", 4);
        SaveFunction(9, "�����ꂩ�̃L�����N�^�[��10���Ԑ���", "weapon", 25);
        SaveFunction(10, "Pungnala��10���Ԑ���", "weapon", 28);
        SaveFunction(11, "Pungnala��15���Ԑ���", "weapon", 29);
        SaveFunction(12, "Poppea���g���āA15���Ԑ���", "weapon", 33);
        SaveFunction(13, "Giovanna���g���āA15���Ԑ���", "weapon", 31);
        SaveFunction(14, "Concetta���g���āA15���Ԑ���", "weapon", 35);
        SaveFunction(15, "�����ꂩ�̃L�����N�^�[��20���Ԑ���", "weapon", 23);
        SaveFunction(16, "Krochi��20���Ԑ���", "item", 16);
        SaveFunction(17, "��10���ȏ�ŁA20���Ԉȏ㐶��", "chara", 6);
        SaveFunction(18, "Lama�ŁA30���Ԑ���", "item", 15);
        SaveFunction(19, "�{��Lv��4��", "item", 7);
        SaveFunction(20, "�|�[�V������Lv��4��", "item", 6);
        SaveFunction(21, "�u���C�Y�E���b�h��Lv��4��", "chara", 4);
        SaveFunction(22, "���̕���Lv��4��", "chara", 5);
        SaveFunction(23, "������Lv��7��", "item", 9);
        SaveFunction(24, "�{��Lv��7��", "weapon", 26);
        SaveFunction(25, "�G���h�E�N���X�^����Lv��7��", "item", 8);
        SaveFunction(26, "�ΒY��Lv��7��", "chara", 7);
        SaveFunction(27, "�G���h�E���b�h��Lv��7��", "chara", 11);
        SaveFunction(28, "1�x��6��ނ̕��������", "item", 5);
        SaveFunction(29, "���v1000HP��", "chara", 8);
        SaveFunction(30, "���̃Q�[����5000�R�C������", "chara", 9);
        SaveFunction(31, "20�̌�������", "weapon", 13);
        SaveFunction(32, "�X�e�[�L��5����", "weapon", 15);
        SaveFunction(33, "�����ȁE�N���[�o�[�����", "item", 12);
        SaveFunction(34, "�G���_�[�E�A�C�����", "item", 11);
        SaveFunction(35, "�������v�����", "weapon", 37);
        SaveFunction(36, "�R�}���h�E�u���b�N�����", "weapon", 9);
        SaveFunction(37, "���~�����", "item", 14);
        SaveFunction(38, "�����̐X�ɂāA���������ĊJ����", "chara", 12);
        SaveFunction(39, "�ۊ�̐}���قɂāA���������ĊJ����", "chara", 13);
        SaveFunction(40, "���_��ɂāA���������ĊJ����", "chara", 14);
        SaveFunction(41, "Gallo Tower�ɂāA���������ĊJ����", "chara", 15);
        SaveFunction(42, "TEST�����v3000�̓|��", "chara", 16);
        SaveFunction(43, "TEST�����v3000�̓|��", "chara", 17);
        SaveFunction(44, "TEST�����v3000�̓|��", "chara", 18);
        SaveFunction(45, "TEST�����v3000�̓|��", "chara", 19);
        SaveFunction(46, "�G�����v5,000�̓|��", "weapon", 21);
        SaveFunction(47, "�G�����v10,000�̓|��", "chara", 10);
        SaveFunction(48, "�����̐X�ɏo������TEST�����j", "hyper", 1);
        SaveFunction(49, "�ۊ�̐}���قɏo������TEST�����j", "hyper", 2);
        SaveFunction(50, "���_��ɏo������TEST�����j", "hyper", 3);
        SaveFunction(51, "Gallo Tower�ɏo������TEST�����j", "hyper", 4);
        SaveFunction(52, "�؂̌���i��", "money", 2);
        SaveFunction(53, "������i��", "money", 4);
        SaveFunction(54, "���i��", "money", 6);
        SaveFunction(55, "�|�[�V������i��", "money", 18);
        SaveFunction(56, "���̕���i��", "money", 22);
        SaveFunction(57, "�{��i��", "money", 12);
        SaveFunction(58, "�\���˂�i��", "money", 10);
        SaveFunction(59, "�u���C�Y�E���b�h��i��", "money", 14);
        SaveFunction(60, "�ΒY��i��", "money", 16);
        SaveFunction(61, "�G���h�E�N���X�^����i��", "money", 20);
        SaveFunction(62, "�G���h�E���b�h��i��", "money", 24);
        SaveFunction(63, "�{&�_�[�N�E�o�b�g������", "money", 27);
        SaveFunction(64, "�}�W�b�N&�N���C�W�[�E�����h������", "money", 30);
        SaveFunction(65, "�L��i��", "money", 32);
        SaveFunction(66, "�z����i��", "money", 34);
        SaveFunction(67, "�T�{�e����i��", "money", 36);
        return player;
    }

    public PlayerData SaveMaps(PlayerData player)
    {
        void SaveFunction(Map map_data)
        {
            MapData map = new MapData();
            map.id = map_data.GetId();
            map.use = map_data.GetUse();
            map.hyper = false;
            player.Map.Add(map);
        };

        for (int i = 1; i < MapDataBase.GetMapLists().Count + 1; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
        {
            Map item_data = MapDataBase.FindMapFromId(i);
            if (player.Map.Count == i - 1)
            {
                SaveFunction(item_data);
            };
        };
        return player;
    }

    public PlayerData Load()
    {
        StreamReader reader = new StreamReader(datapath); //�󂯎�����p�X�̃t�@�C����ǂݍ���
        string datastr = reader.ReadToEnd();//�t�@�C���̒��g�����ׂēǂݍ���
        reader.Close();//�t�@�C�������
        return JsonUtility.FromJson<PlayerData>(datastr);
    }

    public void Save(PlayerData player)
    {
        string jsonstr = JsonUtility.ToJson(player, true);//�󂯎����PlayerData��JSON�ɕϊ�
        StreamWriter writer = new StreamWriter(datapath, false);//���߂Ɏw�肵���f�[�^�̕ۑ�����J��
        writer.WriteLine(jsonstr);//JSON�f�[�^����������
        writer.Flush();//�o�b�t�@���N���A����
        writer.Close();//�t�@�C�����N���[�Y����
    }
}