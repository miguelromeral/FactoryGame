using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var prob = Random.Range(0, 2);
        //Debug.Log("Prob: " + prob);
        if(prob < 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Changed color!");
            Camera.main.backgroundColor = new Color(0.9f, 0.9f, 0.9f, 1f);
        }
    }
    
}
