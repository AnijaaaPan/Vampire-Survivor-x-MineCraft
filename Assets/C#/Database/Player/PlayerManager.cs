using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private PlayerDataBase PlayerDataBase;//  使用するデータベース

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PlayerDataBase.GetPlayerLists().Count; i++)//  Playerのリストの数の分だけ繰り返す処理
        {
            PlayerDataBase.GetPlayerLists()[i].SetMoney(PlayerDataBase.GetPlayerLists()[i].GetMoney() + 1);
            Debug.Log(PlayerDataBase.GetPlayerLists()[i].GetMoney());//　確認の為のデータ出力
        }
        EditorUtility.SetDirty(PlayerDataBase);
        AssetDatabase.SaveAssets();
    }
 }