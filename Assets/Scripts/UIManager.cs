using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        DontDestroyOnLoad(GameObject.Find("UserPrefrabs"));
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("PacStudent");
    }
    public void LoadStartLevel()
    {
        DontDestroyOnLoad(GameObject.Find("UserPrefrabs"));
        SceneManager.LoadScene("StartScene");
    }

}
