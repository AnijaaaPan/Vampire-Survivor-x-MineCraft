using System.IO;
using System.Collections;
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
    private MobDataBase MobDataBase;//  �g�p����f�[�^�x�[�X

    private void Awake()
    {
        datapath = Application.dataPath + "/json/Player.json";
        //���߂ɕۑ�����v�Z����@Application.dataPath�ō��J���Ă���Unity�v���W�F�N�g��Assets�t�H���_�������w�肵�āA���ɕۑ���������
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
            for (int i = 0; i < MobDataBase.GetMobLists().Count; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
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