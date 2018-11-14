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

	// Use this for initialization
	void Start () {
        RB = gameObject.GetComponent<Rigidbody2D>();
        orgPos = RB.position;
	}
	
	// Update is called once per frame
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
