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
    public Text totalTimeText;
    public GameObject losePanel;
    public Image lifeImage;

    public float MaxLife = 100;

    Rigidbody2D rb;

    private Animator anim;
    
    private float input;

    public float MaxBarSize = 80f;

    static public int Health;
    static public int Points = 0;

    static public float totalTime = 0;

    public float Strength;

    public float PenaltyTime = 0f;
    public float DefaultPenalty = 5f;

    public bool IsFrozen;
    public bool IsFaster;
    public float FasterSpeed = 20f;
    public float DashSpeed = 30f;

    public bool IsSlowed;
    public bool IsProtected;


    public GameObject dashMove;

    public static void InitPlayer()
    {
        Health = 3;
        Points = 0;
        totalTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lifeText.text = Health.ToString();
        UpdatePoints();
        speed = DefaultSpeed;
        penaltyText.gameObject.SetActive(false);
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
        penaltyText.gameObject.SetActive(true);
    }

    public void Faster()
    {
        IsFaster = true;
        speed = FasterSpeed;
        PenaltyTime = DefaultPenalty;
        penaltyText.text = PenaltyTime.ToString();
        penaltyText.gameObject.SetActive(true);
    }

    public void QuickRunBegin()
    {
        speed = DashSpeed;
        Instantiate(dashMove, transform.position, Quaternion.identity);
    }
    public void QuickRunEnd()
    {
        speed = DefaultSpeed;
    }

    public void SlowEverything()
    {
        IsSlowed = true;
        PenaltyTime = DefaultPenalty;
        penaltyText.text = PenaltyTime.ToString();
        penaltyText.gameObject.SetActive(true);
    }

    public void Protect()
    {
        IsProtected = true;
        PenaltyTime = DefaultPenalty;
        penaltyText.text = PenaltyTime.ToString();
        penaltyText.gameObject.SetActive(true);
    }


    private void UpdatePoints()
    {
        pointsText.text = Points.ToString();
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
        if (Strength > 0f)
        {
            Strength -= Time.deltaTime;
        }
        UpdateLifeBar();
        //pointsText.text = Strength.ToString();
        if(Strength <= 0f)
        {
            Strength = 0f;
            UpdateLifeBar();
            losePanel.SetActive(true);
            totalTimeText.text = "Total Time: "+ totalTime.ToString("#.#", System.Globalization.CultureInfo.InvariantCulture) + " s.";
            if (Health <= 0)
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
            if (IsFrozen || IsFaster || IsSlowed || IsProtected)
            {
                IsFrozen = false;
                IsFaster = false;
                IsSlowed = false;
                IsProtected = false;
                speed = DefaultSpeed;
                penaltyText.gameObject.SetActive(false);
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
