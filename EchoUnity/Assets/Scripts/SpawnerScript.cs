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
    public float stalagmiteMax;
    public float stalactiteMax;
    public float waveWait;

    private int obstacleCounter;
    private float spawnTimer;
    private float spawnTime;
    private float waveTimer;
    private GameObject currBoss;


    // Use this for initialization
    void Start()
    {
        waveTimer = 0;
        spawnTimer = 0;
        obstacleCounter = 0;
        spawnTime = Random.Range(minTime, maxTime);
        currBoss = null;
    }

    // Update is called once per frame
    void Update()
    {
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
            checkWave();
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
        if(currBoss == null)
        {
            if(waveTimer >= waveWait)
            {
                obstacleCounter = 0;
                waveTimer = 0;
                this.gameObject.GetComponent<GameManagerScript>().NextWave();
            }
            else
            {
                waveTimer += Time.deltaTime;
            }
        }
    }

    public void spawnLifeUp()
    {
        Vector3 obstaclePos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0.0f);
        Instantiate(fruit, obstaclePos, Quaternion.identity);
    }
}
