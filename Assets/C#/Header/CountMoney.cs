using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text�֘A�𑀍삷��Ƃ��ɕK�v�Ȗ��O���

public class CountMoney : MonoBehaviour
{
    string datapath;

    // Start is called before the first frame update
    void Start() { 
        Json.PlayerData player = Json.instance.Load();
        gameObject.GetComponent<Text>().text = player.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
