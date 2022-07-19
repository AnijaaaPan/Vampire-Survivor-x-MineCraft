using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Timer
{
    public int AllSecond = 0;
    public int Second = 0;
    public int Minute = 0;
}

public class CountTimer : MonoBehaviour
{
    static public CountTimer instance;

    private readonly float sleep = 1f;
    private Timer Timer = new Timer();

    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(sleep);
            SecondCountUp();
        }
    }

    private void SecondCountUp()
    {
        if (!IsPlaying.instance.isPlay()) return;

        Timer.AllSecond++;
        Timer.Second = Timer.AllSecond % 60;
        Timer.Minute = (int)Mathf.Floor(Timer.AllSecond / 60);
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        string MinuteText = Timer.Minute.ToString();
        string SecondText = Timer.Second.ToString();
        GetComponent<Text>().text = $"{MinuteText.PadLeft(2, '0')}:{SecondText.PadLeft(2, '0')}";
    }

    public Timer GetTimer()
    {
        return Timer;
    }
}
