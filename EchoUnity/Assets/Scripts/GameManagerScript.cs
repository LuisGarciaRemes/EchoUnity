using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public AudioClip loopBG;

    private GMode mode;

    private GameObject echo;
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
        echo = GameObject.Find("Echo");
        startScreen.SetActive(true);
        gameOver = false;
        paused = true;
        timerPointsPerSecond = 0;
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        level = 1;
        waveTextTimer = 0;
        fruitPoints = pointsSpawnFruit;


        //HighScoreManager._instance.ClearLeaderBoard();
        HighScoreManager._instance.SaveHighScore("      ", 0);
        UpdateScore();

        Time.timeScale = 0;

        mode = GMode.Default;
    }
	
	// Update is called once per frame
	void Update () {
        GameStartControls();
        UpdatePoints();
        DisplayWaveText();
        CheckFruitSpawn();
        if(audioSource.isPlaying == false && gameObject.GetComponent<SpawnerScript>().currBoss == null)
        {
            audioSource.clip = loopBG;
            audioSource.Play();
            audioSource.loop = true;
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
        paused = true;
        gameOver = true;
        gameOverScreen.SetActive(true);              
    }

    public void NextWave()
    {
        level++;
        SetWaveDisplayOn();
    }

    public void NoUIPause()
    {
        paused = true;
    }

    public void NoUIResume()
    {
        paused = false;
    }

    public void ToStartScreen()
    {
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
        fruitPoints = pointsSpawnFruit;
        Time.timeScale = 0;

        mode = GMode.Default;
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
                break;
            case GMode.Tutorial:
                mode = m;
                if (spawner)
                    spawner.enabled = false;
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
        if(die)
        {
            die = false;
        }
        // Enter tutorial mode
        if (Input.GetButtonDown("tutorialButton") && mode == GMode.Default)
        {
            SwitchMode(GMode.Tutorial);
            startScreen.SetActive(false);
            audioSource.PlayOneShot(startSound);
            HighScoreManager._instance.SaveHighScore(gameObject.GetComponent<GetPlayerNameScript>().stringToEdit, score);
            gameObject.GetComponent<GetPlayerNameScript>().enabled = false;
            paused = false;
            gameOver = false;
            isLarger = false;
            waveText.SetActive(false);
        }
        if (Input.GetButtonDown("Start") && gameObject.GetComponent<GetPlayerNameScript>().enabled)
        {
            SwitchMode(GMode.Normal);
            audioSource.PlayOneShot(startSound);
            HighScoreManager._instance.SaveHighScore(gameObject.GetComponent<GetPlayerNameScript>().stringToEdit,score);
            gameObject.GetComponent<GetPlayerNameScript>().enabled = false;
            isLarger = false;
        }
        else if (Input.GetButtonDown("Start") && !gameOver)
        {
            SwitchMode(GMode.Normal);
            menuText.text = "Press <Start> Play ,<X> Restart, <B> HighScores";
            paused = !paused;
            startScreen.SetActive(paused);
            audioSource.PlayOneShot(startSound);          
            SetScoreDisplayOff();
            Time.timeScale = paused ? 0 : 1;
        }
        else if (Input.GetButtonDown("Restart") && paused)
        {
            UpdateScore();
            SwitchMode(GMode.Normal);
            Time.timeScale = 1;
            die = true;
            audioSource.PlayOneShot(startSound);
            paused = false;
            gameOver = false;
            gameOverScreen.SetActive(false);
            startScreen.SetActive(false);
            pointsSpawnFruit = fruitPoints;
            score = 0;
            level = 1;
            echo.GetComponent<PlayerScript>().restart();
            isLarger = false;
            SetScoreDisplayOff();
            gameObject.GetComponent<SpawnerScript>().resetSpawner();
            SetWaveDisplayOn();
        }

        if (Input.GetButtonDown("HighScore") && paused)
        {
            UpdateScore();
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
            pauseTexts[i].GetComponent<Text>().text = (i + 1) + ". " + highScore.name + " : " + highScore.score;
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
}
