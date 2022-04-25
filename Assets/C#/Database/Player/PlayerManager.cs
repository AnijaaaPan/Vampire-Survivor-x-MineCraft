using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private PlayerDataBase PlayerDataBase;//  �g�p����f�[�^�x�[�X

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PlayerDataBase.GetPlayerLists().Count; i++)//  Player�̃��X�g�̐��̕������J��Ԃ�����
        {
            PlayerDataBase.GetPlayerLists()[i].SetMoney(PlayerDataBase.GetPlayerLists()[i].GetMoney() + 1);
            Debug.Log(PlayerDataBase.GetPlayerLists()[i].GetMoney());//�@�m�F�ׂ̈̃f�[�^�o��
        }
        EditorUtility.SetDirty(PlayerDataBase);
        AssetDatabase.SaveAssets();
    }
 }