using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int Minutes;
    [SerializeField]
    private int Seconds;
    [SerializeField]
    private Text text;

    private void Start()
    {
        InvokeRepeating("ClockTimer", 0.0f, 1.0f);
    }

    void ClockTimer()
    {
        Seconds--;
        if (Seconds <= 0)
        {
            Minutes--;
            Seconds = 59;
        }
        if (Seconds <= 0 && Minutes <= 0)
        {
            Time.timeScale = 0;
        }
        SetText();
    }

    void SetText()
    {
        text.text = Minutes + ":" + Seconds;
    }
}
