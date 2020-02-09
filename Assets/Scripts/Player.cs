using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float DefaultSpeed = 10;
    public Text lifeText;
    public Text pointsText;
    public Text penaltyText;
    public GameObject losePanel;
    public Image lifeImage;

    public float MaxLife = 100;

    Rigidbody2D rb;

    private Animator anim;
    
    private float input;

    public float MaxBarSize = 80f;

    static public int Health;
    static public int Points = 0;

    public float Strength;

    public float PenaltyTime = 0f;
    public float DefaultPenalty = 5f;

    public bool IsFrozen;
    public bool IsFaster;
    public float FasterSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lifeText.text = Health.ToString();
        UpdatePoints();
        speed = DefaultSpeed;
        penaltyText.text = "0";
    }
    
    private void UpdateLifeBar()
    {
        float newWidth = (Strength * MaxBarSize) / MaxLife;
        lifeImage.transform.localScale = 
            new Vector2(newWidth, lifeImage.transform.localScale.y);
    }

    public void Running(float input)
    {
        anim.SetBool("isRunning", input != 0);
        if (input > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (input < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

        if(input != 0f)
        {
            Points += 1;
            UpdatePoints();
        }
    }

    public void Run(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        Running(horizontalInput);
    }

    public void Freeze()
    {
        IsFrozen = true;
        PenaltyTime = DefaultPenalty;
        penaltyText.text = PenaltyTime.ToString();
    }

    public void Faster()
    {
        IsFaster = true;
        speed = FasterSpeed;
        PenaltyTime = DefaultPenalty;
        penaltyText.text = PenaltyTime.ToString();
    }

    private void UpdatePoints()
    {
        pointsText.text = Points.ToString();
    }

    private void Update()
    {
        if (Strength > 0f)
        {
            Strength -= Time.deltaTime;
        }
        UpdateLifeBar();
        //pointsText.text = Strength.ToString();
        if(Strength <= 0f)
        {
            losePanel.SetActive(true);
            if(Health <= 0)
            {
                GameObject.Find("Start_Text").SetActive(false);
            }
            Destroy(gameObject);
        }
        if(PenaltyTime > 0f)
        {
            PenaltyTime -= Time.deltaTime;
            penaltyText.text = PenaltyTime.ToString("#.#", System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            if (IsFrozen || IsFaster)
            {
                IsFrozen = false;
                IsFaster = false;
                speed = DefaultSpeed;
                penaltyText.text = "0";
            }
        }

    }
    


    private void SetStrength(float addition)
    {
        if (Strength + addition > MaxLife)
            Strength = MaxLife;
        else
            Strength += addition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void Collide(int points, float boost)
    {
        SetStrength(boost);
        if(Strength > 0f)
        {
            Points += points;
        }
        /*
        if (health <= 0)
        {
            // DESTROY THE PLAYER
            health = 0;
            losePanel.SetActive(true);
            Destroy(gameObject);
        }
    */
        lifeText.text = Health.ToString();
        UpdatePoints();
        UpdateLifeBar();
    }
}
