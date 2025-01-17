﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour {
    // singleton instance
    public static GameManagerScript instance;

    // Data classes
    enum GMode {
        Normal,
        Tutorial,
        Default
    }

    public Text scoreText;
    public bool timerPoints;
    public AudioClip addPointsSound;
    public AudioClip startSound;
    public AudioClip errorSound;
    public GameObject menuScreen;
    public GameObject waveText;
    public float speedUpAmount;
    public float waveTextTime;
    public int pointsSpawnFruit;
    public GameObject[] pauseTexts;
    public AudioClip loopBG;
    public GameObject menuButton;
    public GameObject restartButton;
    public GameObject startButton;
    public GameObject joystick;
    public GameObject floatingButton;
    public GameObject tutorialCanvas;
    public GameObject tutorialManager;
    public bool resetScores;
    public bool mobileMode;
    public Sprite gameOverSprite;
    public Sprite menuSprite;

    private GMode mode;

    public GameObject echo;
    private int score;
    private float timerPointsPerSecond;
    private AudioSource audioSource;
    internal bool gameOver;
    private float waveTextTimer;
    private bool isLarger;
    private int fruitPoints;

    internal bool die;
    internal bool paused;
    internal int level;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        isLarger = false;
        menuScreen.SetActive(true);
        gameOver = false;
        paused = true;
        timerPointsPerSecond = 0;
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        level = 1;
        waveTextTimer = 0;
        fruitPoints = pointsSpawnFruit;

        HighScoreManager._instance.SaveHighScore("      ", 0);

        if (resetScores)
        {
        HighScoreManager._instance.ClearLeaderBoard();
        }

        UpdateScoreText();

        Time.timeScale = 0;

        mode = GMode.Default;

        if (!mobileMode)
        {
            startButton.GetComponent<Button>().Select();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (!mobileMode)
        {
            GameStartControls();
        }
        
        UpdatePoints();
        DisplayWaveText();
        CheckFruitSpawn();
        if(audioSource.isPlaying == false && gameObject.GetComponent<SpawnerScript>().currBoss == null)
        {
            audioSource.clip = loopBG;
            audioSource.loop = true;
        }
    }

    private void LateUpdate()
    {
        if (die)
        {
            die = false;
        }
    }

    // public functions
    public void AddPoints(int points)
    {
        audioSource.PlayOneShot(addPointsSound);
        score += points;       
    }

    public void GameOver()
    {
        CheckIfHighScore();
        die = true;
        gameOver = true;
        menuScreen.GetComponent<Image>().sprite = gameOverSprite;
        ResumeButton();
        startButton.SetActive(false);
    }

    public void NextWave()
    {
        level++;
        SetWaveDisplayOn();
    }

    public void NoUIPause()
    {
        GameManagerScript.instance.echo.GetComponent<AudioSource>().volume = 0.0f;
        paused = true;
    }

    public void NoUIResume()
    {
        GameManagerScript.instance.echo.GetComponent<AudioSource>().volume = 0.0f;
        paused = false;
    }

    public void ToStartScreen()
    {
        ResumeButton();
    }

    // private functions
    void SwitchMode(GMode m)
    {
        var spawner = GetComponent<SpawnerScript>();
        switch (m) {
            case GMode.Normal:
                mode = m;
                if (spawner)
                    spawner.enabled = true;
                TutorialManager.DisableTutorial();
                tutorialManager.SetActive(false);
                tutorialCanvas.SetActive(false);
                break;
            case GMode.Tutorial:
                mode = m;
                if (spawner)
                    spawner.enabled = false;
                tutorialManager.SetActive(true);
                tutorialCanvas.SetActive(true);
                TutorialManager.StartTutorial();
                break;
            default:
                break;
        }
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
        if(score >= pointsSpawnFruit)
        {
            this.gameObject.GetComponent<SpawnerScript>().spawnLifeUp();
            pointsSpawnFruit += fruitPoints; 
        }
    }

    private void GameStartControls()
    {
       if (Input.GetButtonDown("Start") && !gameOver)
        {
            ResumeButton();
        }      
        
        else if (Input.GetKey("escape"))
        {
            QuitButton();
        }
    }

    private void UpdateScoreText()
    {       
        int i = 0;
        foreach (Scores highScore in HighScoreManager._instance.GetHighScore())
        {        
            pauseTexts[i].GetComponent<Text>().text = (i + 1) + ". " + highScore.name + " : " + highScore.score;
            i++;
        }
    }

    private void SetWaveDisplayOn()
    {
        waveText.GetComponent<Text>().text = "Wave: " + level;
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

    public void RestartButton()
    {
        if (!gameObject.GetComponent<GetPlayerNameScript>().enabled)
        {
            if (mobileMode)
            {
                joystick.GetComponent<FloatingJoystick>().ResetJoystick();
                menuButton.SetActive(!menuButton.activeSelf);
                joystick.SetActive(!joystick.activeSelf);
                floatingButton.SetActive(!floatingButton.activeSelf);
            }

            UpdateScoreText();
            SwitchMode(GMode.Normal);
            Time.timeScale = 1;
            die = true;
            audioSource.PlayOneShot(startSound);
            paused = false;
            gameOver = false;
            menuScreen.GetComponent<Image>().sprite = menuSprite;
            startButton.SetActive(true);
            menuScreen.SetActive(false);
            pointsSpawnFruit = fruitPoints;
            score = 0;
            level = 1;
            echo.GetComponent<PlayerScript>().Restart();
            isLarger = false;
            gameObject.GetComponent<SpawnerScript>().resetSpawner();
            SetWaveDisplayOn();
        }
        else
        {
            audioSource.PlayOneShot(errorSound);
        }
    }

    public void ResumeButton()
    {       
            if (mobileMode)
            {
                joystick.GetComponent<FloatingJoystick>().ResetJoystick();
                joystick.SetActive(!joystick.activeSelf);
                floatingButton.SetActive(!floatingButton.activeSelf);
                menuButton.SetActive(!menuButton.activeSelf);
            }
            paused = !paused;
            echo.GetComponent<PlayerScript>().MuteFire();
            menuScreen.SetActive(paused);

        if (mode == GMode.Tutorial)
        {
            startButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(true);
        }

        SwitchMode(GMode.Normal);

            restartButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            if (!gameOver)
            {
                audioSource.PlayOneShot(startSound);
                if (!mobileMode)
                {
                    if (startButton.activeSelf)
                    {
                        EventSystem.current.SetSelectedGameObject(startButton);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(restartButton);
                    }
                }
            }
            else
            {
                audioSource.PlayOneShot(echo.GetComponent<PlayerScript>().hurtSound);
                if (!mobileMode)
                {
                    EventSystem.current.SetSelectedGameObject(restartButton);
                }
            }
            Time.timeScale = paused ? 0 : 1;
    }

    public void TutorialButton()
    {
        if (!gameObject.GetComponent<GetPlayerNameScript>().enabled)
        {
            if (mobileMode)
            {
                joystick.GetComponent<FloatingJoystick>().ResetJoystick();
                joystick.SetActive(!joystick.activeSelf);
                floatingButton.SetActive(!floatingButton.activeSelf);
                menuButton.SetActive(!menuButton.activeSelf);
            }
            die = true;
            echo.GetComponent<PlayerScript>().Restart();
            SwitchMode(GMode.Tutorial);
            menuScreen.SetActive(false);
            audioSource.PlayOneShot(startSound);
            paused = false;
            gameOver = false;
            isLarger = false;
            pointsSpawnFruit = fruitPoints;
            score = 0;
            level = 1;
            waveText.SetActive(false);
            tutorialCanvas.SetActive(true);
            tutorialManager.SetActive(true);
        }
        else
        {
            audioSource.PlayOneShot(errorSound);
        }
    }

    public void EnterNameButton()
    {
        if (!gameObject.GetComponent<GetPlayerNameScript>().text.text.Equals(""))
        {
            SwitchMode(GMode.Normal);
            audioSource.PlayOneShot(startSound);
            HighScoreManager._instance.SaveHighScore(gameObject.GetComponent<GetPlayerNameScript>().text.text, score);
            gameObject.GetComponent<GetPlayerNameScript>().enabled = false;
            isLarger = false;
            UpdateScoreText();
            EventSystem.current.SetSelectedGameObject(restartButton);
        }
        else
        {
            audioSource.PlayOneShot(errorSound);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
