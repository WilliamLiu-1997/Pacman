using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Highest Score").GetComponent<Text>().text = "Highest Score: " + GameObject.Find("UserPrefrabs").GetComponent<Text>().text.Split(char.Parse(" "))[0];
        TimeSpan timeSpan = TimeSpan.FromSeconds(float.Parse(GameObject.Find("UserPrefrabs").GetComponent<Text>().text.Split(char.Parse(" "))[1]));
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        GameObject.Find("Time").GetComponent<Text>().text = "Time: " + timeText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
