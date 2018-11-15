using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocateScript : MonoBehaviour {

    public float timeLapse;
    private Color color;

    //Makes gameObject invisible
	void Start () {
       gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
	}
	
    //Updates the opacity
	void Update () {

        if (gameObject.GetComponent<SpriteRenderer>().color.a > 0)
        {
            color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a -= (1 / (timeLapse))*Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    //Makes the gameObject visible when hit by Wave
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wave"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

}
