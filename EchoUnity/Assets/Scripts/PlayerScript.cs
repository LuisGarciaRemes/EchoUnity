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

    private const float NORMALIZE = .01f;
    private Rigidbody2D playerRB;
    private bool canFire;
    private Sprite anim;
    private float timer;

    // Initialized variables
    void Start () {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<SpriteRenderer>().sprite;
        canFire = true;
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        ColliderController();
        CheckFire();
    }

    private void ColliderController()
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

    void CheckFire()
    {
        if(!canFire)
        {
            if (timer <= 0)
            {
                canFire = true;
            }
            else
            {
                timer -= Time.deltaTime;
            }
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
            timer = fireRate;
            canFire = false;
        }

        playerRB.transform.position += new Vector3(xVel * NORMALIZE, yVel * NORMALIZE, 0.0f);
    }
}
