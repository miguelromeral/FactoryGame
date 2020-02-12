using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject character;

    private Player playerScript;

    private Rigidbody2D characterBody;
    private float ScreenWidth;

    public float DoubleTapGap = 1f;
    public float timeSinceLastTap = 0;
    public int lastDirection = 0; // -1 = L | 1 = R

    public static float DefaultTimeBetweetnDoubleTaps = 5f;
    public float timeBetweenDoubleTaps = 0f;


    public Image dashImage;
    public Image dashImageBack;
    

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        ScreenWidth = Screen.width;
        characterBody = character.GetComponent<Rigidbody2D>();

        dashImageBack.transform.localScale = new Vector2(1f, dashImageBack.transform.localScale.y);
    }
    
    void Update()
    {
        if (Pause.IsPaused)
        {
            RunCharacter(0f);
            return;
        }

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
        
        if (timeBetweenDoubleTaps > 0f)
        {
            timeBetweenDoubleTaps -= Time.deltaTime;
            
            dashImage.transform.localScale = new Vector2(timeBetweenDoubleTaps / DefaultTimeBetweetnDoubleTaps, dashImage.transform.localScale.y);
            
            dashImage.color = new Color(1f, 0f, 0f);
        }
        else
        {
            dashImage.transform.localScale = new Vector2(
                (timeBetweenDoubleTaps > 0f ? 0f : 1f)
                , dashImage.transform.localScale.y);


            dashImage.color = new Color(0f, 1f, 0f);
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
