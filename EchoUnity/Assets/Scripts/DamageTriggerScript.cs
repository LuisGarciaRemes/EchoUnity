using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerScript : MonoBehaviour {

    private GameObject echo;

    private void Start()
    {
        echo = GameObject.Find("Echo");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Venom")|| collision.gameObject.CompareTag("Snake")))
        {
            if(collision.gameObject.CompareTag("Venom") && echo.GetComponent<PlayerScript>().canTakeDamage)
            {
                echo.GetComponent<PlayerScript>().PlaySplat();
            }
            echo.GetComponent<PlayerScript>().TakeDamage();
        }
    }
}
