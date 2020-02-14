using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] Backgrounds;
    public GameObject Background;

    public Camera Camera;

    void Start()
    {
        int randomRange = Random.Range(0, Backgrounds.Length);
        Background = Backgrounds[randomRange];
        //Debug.Log("Random Background: " + randomRange);
        Instantiate(Background, transform.position, Quaternion.Euler(0, 0, 0));


        Color[] colors = {
                    new Color(0.04f, 0.69f, 1f, 1f),
                    new Color(0.05f, 0.05f, 0.05f, 1f)
                };
        Camera.backgroundColor = colors[Random.Range(0, colors.Length)];
        //Debug.Log("Random Color!");
    }
}
