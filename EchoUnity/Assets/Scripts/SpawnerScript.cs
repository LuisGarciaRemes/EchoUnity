using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public float minTime;
    public float maxTime;
    public float minY;
    public float maxY;
    public int obstaclesPerLevel;
    public GameObject[] stalagmites;
    public GameObject[] stalactites;
    public GameObject[] floatingObstacles;
    public GameObject firefly;
    public GameObject fruit;
    public GameObject[] Snakes;
    public float stalagmiteMax;
    public float stalactiteMax;
    public float waveWait;
    public float bossWait;

    private int obstacleCounter;
    private float spawnTimer;
    private float spawnTime;
    private float waveTimer;
    private float bossTimer;
    private GameObject currBoss;
    internal bool spawnNewWave;


    // Use this for initialization
    void Start()
    {
        resetSpawner();
        currBoss = null;
        bossTimer = 0;
        spawnNewWave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<GameManagerScript>().gameOver) {
            if (obstacleCounter <= obstaclesPerLevel)
            {
                if (spawnTimer > spawnTime)
                {
                    Vector3 obstaclePos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0.0f);
                    Instantiate(ObstacleType(obstaclePos.y), obstaclePos, Quaternion.identity);
                    obstaclePos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0.0f);
                    Instantiate(firefly, obstaclePos, Quaternion.identity);
                    spawnTime = Random.Range(minTime, maxTime);
                    spawnTimer = 0;
                    obstacleCounter++;
                }
                else
                {
                    spawnTimer += Time.deltaTime;
                }
            }
            else
            {
                if (currBoss == null)
                {                  
                    if (bossTimer >= bossWait)
                    {
                        Vector3 obstaclePos = new Vector3(transform.position.x-1, transform.position.y, 0.0f);
                        currBoss = Instantiate(Snakes[0], obstaclePos, Quaternion.identity);
                        GameObject.Find("BackGround").GetComponent<ScrollingBackgroundScript>().enabled = false;
                        bossTimer = 0;
                        gameObject.GetComponentInParent<AudioSource>().Pause();                       
                    }
                    {
                        bossTimer += Time.deltaTime;
                    }
                }

                if (spawnNewWave)
                {
                    checkWave();
                }
            }
        }
    }

    private GameObject ObstacleType(float pos)
    {
        if(pos >= stalactiteMax)
        {
            return stalactites[Random.Range(0,stalactites.Length)];
        }
        else if(pos <= stalagmiteMax)
        {
            return stalagmites[Random.Range(0, stalagmites.Length)];
        }
        return floatingObstacles[Random.Range(0, floatingObstacles.Length)];
    }

    private void checkWave()
    {
       GameObject.Find("BackGround").GetComponent<ScrollingBackgroundScript>().enabled = true;
       gameObject.GetComponentInParent<AudioSource>().UnPause();

            if(waveTimer >= waveWait)
            {
                gameObject.GetComponent<GameManagerScript>().NextWave();
                obstacleCounter = 0;
                waveTimer = 0;
                spawnNewWave = false;
            }
            else
            {
                waveTimer += Time.deltaTime;
            }
    }

    public void spawnLifeUp()
    {
        Vector3 obstaclePos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0.0f);
        Instantiate(fruit, obstaclePos, Quaternion.identity);
    }

    public void resetSpawner()
    {
        waveTimer = 0;
        spawnTimer = 0;
        bossTimer = 0;
        obstacleCounter = 0;
        spawnTime = Random.Range(minTime, maxTime);
    }
}
