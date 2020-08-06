using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Venom")|| collision.gameObject.CompareTag("Snake")))
        {
            if(collision.gameObject.CompareTag("Venom") && gameObject.GetComponent<PlayerScript>().canTakeDamage)
            {
                GameManagerScript.instance.echo.GetComponent<PlayerScript>().PlaySplat();
            }
            gameObject.GetComponent<PlayerScript>().TakeDamage();
        }
    }
}
