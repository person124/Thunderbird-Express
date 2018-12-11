using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {



    public float hp;
    float rotSpeed = 200;

	// Use this for initialization
	void Start () {
        hp = 1000;
	}
	
	// Update is called once per frame
	void Update () {

        //rotate slowly on multiple axes
        //this.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed);
        this.transform.Rotate(Vector3.right * Time.deltaTime * rotSpeed);
        this.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);

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
