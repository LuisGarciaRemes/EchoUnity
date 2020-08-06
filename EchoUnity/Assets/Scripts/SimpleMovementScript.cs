using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementScript : MonoBehaviour {

    public bool oscillate;
    public bool expand;
    public float amplitude;
    public float speedX;
    public float speedY;
    public float rateExpand;
    public float expandMax;

    private Vector3 orgPos;
    private float initSpeed;

    //Gets Rerence for gameObjects Rigidbody
    void Start () {
        orgPos = transform.position;
        initSpeed = speedX;
	}
	
	//Moves the gameObject left or right and oscillates if oscillate option is checked
	void Update () {

        if (!GameManagerScript.instance.paused) {
            if (oscillate)
            {
                transform.position += new Vector3(speedX, speedY, 0.0f) * Time.deltaTime;
                if (transform.position.y >= orgPos.y + amplitude / 2 || transform.position.y <= orgPos.y - amplitude / 2)
                {
                    speedY *= -1;
                }

            }
            else
            {
                transform.position += new Vector3(speedX, speedY, 0.0f) * Time.deltaTime;
            }

            if (expand && transform.localScale.x < expandMax)
            {
                transform.localScale += new Vector3(rateExpand, rateExpand, 0.0f) * Time.deltaTime;
            }

        }

        if (this.gameObject.CompareTag("Rock") || this.gameObject.CompareTag("Sticky"))
        {
            speedX = initSpeed - ((GameManagerScript.instance.level - 1)* GameManagerScript.instance.speedUpAmount);
        }
    }
}
