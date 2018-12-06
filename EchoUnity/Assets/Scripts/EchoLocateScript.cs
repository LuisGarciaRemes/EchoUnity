using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocateScript : MonoBehaviour {

    public float timeLapse;
    public float echoVol;
    public AudioClip echoSound;

    private Color color;
    private AudioSource audioSource;
    private GameObject gameManager;

    //Makes gameObject invisible
    void Start () {
       gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager");
    }
	
    //Updates the opacity
	void Update () {

        if (gameObject.GetComponent<SpriteRenderer>().color.a > 0 && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a -= (1 / (timeLapse))*Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    //Makes the gameObject visible when hit by Wave
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wave") && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            audioSource.PlayOneShot(echoSound,echoVol);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else if(other.CompareTag("Player") && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

}
