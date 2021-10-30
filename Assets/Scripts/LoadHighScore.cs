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
        GameObject.Find("Time").GetComponent<Text>().text = "Time: " + GameObject.Find("UserPrefrabs").GetComponent<Text>().text.Split(char.Parse(" "))[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
