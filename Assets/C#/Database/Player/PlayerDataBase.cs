using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "CreatePlayerDataBase")]//  Create����CreatePlayer�Ƃ������j���[��\�����APlayer���쐬����
public class PlayerDataBase : ScriptableObject
{

    [SerializeField]
    private List<Player> PlayerLists = new List<Player>();//  Player�̃��X�g��V������������

    public List<Player> GetPlayerLists()//  Player�̃��X�g����������A
    {
        return PlayerLists;//  PlayerLists�ɕԂ�
    }
}