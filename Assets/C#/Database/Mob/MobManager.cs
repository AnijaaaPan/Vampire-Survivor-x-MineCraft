using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MobManager : MonoBehaviour
{

    [SerializeField]
    private MobDataBase MobDataBase;//  �g�p����f�[�^�x�[�X

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < MobDataBase.GetMobLists().Count; i++)//  Mob�̃��X�g�̐��̕������J��Ԃ�����
        {
            Debug.Log(MobDataBase.GetMobLists()[i]);//�@�m�F�ׂ̈̃f�[�^�o��
        }
    }
 }