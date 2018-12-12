using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    public float score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void incrementScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
}
