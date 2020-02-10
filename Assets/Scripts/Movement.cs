using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject character;

    private Player playerScript;
    public Text dashText;

    private Rigidbody2D characterBody;
    private float ScreenWidth;

    public float DoubleTapGap = 1f;
    public float timeSinceLastTap = 0;
    public int lastDirection = 0; // -1 = L | 1 = R

    public float DefaultTimeBetweetnDoubleTaps = 2f;
    public float timeBetweenDoubleTaps = 0f;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        ScreenWidth = Screen.width;
        characterBody = character.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (playerScript != null)
        {
            Vector3 px = Camera.main.WorldToScreenPoint(playerScript.transform.position);
            
            if (Input.touchCount > 0 && !playerScript.IsFrozen)
            {
                if (Input.GetTouch(0).position.x > ScreenWidth / 2)
                {
                    if(lastDirection == 1 && Input.GetTouch(0).phase == TouchPhase.Began
                        && timeSinceLastTap > 0f && timeBetweenDoubleTaps <= 0f)
                    {
                        Debug.Log("Double Click Right!");
                        timeBetweenDoubleTaps = DefaultTimeBetweetnDoubleTaps;
                        timeSinceLastTap = 0f;

                        playerScript.QuickRunBegin();
                        CheckAndPerformMove(px.x, 1f);
                        playerScript.QuickRunEnd();
                    }
                    else
                    {
                        CheckAndPerformMove(px.x, 1f);
                    }
                    
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        lastDirection = 1;
                        timeSinceLastTap = DoubleTapGap;
                    }
                }
                if (Input.GetTouch(0).position.x < ScreenWidth / 2)
                {
                    if (lastDirection == -1 && Input.GetTouch(0).phase == TouchPhase.Began
                        && timeSinceLastTap > 0f && timeBetweenDoubleTaps <= 0f)
                    {
                        Debug.Log("Double Click Left!");
                        timeBetweenDoubleTaps = DefaultTimeBetweetnDoubleTaps;
                        timeSinceLastTap = 0f;

                        playerScript.QuickRunBegin();
                        CheckAndPerformMove(px.x, -1f);
                        playerScript.QuickRunEnd();
                    }
                    else
                    {
                        CheckAndPerformMove(px.x, -1f);
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        lastDirection = -1;
                        timeSinceLastTap = DoubleTapGap;
                    }
                }
            }
            else
            {
                RunCharacter(0f);
                if(timeSinceLastTap <= 0f)
                    lastDirection = 0;
            }
        }

        if(timeSinceLastTap > 0f)
        {
            timeSinceLastTap -= Time.deltaTime;
        }
        else
        {
            lastDirection = 0;
        }

        if(timeBetweenDoubleTaps > 0f)
        {
            timeBetweenDoubleTaps -= Time.deltaTime;
            dashText.text = timeBetweenDoubleTaps.ToString("#.#", System.Globalization.CultureInfo.InvariantCulture) + "s";
        }
        else
        {
            dashText.text = "Boost!";
        }
    }



    private void CheckAndPerformMove(float pxx, float input)
    {
        if(input > 0)
        {
            if ((pxx + playerScript.speed * input) < ScreenWidth)
                RunCharacter(input);
            else
                RunCharacter(0f);
        }
        else
        {
            if ((pxx + playerScript.speed * input) > 0)
                RunCharacter(input);
            else
                RunCharacter(0f);
        }
    }

    private void RunCharacter(float horizontalInput)
    {
        if (playerScript != null)
        {
            playerScript.Run(horizontalInput);
        }
    }
}
