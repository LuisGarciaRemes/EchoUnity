using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerScript : MonoBehaviour {

    public GameObject objectTakingDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Venom")) && objectTakingDamage.CompareTag("Player"))
        {
            if(collision.gameObject.CompareTag("Venom") && objectTakingDamage.GetComponent<PlayerScript>().canTakeDamage)
            {
                objectTakingDamage.GetComponent<PlayerScript>().PlaySplat();
            }
            objectTakingDamage.GetComponent<PlayerScript>().TakeDamage();

        }
        else if(collision.gameObject.CompareTag("Wave") && objectTakingDamage.CompareTag("Snake"))
        {
            GameObject.Find("GameManager").GetComponent<SpawnerScript>().spawnNewWave = true;
            Destroy(objectTakingDamage);
        }
    }
}
