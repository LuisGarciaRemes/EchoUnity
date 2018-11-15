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

    private Vector3 orgPos;
    private const float NORMALIZE = .01f;
    private Rigidbody2D RB;

	//Gets Rerence for gameObjects Rigidbody
	void Start () {
        RB = gameObject.GetComponent<Rigidbody2D>();
        orgPos = RB.position;
	}
	
	//Moves the gameObject left or right and oscillates if oscillate option is checked
	void Update () {

		if(oscillate)
        {
            RB.transform.position += new Vector3(NORMALIZE * speedX, NORMALIZE * speedY, 0.0f);
        }
        else
        {
            RB.transform.position += new Vector3(NORMALIZE * speedX, 0.0f, 0.0f);
        }

        if(expand)
        {
            RB.transform.localScale += new Vector3(rateExpand* NORMALIZE, rateExpand* NORMALIZE, 0.0f);
        }

        if (RB.transform.position.y >= orgPos.y + amplitude / 2 || RB.transform.position.y <= orgPos.y - amplitude / 2)
        {
            speedY *= -1;
        }

	}
}
