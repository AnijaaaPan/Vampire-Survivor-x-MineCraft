using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SetTimer
{
    public float AllSecond = 0;
    public float Second = 0;
    public float Minute = 0;
}

public class CountTimer : MonoBehaviour
{
    static public CountTimer instance;

    private float sleep = 1f;
    private SetTimer SetTimer = new SetTimer();

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
        SetTimer.AllSecond++;
        SetTimer.Second = SetTimer.AllSecond % 60;
        SetTimer.Minute = Mathf.Floor(SetTimer.AllSecond / 60);
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        string MinuteText = SetTimer.Minute.ToString();
        string SecondText = SetTimer.Second.ToString();
        GetComponent<Text>().text = $"{MinuteText.PadLeft(2, '0')}:{SecondText.PadLeft(2, '0')}";
    }

    public SetTimer GetSetTimer()
    {
        return SetTimer;
    }
}
