using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "CreatePlayerDataBase")]//  CreateからCreatePlayerというメニューを表示し、Playerを作成する
public class PlayerDataBase : ScriptableObject
{

    [SerializeField]
    private List<Player> PlayerLists = new List<Player>();//  Playerのリストを新しく生成する

    public List<Player> GetPlayerLists()//  Playerのリストがあったら、
    {
        return PlayerLists;//  PlayerListsに返す
    }
}