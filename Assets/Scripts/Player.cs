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
    public Text totalTimeText;
    public Text remainText;
    public GameObject losePanel;
    public Image lifeImage;
    public Image lifeImageBack;
    public Image powerImage;
    public GameObject characterHelmet;
    public GameObject characterCoffee;

    public float MaxLife = 100;

    Rigidbody2D rb;

    private Animator anim;
    
    private float input;

    public float MaxBarSize = 60f;

    static public int Health;
    static public int Points = 0;

    static public float totalTime = 0;

    private float Strength;

    public float PenaltyTime = 0f;
    public float DefaultPenalty = 5f;

    public bool IsFrozen;
    public bool IsFaster;
    public float FasterSpeed = 20f;
    public float DashSpeed = 30f;

    public bool IsSlowed;
    public bool IsProtected;
    public bool IsHidden;

    public Image timeImage;
    public Image coffeeImage;
    public Image freezeImage;
    public Image helmetImage;
    public Image hiddenImage;

    public GameObject dashMove;

    private int count_boosts = 0;
    private int count_powers = 0;
    private int count_hit = 0;

    private SpriteRenderer[] bodySprites;


    public Text textHits;
    public Text textBoosts;
    public Text textPowers;
    public Text textPointsEnd;

    private AudioSource source;

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
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        lifeText.text = Health.ToString();
        UpdatePoints();
        speed = DefaultSpeed;
        Strength = MaxLife;
        bodySprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        
        lifeImageBack.transform.localScale = new Vector2(MaxBarSize, lifeImageBack.transform.localScale.y);
        
    }
    
    private void UpdateLifeBar()
    {
        float newWidth = Strength / MaxLife;
        lifeImage.transform.localScale = 
            new Vector2(newWidth, lifeImage.transform.localScale.y);
    }

    public void Running(float input)
    {
        anim.SetBool("isRunning", input != 0);
        anim.SetBool("IsFaster", IsFaster);
        Debug.Log("isRunning: " + (input != 0) + " | IsFaster: " + IsFaster);

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
        count_powers++;
        IsFrozen = true;
        PenaltyTime = DefaultPenalty;
        freezeImage.gameObject.SetActive(true);
        PaintBody(true);
    }

    public void Hide()
    {
        count_powers++;
        IsHidden = true;
        PenaltyTime = DefaultPenalty;
        hiddenImage.gameObject.SetActive(true);
        HideBody(true);
    }

    public void Faster()
    {
        count_powers++;
        IsFaster = true;
        speed = FasterSpeed;
        PenaltyTime = DefaultPenalty;
        coffeeImage.gameObject.SetActive(true);
        characterCoffee.SetActive(true);
    }

    public void QuickRunBegin()
    {
        speed = DashSpeed;
        //Instantiate(dashMove, transform.position, Quaternion.identity);
    }
    public void QuickRunEnd()
    {
        if (IsFaster)
        {
            speed = FasterSpeed;
        }
        else {
            speed = DefaultSpeed;
        }
    }

    public void SlowEverything()
    {
        count_powers++;
        IsSlowed = true;
        PenaltyTime = DefaultPenalty;
        timeImage.gameObject.SetActive(true);
    }

    public void Protect()
    {
        count_powers++;
        IsProtected = true;
        PenaltyTime = DefaultPenalty;
        helmetImage.gameObject.SetActive(true);
        characterHelmet.SetActive(true);
    }


    private void UpdatePoints()
    {
        pointsText.text = Points.ToString();
    }

    private void PaintBody(bool freeze = false)
    {

        foreach (SpriteRenderer s in bodySprites)
        {
            if (freeze)
            {
                s.color = new Color(0.33f, 0.67f, 255f, 1f);
            }
            else
            {
                s.color = new Color(1f, 1, 1f, 1f);
            }
        }
    }


    private void HideBody(bool hide = false)
    {
        foreach (SpriteRenderer s in bodySprites)
        {
            if (s.tag != "Shadow")
            {
                if (hide)
                {
                    s.color = new Color(1f, 1f, 1f, 0f);

                }
                else
                {
                    s.color = new Color(1f, 1, 1f, 1f);
                }
            }
        }
    }
    
    private void Update()
    {
        if (Pause.instance != null && Pause.instance.IsPaused)
        {
            return;
        }

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
            totalTimeText.text = "Tiempo Total: "+ totalTime.ToString("#.#", System.Globalization.CultureInfo.InvariantCulture) + " s.";
            if (Health <= 0)
            {
                GameObject.Find("Start_Text").SetActive(false);
            }

            textHits.text = "Golpes: " + count_hit.ToString();
            textBoosts.text = "Vidas: " + count_boosts.ToString();
            textPowers.text = "Poten.: " + count_powers.ToString();
            textPointsEnd.text = "Puntos: " + Points.ToString();
            
            Destroy(gameObject);
        }

        if(PenaltyTime > 0f)
        {
            PenaltyTime -= Time.deltaTime;
            
            powerImage.gameObject.SetActive(true);
            powerImage.transform.localScale = new Vector2(PenaltyTime / DefaultPenalty, powerImage.transform.localScale.y);
        }
        else
        {
            if (IsFrozen || IsFaster || IsSlowed || IsProtected || IsHidden)
            {
                IsFrozen = false;
                freezeImage.gameObject.SetActive(false);
                PaintBody();
                IsFaster = false;
                coffeeImage.gameObject.SetActive(false);
                characterCoffee.SetActive(false);
                IsSlowed = false;
                timeImage.gameObject.SetActive(false);
                IsProtected = false;
                helmetImage.gameObject.SetActive(false);
                characterHelmet.SetActive(false);
                speed = DefaultSpeed;
                IsHidden = true;
                HideBody();
                hiddenImage.gameObject.SetActive(false);

                powerImage.gameObject.SetActive(false);
            }
        }

        remainText.text = ((int) Strength).ToString();
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
        if(boost > 0f)
        {
            count_boosts++;
        }
        else if(boost < 0f)
        {
            count_hit++;
            source.Play();
        }

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
