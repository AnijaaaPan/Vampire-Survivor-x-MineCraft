using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "CreatePlayer")]//  CreateからCreatePlayerというメニューを表示し、Playerを作成する
public class Player : ScriptableObject
{
    [SerializeField]
    private int money; //所持金額

    [SerializeField]
    private int sound; //サウンド(SE)の音量

    [SerializeField]
    private int music; //音楽の音量

    [SerializeField]
    private bool flash; //点滅の有無

    [SerializeField]
    private bool joystick; //ジョイスティックの有無

    [SerializeField]
    private bool damage; //ダメージ表記の有無

    public int GetMoney() //所持金額を入力したら、
    {
        return money; // moneyに返す
    }
    public int GetSound() //サウンド(SE)の音量を入力したら、
    {
        return sound; // soundに返す
    }
    public int GetMusic() //音楽の音量を入力したら、
    {
        return music; // musicに返す
    }
    public bool GetFlash() //点滅の有無を入力したら、
    {
        return flash; // flashに返す
    }
    public bool GetJoystick() //ジョイスティックの有無を入力したら、
    {
        return joystick; // joystickに返す
    }
    public bool GetDamage() //ダメージ表記の有無を入力したら、
    {
        return damage; // damageに返す
    }

    public void SetMoney(int value) //所持金額を入力したら、
    {
        money = value; // moneyに返す
    }

}