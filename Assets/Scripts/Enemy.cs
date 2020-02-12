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

    private float randomXdirection;
    private float rotationSpeed;

    Player playerScript;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        DefaultSpeed = Random.Range(minSpeed, maxSpeed);
        speed = DefaultSpeed;
        // We get the player given its tag.
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();


        // - half = -1
        // 0 = 0
        // half = 2
        float tempX = transform.position.x / 2;
        if(tempX > 0)
        {
            var limit = (tempX / 2) / 10f;
            randomXdirection = Random.RandomRange(-1f, limit);
            //Debug.Log("Position "+transform.position.x+" | Limit (-1, "+limit+")");
        }
        else
        {
            var limit = ((tempX / 2) / 10f);
            randomXdirection = Random.RandomRange(limit, 1f);
            //Debug.Log("Position " + transform.position.x + " | Limit (" + limit + ",1)");
        }

        rotationSpeed = Random.RandomRange(10f, 50f);

        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        /*
        float x = transform.position.x;
        float y = transform.position.y;

        transform.RotateAround(
            new Vector3(-1 * Time.deltaTime, 0, 0),
            new Vector3(0f, 0f, 1f), 70 * speed * Time.deltaTime);*/
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

        //speed = 0.5f;

        transform.Rotate(0f, 0f, rotationSpeed * speed * Time.deltaTime);
        //Vector2.down ==> new Vector2(0, -1)
        /*transform.Translate(
            new Vector2(1f * speed * Time.deltaTime,
            -1* speed * Time.deltaTime), Space.World);
            */
       
        
        //transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        transform.Translate(
            randomXdirection * speed * Time.deltaTime,
            -1 * speed * Time.deltaTime,
            0f,
            Space.World);
            
        /*
        float width = transform.localScale.x;
        float height = transform.localScale.y;

        transform.rotation = Quaternion.identity;
        transform.RotateAround(transform.position + 
            new Vector3(width / 2f, height / 2f, 0f), Vector3.forward, 1f);
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
                    AudioSource.PlayClipAtPoint(GetComponent<RandomSound>().GetRandom(), transform.position);
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
