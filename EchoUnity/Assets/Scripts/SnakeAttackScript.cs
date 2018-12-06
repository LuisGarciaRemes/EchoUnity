using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttackScript : MonoBehaviour {

    public float attackSpeedMin;
    public float attackSpeedMax;
    public GameObject venom;
    public GameObject snakeHead;
    public float acidOffset;
    public Animator anim;

    private float attackTimer;
    private float attackSpeed;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        attackTimer = 0;
        attackSpeed = Random.Range(attackSpeedMin,attackSpeedMax);
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().gameOver) {
            if (attackTimer >= attackSpeed)
            {
                anim.SetBool("isAttacking",true);
            }
            else
            {
                attackTimer += Time.deltaTime;
            }
        }
	}

    public void shootAcid()
    {
        audioSource.Play();
        Instantiate(venom, new Vector3(snakeHead.transform.position.x + acidOffset, snakeHead.transform.position.y, 0.0f), Quaternion.identity);
        attackTimer = 0;
        attackSpeed = Random.Range(attackSpeedMin, attackSpeedMax);
        anim.SetBool("isAttacking", false);
    }
}
