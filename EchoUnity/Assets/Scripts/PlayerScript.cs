using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
   
    public float speed;
    public float maxY;
    public float minY;
    public float maxX;
    public float minX;
    public float xOffset;
    public float yOffset;
    public float fireRate;
    public GameObject wave;

    private const float NORMALIZE = .01f;
    private Rigidbody2D playerRB;
    private bool canFire;

    // Use this for initialization
    void Start () {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        canFire = true;
	}
	
	// Update is called once per frame
	void Update () {
        getInput();
    }

    private void getInput()
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
            canFire = false;
        }

        playerRB.transform.position += new Vector3(xVel * NORMALIZE, yVel * NORMALIZE, 0.0f);
    }
}
