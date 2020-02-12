using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject panel;

    public bool IsPaused;

    public static Pause instance;

    void Start()
    {
        IsPaused = false;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
        /*else
        {
            Destroy(gameObject);
        }*/
    }
    

    public void EnterPause()
    {
        IsPaused = true;
        panel.gameObject.SetActive(true);
        Debug.Log("PAUSE!!");
    }

    public void ExitPause()
    {
        panel.gameObject.SetActive(false);
        IsPaused = false;
        Debug.Log("EXIT PAUSE!!");
    }
}
