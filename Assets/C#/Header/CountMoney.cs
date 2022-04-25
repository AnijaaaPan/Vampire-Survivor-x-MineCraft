using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text関連を操作するときに必要な名前空間

public class CountMoney : MonoBehaviour
{

    [SerializeField]
    private PlayerDataBase PlayerDataBase;//  使用するデータベース

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerDataBase.GetPlayerLists().Count; i++)//  Playerのリストの数の分だけ繰り返す処理
        {
            gameObject.GetComponent<Text>().text = PlayerDataBase.GetPlayerLists()[i].GetMoney().ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
