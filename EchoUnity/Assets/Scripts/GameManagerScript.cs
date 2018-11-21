using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public Text scoreText;
    public bool timerPoints;
    public AudioClip addPointsSound;

    private int score;
    private float timerPointsPerSecond;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        timerPointsPerSecond = 0;
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if(timerPoints)
        {
            if(timerPointsPerSecond >= 1)
            {
                score+=1;
                timerPointsPerSecond = 0;
            }
            else
            {
            timerPointsPerSecond += Time.deltaTime;
            }
        }

        scoreText.text = "Score: " + score;
	}

    public void addPoints(int points)
    {
        audioSource.PlayOneShot(addPointsSound);
        score += points;
    }
}
