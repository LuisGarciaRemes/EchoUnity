using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnCollisionScript : MonoBehaviour {

    public int points;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManagerScript.instance.AddPoints(points);
            Destroy(this.gameObject);
        }
    }
    }
