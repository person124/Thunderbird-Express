using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public float attackTimer;
    public float attackTimerMax;

    public GameObject attack;

    public float hp;
    float rotSpeed = 200;

	// Use this for initialization
	void Start () {
        hp = 1000;

        attackTimer = attackTimerMax;
	}
	
	// Update is called once per frame
	void Update () {

        //rotate slowly on multiple axes
        //this.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed);
        this.transform.Rotate(Vector3.right * Time.deltaTime * rotSpeed);
        this.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            Instantiate(attack, transform.position, Quaternion.identity); 
            attackTimer = attackTimerMax;
        }

    }


    void damageBoss(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Debug.Log("GAME OVER WOW NICE");
        }
    }


}
