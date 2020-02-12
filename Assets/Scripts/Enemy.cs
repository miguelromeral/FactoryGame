using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public int damage;
    public int points;
    public int boost;
    
    float speed;
    private float DefaultSpeed;
    private float SlowSpeed = 4f;

    public GameObject explosion;
   

    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        DefaultSpeed = Random.Range(minSpeed, maxSpeed);
        speed = DefaultSpeed;
        // We get the player given its tag.
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.instance != null && Pause.instance.IsPaused)
        {
            return;
        }

        if (gameObject.tag == "Enemy")
        {
            if (playerScript.IsSlowed)
                speed = DefaultSpeed / SlowSpeed;
            else
                speed = DefaultSpeed;
        }

        // Vector2.down ==> new Vector2(0, -1)
        transform.Translate(Vector2.down * speed * Time.deltaTime);

            //transform.Rotate(0, 0, 5f);
            //r.rotation = r.angularVelocity * speed * Time.deltaTime;

            //r.velocity = new Vector2(r.velocity.x * speed * Time.deltaTime, 0f);
            //r.velocity = new Vector2(0f, r.velocity.y * speed);
        

    }

    void FixedUpdate()
    {
        /*
        float angle;
        Vector3 axis;
        Quaternion.AngleAxis(180, Vector3.up).ToAngleAxis(out angle, out axis);
        r.angularVelocity = axis.magnitude * angle;
        */
    }

    // Raised when the BOX COLLIDER 2D has checked "IsTrigger" and detects a collision.
    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        // We have to mark the Player object in Unity as "Player".
        if(hitObject.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "Enemy":
                    if (!playerScript.IsProtected)
                    {
                        playerScript.Collide(0, -damage);
                    }
                    else
                    {
                        playerScript.Collide(points, 0);
                    }
                    break;
                case "Boost":
                    playerScript.Collide(points, boost);
                    break;
                case "Power_Freeze":
                    playerScript.Collide(points, boost);
                    playerScript.Freeze();
                    break;
                case "Power_Fast":
                    playerScript.Collide(points, boost);
                    playerScript.Faster();
                    break;
                case "Power_Slow":
                    playerScript.Collide(points, boost);
                    playerScript.SlowEverything();
                    break;
                case "Power_Shield":
                    playerScript.Collide(points, boost);
                    playerScript.Protect();
                    break;
                case "Power_Hide":
                    playerScript.Collide(points, boost);
                    playerScript.Hide();
                    break;
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if (hitObject.tag == "Ground")
        {

            switch (gameObject.tag)
            {
                case "Enemy":
                    playerScript.Collide(points, 0);
                    break;
                default:
                    playerScript.Collide(0, 0);
                    break;
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if (hitObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
