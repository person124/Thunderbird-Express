using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    public int score;
    public ObjectManager objManager;
	// Use this for initialization
	void Start ()
    {
        objManager = GameObject.FindGameObjectWithTag("CONTROL").GetComponent<ObjectManager>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void incrementScoreLocal(int scoreToAdd)
    {
        if (Wrapper.NetworkingPlugin_IsServer() || objManager.localPlayerID == GetComponent<PlayerMovementFunctions>().ID)
        {
            score += scoreToAdd;
            incrementScore(score);
            Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, score);
        }
    }
    public void incrementScore(int scoreToAdd)
    {
        score = scoreToAdd;
    }
}
