using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public GameObject first;
    public GameObject second;
    public GameObject third;

    public int health;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ATTACK"))
        {
            
        }
    }


    void DamagePlayer()
    {

    }
}
