using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    public float minX;
    private Rigidbody2D RB;

    private void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
		if(RB.transform.position.x <= minX)
        {
            Destroy(gameObject);
        }
	}
}
