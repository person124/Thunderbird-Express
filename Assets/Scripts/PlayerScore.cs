using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score;

    // Use this for initialization
    void Start ()
    {
        score = 0;
    }
	
    public void incrementScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, score);
    }
}
