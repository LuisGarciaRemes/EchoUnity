using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
   
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

    private const float NORMALIZE = .01f;
    private const float MAXLIVES = 5;
    private Rigidbody2D playerRB;
    private bool canFire;
    private Sprite anim;
    private float fireTimer;
    private float numHearts;
    private bool isHurt;
    private float hurtTimer;
    private bool canTakeDamage;


    // Initialized variables
    void Start () {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<SpriteRenderer>().sprite;
        canFire = true;
        canTakeDamage = true;
        isHurt = false;
        numHearts = 5;
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        UpdateCollider();
        CheckFire();
        UpdateLifeBar();
        UpdateColor();
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
            isHurt = true;
            canTakeDamage = false;
        }
    }

    private void GetInput()
    {
        float xVel = 0.0f;
        float yVel = 0.0f;

        if (Input.GetKey("down") && playerRB.transform.position.y > minY)
        {
            yVel = -speed;
        }

        if (Input.GetKey("up") && playerRB.transform.position.y < maxY)
        {
            yVel = speed;
        }
        if (Input.GetKey("left") && playerRB.transform.position.x > minX)
        {
            xVel = -speed;
        }
        if (Input.GetKey("right") && playerRB.transform.position.x < maxX)
        {
            xVel = speed;
        }

        if(Input.GetKey("space") && canFire)
        {
            Instantiate(wave,new Vector3(playerRB.position.x + wave.transform.position.x,playerRB.position.y + wave.transform.position.y, 0.0f),Quaternion.identity);
            fireTimer = fireRate;
            canFire = false;
        }

        playerRB.transform.position += new Vector3(xVel * NORMALIZE, yVel * NORMALIZE, 0.0f);
    }
 
}

