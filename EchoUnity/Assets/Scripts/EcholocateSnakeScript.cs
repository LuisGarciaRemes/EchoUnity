using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcholocateSnakeScript : MonoBehaviour {

    public float timeLapse;
    public float echoVol;
    public AudioClip echoSound;
    public Anima2D.SpriteMeshInstance spriteMesh;

    private Color color;
    private AudioSource audioSource;

    //Makes gameObject invisible
    void Start()
    {
        spriteMesh.color = new Color(1, 1, 1, 0);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    //Updates the opacity
    void Update()
    {

        if (spriteMesh.color.a > 0 && !GameManagerScript.instance.paused)
        {
            color = spriteMesh.color;
            color.a -= (1 / (timeLapse)) * Time.deltaTime;
            spriteMesh.color = color;
        }
    }

    //Makes the gameObject visible when hit by Wave
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wave") && !GameManagerScript.instance.paused)
        {
            audioSource.PlayOneShot(echoSound, echoVol);
            spriteMesh.color = new Color(1, 1, 1, 1);
        }
    }

}
