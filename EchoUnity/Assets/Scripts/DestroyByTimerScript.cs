using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimerScript : MonoBehaviour {

    private float timer;
    public float lifespan;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer >= lifespan)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
	}
}
