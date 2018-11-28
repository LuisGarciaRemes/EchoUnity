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
    public GameObject waveText;
    public float speedUpAmount;
    public float waveTextTime;
    public int pointsSpawnFruit;

    private GameObject echo;
    private int score;
    private float timerPointsPerSecond;
    private AudioSource audioSource;
    private bool gameOver;
    private float waveTextTimer;
    
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
        level = 1;
        waveTextTimer = 0;
     }
	
	// Update is called once per frame
	void Update () {
        GameStartControls();
        UpdatePoints();
        DisplayWaveText();
        CheckFruitSpawn();
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

    public void NextWave()
    {
        level++;
        waveText.GetComponent<Text>().text = "Wave: " + level;
        waveText.SetActive(true);
        waveTextTimer = 0;
    }

    private void DisplayWaveText()
    {
        if (waveTextTimer >= waveTextTime)
        {
            waveText.SetActive(false);
        }
        else
        {
            waveTextTimer += Time.deltaTime;
        }
    }

    private void UpdatePoints()
    {
        if (timerPoints)
        {
            if (timerPointsPerSecond >= 1)
            {
                score += 1;
                timerPointsPerSecond = 0;
            }
            else
            {
                timerPointsPerSecond += Time.deltaTime;
            }
        }
        scoreText.text = "Score: " + score;
    }

    private void CheckFruitSpawn()
    {
        if(score % pointsSpawnFruit == 0 && score != 0)
        {
            this.gameObject.GetComponent<SpawnerScript>().spawnLifeUp();
        }
    }

    private void GameStartControls()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !gameOver)
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            startScreen.SetActive(paused);
            audioSource.PlayOneShot(startSound);
        }
        else if (Input.GetKeyDown(KeyCode.Return) && gameOver)
        {
            audioSource.PlayOneShot(startSound);
            paused = false;
            Time.timeScale = 1;
            gameOverScreen.SetActive(false);
            score = 0;
            level = 0;
            echo.GetComponent<PlayerScript>().restart();
        }
    }
}
