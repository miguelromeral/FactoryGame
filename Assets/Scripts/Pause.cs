using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject panel;

    static public bool IsPaused;

    void Start()
    {
        IsPaused = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterPause()
    {
        //panel.gameObject.SetActive(true);
        //IsPaused = true;
        Debug.Log("PAUSE!!");
    }

    public void ExitPause()
    {
        //IsPaused = false;
        //panel.gameObject.SetActive(false);
        Debug.Log("EXIT PAUSE!!");
    }
}
