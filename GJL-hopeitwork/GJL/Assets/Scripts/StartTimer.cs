﻿using UnityEngine;
using TMPro;

public class StartTimer : MonoBehaviour
{

    [SerializeField] private TMP_Text timerText;

    private float timerReset = 120f;
    
    public static float timerUsed = 120f;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = timerReset.ToString("F2");
        timerUsed = timerReset;
    }

    // Update is called once per frame
    void Update()
    {
        TimerMethod();    
    }

    private void TimerMethod()
    {
        if (Time.timeScale > 0 && timerUsed > 0)
        {
            timerUsed -= Time.deltaTime;
            timerText.text = timerUsed.ToString("F2");
        }
        else
        {
            timerText.text = "ope";
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}
