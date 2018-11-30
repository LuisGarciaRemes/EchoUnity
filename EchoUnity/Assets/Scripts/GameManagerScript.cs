using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public Text menuText;
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
    public GameObject[] gameOverTexts;
    public GameObject[] pauseTexts;

    private GameObject echo;
    private int score;
    private float timerPointsPerSecond;
    private AudioSource audioSource;
    internal bool gameOver;
    private float waveTextTimer;
    private bool isLarger;

    internal bool die;
    internal bool paused;
    internal int level;

    // Use this for initialization
    void Start () {
        isLarger = false;
        echo = GameObject.Find("Echo");
        startScreen.SetActive(true);
        gameOver = false;
        paused = true;
        timerPointsPerSecond = 0;
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        level = 1;
        waveTextTimer = 0;

        UpdateScore();

        //HighScoreManager._instance.ClearLeaderBoard();
        HighScoreManager._instance.SaveHighScore("      ", 0);

        Time.timeScale = 0;
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
        CheckIfHighScore();
        die = true;
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
        if(die)
        {
            die = false;
        }

        if (Input.GetKeyDown(KeyCode.Return) && gameObject.GetComponent<GetPlayerNameScript>().enabled)
        {
            audioSource.PlayOneShot(startSound);
            HighScoreManager._instance.SaveHighScore(gameObject.GetComponent<GetPlayerNameScript>().stringToEdit,score);
            gameObject.GetComponent<GetPlayerNameScript>().enabled = false;
            isLarger = false;
            UpdateScore();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !gameOver)
        {
            menuText.text = "Press <Enter> To Play	, <R> To Restart	,<S> To See HighScores";
            paused = !paused;
            startScreen.SetActive(paused);
            audioSource.PlayOneShot(startSound);          
            SetScoreDisplayOff();
            Time.timeScale = paused ? 0 : 1;
        }
        else if (Input.GetKeyDown(KeyCode.R) && paused)
        {
            Time.timeScale = 1;
            die = true;
            audioSource.PlayOneShot(startSound);
            paused = false;
            gameOver = false;
            gameOverScreen.SetActive(false);
            startScreen.SetActive(false);
            score = 0;
            level = 0;
            echo.GetComponent<PlayerScript>().restart();
            isLarger = false;
            SetScoreDisplayOff();
            gameObject.GetComponent<SpawnerScript>().resetSpawner();
            SetWaveDisplayOn();
        }

        if (Input.GetKeyDown(KeyCode.S) && paused)
        {
            audioSource.PlayOneShot(startSound);
            ToggleScoreDisplay();
        }
        
        else if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void UpdateScore()
    {       
        int i = 0;
        foreach (Scores highScore in HighScoreManager._instance.GetHighScore())
        {        
            gameOverTexts[i].GetComponent<Text>().text = (i+1) + ". " + highScore.name + " : " + highScore.score;
            i++;
        }
    }

    private void SetScoreDisplayOff()
    {
        foreach (GameObject text in pauseTexts)
        {
            text.SetActive(false);
        }

        foreach (GameObject text in gameOverTexts)
        {
            text.SetActive(false);
        }
    }

    private void ToggleScoreDisplay()
    {
        foreach (GameObject text in pauseTexts)
        {
            text.SetActive(!text.activeSelf);
        }
        foreach (GameObject text in gameOverTexts)
        {
            text.SetActive(!text.activeSelf);
        }
    }

    private void SetWaveDisplayOn()
    {
        waveTextTimer = 0;
        waveText.SetActive(true);
    }

    private void CheckIfHighScore()
    {
        foreach (Scores highScore in HighScoreManager._instance.GetHighScore())
        {
            if (score >= highScore.score)
            {
                isLarger = true;
                break;
            }
        }

        if (isLarger)
        {
            gameObject.GetComponent<GetPlayerNameScript>().enabled = true;
        }
    }
}
