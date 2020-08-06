using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakVenomScript : MonoBehaviour {

    public GameObject[] venomObjects;
    public float speed;

    private GameObject venom;

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wave"))
        {
            Vector3 obstaclePos = new Vector3(transform.position.x, transform.position.y, 0.0f);
            venom = Instantiate(venomObjects[0], obstaclePos, Quaternion.identity);
            venom.transform.localScale = -venom.transform.localScale;
            venom.GetComponent<SimpleMovementScript>().speedX = (speed - (speed * .25f));
            venom.GetComponent<SimpleMovementScript>().speedY = (speed - (speed * .25f));
            venom = Instantiate(venomObjects[1], obstaclePos, Quaternion.identity);
            venom.transform.localScale = -venom.transform.localScale;
            venom.GetComponent<SimpleMovementScript>().speedX = speed;
            venom.GetComponent<SimpleMovementScript>().speedY = 0;
            venom = Instantiate(venomObjects[2], obstaclePos, Quaternion.identity);
            venom.transform.localScale = -venom.transform.localScale;
            venom.GetComponent<SimpleMovementScript>().speedX = (speed-(speed*.25f));
            venom.GetComponent<SimpleMovementScript>().speedY = -(speed - (speed * .25f));
            Destroy(this.gameObject);
        }
    }
}
