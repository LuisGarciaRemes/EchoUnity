using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcholocateSnakeScript : MonoBehaviour {

    public float timeLapse;
    public float echoVol;
    public AudioClip echoSound;

    private Color color;
    private AudioSource audioSource;
    private GameObject gameManager;

    //Makes gameObject invisible
    void Start()
    {
        gameObject.GetComponent<Anima2D.SpriteMeshInstance>().color = new Color(1, 1, 1, 0);
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager");
    }

    //Updates the opacity
    void Update()
    {

        if (gameObject.GetComponent<Anima2D.SpriteMeshInstance>().color.a > 0 && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            color = gameObject.GetComponent<Anima2D.SpriteMeshInstance>().color;
            color.a -= (1 / (timeLapse)) * Time.deltaTime;
            gameObject.GetComponent<Anima2D.SpriteMeshInstance>().color = color;
        }
    }

    //Makes the gameObject visible when hit by Wave
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wave") && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            audioSource.PlayOneShot(echoSound, echoVol);
            gameObject.GetComponent<Anima2D.SpriteMeshInstance>().color = new Color(1, 1, 1, 1);
        }
    }

}
