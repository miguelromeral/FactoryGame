using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    static public OptionsManager Options;

    //public int Health = 3;


    private void Awake()
    {
        if (Options == null)
        {
            Options = this;
            Debug.Log("Options initiated.");
            DontDestroyOnLoad(Options);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
