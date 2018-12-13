using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score;
    private Wrapper.FuncPlayerUpdate scoreRecieve;

    // Use this for initialization
    void Start ()
    {
        score = 0;
        scoreRecieve = HandleScore;
        Wrapper.SetFuncPlayerUpdateScore(scoreRecieve);
    }
	
    public void incrementScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, score);
    }

    void HandleScore(ulong time, int objectID, int ScorePassed)
    {
        score = ScorePassed;
    }
}
