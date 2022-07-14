using UnityEngine;

public class isPlaying : MonoBehaviour
{
    static public isPlaying instance;
    private bool isPlayGame = true;

    private void Awake()
    {
        instance = this;
    }

    public void Stop()
    {
        isPlayGame = false;
    }


    public void reStart()
    {
        isPlayGame = true;
    }

    public bool isPlay()
    {
        return isPlayGame;
    }
}
