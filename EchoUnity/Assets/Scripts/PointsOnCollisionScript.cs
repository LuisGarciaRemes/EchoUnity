using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnCollisionScript : MonoBehaviour {

    public int points;
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManagerScript>().addPoints(points);
            Destroy(this.gameObject);
        }
    }

    }
