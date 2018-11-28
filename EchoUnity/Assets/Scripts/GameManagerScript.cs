using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public Text scoreText;
    public bool timerPoints;
    public AudioClip addPointsSound;
    public AudioClip startSound;
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public float speedUpAmount;

    private GameObject echo;
    private int score;
    private float timerPointsPerSecond;
    private AudioSource audioSource;
    private bool gameOver;
    
    internal bool paused;
    internal int level;

    // Use this for initialization
    void Start () {
        echo = GameObject.Find("Echo");
        startScreen.SetActive(true);
        gameOver = false;
        paused = true;
        Time.timeScale = 0;
        timerPointsPerSecond = 0;
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        level = 0;
     }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return) && !gameOver)
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            startScreen.SetActive(paused);
            audioSource.PlayOneShot(startSound);
        }
        else if(Input.GetKeyDown(KeyCode.Return) && gameOver)
        {
            audioSource.PlayOneShot(startSound);
            paused = false;
            Time.timeScale = 1;
            gameOverScreen.SetActive(false);
            score = 0;
            echo.GetComponent<PlayerScript>().restart();
        }

        if (timerPoints)
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

    public void GameOver()
    {
        paused = true;
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void NextLevel()
    {
        level++;
    }
}
