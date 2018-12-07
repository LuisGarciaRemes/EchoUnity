using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDamageScript : MonoBehaviour {

    public GameObject boss;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Venom"))
        {          
            boss.GetComponent<SnakeHealthScript>().TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
