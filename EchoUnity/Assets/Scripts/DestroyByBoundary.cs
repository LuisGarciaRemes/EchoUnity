using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    public float minX;

    //Destroys the gameObject when object has passed the threshold
    void Update () {
		if(transform.position.x <= minX)
        {
            Destroy(gameObject);
        }
	}
}
