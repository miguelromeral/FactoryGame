using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject character;

    private Player playerScript;

    private Rigidbody2D characterBody;
    private float ScreenWidth;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        ScreenWidth = Screen.width;
        moveSpeed = playerScript.speed;
        characterBody = character.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (playerScript != null)
        {
            Vector3 px = Camera.main.WorldToScreenPoint(playerScript.transform.position);
            //Debug.Log("PX: " + px.x);


            if (Input.touchCount > 0 && !playerScript.IsFrozen)
            {
                if (Input.GetTouch(0).position.x > ScreenWidth / 2)
                {
                    if ((px.x + moveSpeed * 1f) < ScreenWidth)
                        RunCharacter(1.0f);
                    else
                        RunCharacter(0f);
                }
                if (Input.GetTouch(0).position.x < ScreenWidth / 2)
                {

                    if ((px.x + moveSpeed * -1f) > 0)
                        RunCharacter(-1.0f);
                    else
                        RunCharacter(0f);
                }
            }
            else
            {
                RunCharacter(0f);
            }
        }
    }

    private void RunCharacter(float horizontalInput)
    {
        if (characterBody != null)
        {
            characterBody.velocity = new Vector2(horizontalInput * moveSpeed,
                characterBody.velocity.y);
            playerScript.Running(horizontalInput);
        }
    }
}
