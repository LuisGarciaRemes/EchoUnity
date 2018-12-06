using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    // singleton instance
    public static PlayerScript instance;
   
    public float speed;
    public float maxY;
    public float minY;
    public float maxX;
    public float minX;
    public float fireRate;
    public GameObject wave;
    public Sprite[] sprites;
    public GameObject[] colliders;
    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float hurtTime;
    public AudioClip hurtSound;
    public AudioClip tangleSound;
    public AudioClip lifeUpSound;
    public AudioClip splatSound;
    public float vol;

    private GameObject gameManager;
    private const float NORMALIZE = .01f;
    private const float MAXLIVES = 5;
    private Rigidbody2D playerRB;
    private bool canFire;
    private Sprite anim;
    private float fireTimer;
    private float numHearts;
    private bool isHurt;
    private float hurtTimer;
    private float nerfTimer;
    private bool canBeNerfed;
    private float orgSpeed;
    private AudioSource audioSource;

    internal bool canTakeDamage;

    // Initialized variables
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<SpriteRenderer>().sprite;
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager");
        canFire = true;
        canBeNerfed = true;
        canTakeDamage = true;
        isHurt = false;
        numHearts = 5;
        orgSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.GetComponent<GameManagerScript>().paused)
        {
            GetInput();
            UpdateCollider();
            CheckFire();
            UpdateLifeBar();
            DeathByBoundary();
            UpdateColor();
            CheckSpeed();
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void UpdateCollider()
    {
        anim = gameObject.GetComponent<SpriteRenderer>().sprite;

        if(anim.Equals(sprites[0]))
        {
            colliders[3].SetActive(false);
            colliders[0].SetActive(true);
        }
        else if(anim.Equals(sprites[1]))
        {
            colliders[0].SetActive(false);
            colliders[1].SetActive(true);
        }
        else if (anim.Equals(sprites[2]))
        {
            colliders[1].SetActive(false);
            colliders[2].SetActive(true);
        }
        else if (anim.Equals(sprites[3]))
        {
            colliders[2].SetActive(false);
            colliders[3].SetActive(true);
        }

    }

    private void UpdateLifeBar()
    {
        for(int i = 0; i < MAXLIVES; i++ )
        {       
            if ( i < numHearts && numHearts > 0)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }

        if(numHearts > MAXLIVES)
        {
            numHearts = MAXLIVES;
        }

        if(numHearts <= 0)
        {
            gameManager.GetComponent<GameManagerScript>().GameOver();
        }
    }

    private void CheckFire()
    {
        if(!canFire)
        {
            if (fireTimer <= 0)
            {
                canFire = true;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
        }
    }

    private void UpdateColor()
    {
        if(isHurt)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            if(hurtTimer <= 0)
            {
                isHurt = false;
                canTakeDamage = true;
            }
            else
            {
                hurtTimer -= Time.deltaTime;
            }

        }
        else if(!canBeNerfed)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            hurtTimer = hurtTime;
            numHearts--;

            if (numHearts > 0)
            {
                audioSource.PlayOneShot(hurtSound, vol);
            }

            isHurt = true;
            canTakeDamage = false;
        }
    }

    public void ReduceSpeed(float nerfTime, float amountReduce)
    {
        if(canBeNerfed)
        {
            audioSource.PlayOneShot(tangleSound,vol*4);
            nerfTimer = nerfTime;
            speed -= amountReduce;
            canBeNerfed = false;
        }
    }

    private void CheckSpeed()
    {
        if (!canBeNerfed)
        {
            if(nerfTimer <= 0)
            {
                speed = orgSpeed;
                canBeNerfed = true;
            }
            else
            {
                nerfTimer -= Time.deltaTime;
            }
        }
    }

    public void AddHeart()
    {
        if (numHearts < 5)
        {
        numHearts++;
        }
        else
        {
            gameManager.GetComponent<GameManagerScript>().AddPoints(50); 
        }
        audioSource.PlayOneShot(lifeUpSound);
    }

    private void GetInput()
    {
        float xVel = 0.0f;
        float yVel = 0.0f;

        yVel = Input.GetAxis("Vertical") * -speed;

        xVel = Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Echo"))
        {
            if (canFire)
            {
                Instantiate(wave, new Vector3(playerRB.position.x + wave.transform.position.x, playerRB.position.y + wave.transform.position.y, 0.0f), Quaternion.identity);
                fireTimer = fireRate;
                canFire = false;
            }

        }

        playerRB.transform.position += new Vector3(xVel * NORMALIZE, yVel * NORMALIZE, 0.0f);
    }

    public void restart()
    {
        numHearts = 5;
        transform.position = new Vector3(-7f,0f,0f);
        isHurt = false;
        canBeNerfed = true;
        speed = orgSpeed;
        canTakeDamage = true;
        canFire = true;
    }

    public void PlaySplat()
    {
        audioSource.PlayOneShot(splatSound,vol*4);
    }

    private void DeathByBoundary()
    {
       if(playerRB.transform.position.x < minX)
        {
            TakeDamage();
            numHearts = 0;
        }
    }
 
}

