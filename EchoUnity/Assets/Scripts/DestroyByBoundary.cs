using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    public float minX;
    private Rigidbody2D RB;

    //Gets reference of the Rigidbody on the gameObject
    private void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
    }

    //Destroys the gameObject when object has passed the threshold
    void Update () {
		if(RB.transform.position.x <= minX)
        {
            Destroy(gameObject);
        }
	}
}
