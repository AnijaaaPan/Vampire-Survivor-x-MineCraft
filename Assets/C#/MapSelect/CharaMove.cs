using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaMove : MonoBehaviour
{
    public GameObject CharaObject;
    public Image CharaImg;

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    Vector3 camera_pos;
    Vector3 chara_pos;
    Vector3 ImageLeftRight;
    Sprite[] CharaLists;
    int index = 0;
    int max_index;

    void Start()
    {
        Json.PlayerData player = Json.instance.Load();
        Mob mob = MobDataBase.FindMobFromId(player.Latest_Chara);
        CharaLists = mob.GetIcons();
        max_index = CharaLists.Length-1;
        //オブジェクトの現在の座標を入手
        camera_pos = Camera.main.transform.position;
        chara_pos = CharaObject.transform.position;
        ImageLeftRight = CharaObject.transform.localScale;
    }

    void Update()
    {
        bool value = CharaMoveBool();
        if ( value )
        {
            index++;
            if (Mathf.FloorToInt(index / 4) > max_index)
            {
                index = 0;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                camera_pos.x += 0.014f;
                chara_pos.x += 0.014f;
                ImageLeftRight.x = -1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                camera_pos.x -= 0.014f;
                chara_pos.x -= 0.014f;
                ImageLeftRight.x = 1;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                camera_pos.y += 0.014f;
                chara_pos.y += 0.014f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                camera_pos.y -= 0.014f;
                chara_pos.y -= 0.014f;
            }

            Camera.main.transform.position = camera_pos;
            CharaObject.transform.position = chara_pos;
            CharaObject.transform.localScale = ImageLeftRight;

        } else
        {
            index = 0;
        }

        CharaImg.sprite = CharaLists[Mathf.FloorToInt(index / 4)];
    }

    public bool CharaMoveBool()
    {
        if (Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || 
            Input.GetKey(KeyCode.UpArrow) || 
            Input.GetKey(KeyCode.DownArrow)) return true;
        return false;
    }
}
