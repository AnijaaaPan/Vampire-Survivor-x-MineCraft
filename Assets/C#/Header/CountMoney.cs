using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text�֘A�𑀍삷��Ƃ��ɕK�v�Ȗ��O���

public class CountMoney : MonoBehaviour
{

    [SerializeField]
    private PlayerDataBase PlayerDataBase;//  �g�p����f�[�^�x�[�X

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerDataBase.GetPlayerLists().Count; i++)//  Player�̃��X�g�̐��̕������J��Ԃ�����
        {
            gameObject.GetComponent<Text>().text = PlayerDataBase.GetPlayerLists()[i].GetMoney().ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
