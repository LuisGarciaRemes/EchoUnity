using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerScript : MonoBehaviour {

    public GameObject objectTakingDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rock") & objectTakingDamage.CompareTag("Player"))
        {

            objectTakingDamage.GetComponent<PlayerScript>().TakeDamage();
        }
        else if(collision.gameObject.CompareTag("Wave") & objectTakingDamage.CompareTag("Snake"))
        {

        }
    }
}
