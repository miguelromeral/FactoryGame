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
    public bool Good;

    public float MinAngle;
    public float MaxAngle;

    float speed;

    public GameObject explosion;

    Player playerScript;
    Rigidbody2D r;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        // We get the player given its tag.
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Good)
            {
                playerScript.Collide(points, boost);
            }
            else
            {
                playerScript.Collide(0, -damage);
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if (hitObject.tag == "Ground")
        {
            if (Good)
            {
                playerScript.Collide(0, 0);
            }
            else
            {
                playerScript.Collide(points, 0);
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if (hitObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
