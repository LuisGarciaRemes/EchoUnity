using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimerScript : MonoBehaviour {

    private float timer;
    public float lifespan;

    //Sets timer to 0
	void Start () {
        timer = 0;
	}
	
    //Destroys gameObject when the timer reaches the specified time
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
