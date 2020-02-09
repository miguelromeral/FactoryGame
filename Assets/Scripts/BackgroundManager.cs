using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] Backgrounds;
    public GameObject Background;

    void Start()
    {
        int randomRange = Random.Range(0, Backgrounds.Length);
        Background = Backgrounds[randomRange];
        //Debug.Log("Random Background: " + randomRange);
        Instantiate(Background, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
