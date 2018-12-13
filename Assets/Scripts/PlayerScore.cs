using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    public int score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void incrementScore(int scoreToAdd)
    {
        if (!Wrapper.NetworkingPlugin_IsServer())
            return;

        score += scoreToAdd;
        Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, score);
    }
}
