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
    public float stalagmiteMax;
    public float stalactiteMax;

    private int obstacleCounter;
    private float spawnTimer;
    private float spawnTime;


    // Use this for initialization
    void Start()
    {
        spawnTimer = 0;
        obstacleCounter = 0;
        spawnTime = Random.Range(minTime, maxTime);
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
                spawnTime = Random.Range(minTime, maxTime);
                spawnTimer = 0;
                obstacleCounter++;
            }
            else
            {
                spawnTimer += Time.deltaTime;             
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
}
