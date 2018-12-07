using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundScript : MonoBehaviour {

    public AudioClip loopBG;
    public AudioClip BG;

    private AudioSource audioSource;
    private bool playLoop;
	// Use this for initialization
	void Start () {
        playLoop = false;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (audioSource.isPlaying == false && !playLoop)
        {
            audioSource.clip = BG;
            audioSource.Play();
            playLoop = true;
        }
        else if (audioSource.isPlaying == false && playLoop)
        {
            audioSource.clip = loopBG;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
