using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Timer : MonoBehaviour
{
    public Text currentTimeText;
    float currentTime;
    public Text timeFinish;
    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if(PlayerManager.isWin == false)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss");
        timeFinish.text = time.ToString(@"mm\:ss");
    }
}
