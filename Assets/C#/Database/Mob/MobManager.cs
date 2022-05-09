using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MobManager : MonoBehaviour
{

    [SerializeField]
    private MobDataBase MobDataBase;//  使用するデータベース

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < MobDataBase.GetMobLists().Count; i++)//  Mobのリストの数の分だけ繰り返す処理
        {
            Debug.Log(MobDataBase.GetMobLists()[i]);//　確認の為のデータ出力
        }
    }
 }