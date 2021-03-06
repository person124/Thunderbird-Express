﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    public GameObject playerList;
    public GameObject boss;
    //i didnt think until too late to use arrays, shoot me
    float setupTimer;
    float setupTimerMax;

    public GameObject scoreScreen;
    public GameObject gameOverScreen;

    private Wrapper.FuncPlayerUpdate scoreHandler;
    private Wrapper.FuncPlayerUpdate healthHandler;
	private Wrapper.FuncPlayerData playerNameHandler;

    public int winnerIndex = 0;

    ObjectManager objControl;
    public struct PlayerReferences
    {
        public PlayerScore playerScore;
        public PlayerHealth playerHealth;
        public bool playerDead;
        public string playerName;
        public GameObject playerScoreObj;
        public TextMeshProUGUI playerScoreText;
    }

    public PlayerReferences[] playerListArray;

    public GameObject playerScorePanel;

    // Use this for initialization
    void Start()
    {
        playerList = GameObject.Find("PlayerList");
        playerListArray = new PlayerReferences[4];

        for (int i = 0; i < playerListArray.Length; ++i)
        {
            playerListArray[i].playerScore = playerList.transform.GetChild(i).gameObject.GetComponent<PlayerScore>();
            playerListArray[i].playerHealth = playerList.transform.GetChild(i).gameObject.GetComponent<PlayerHealth>();
            playerListArray[i].playerHealth.health = 3;
            playerListArray[i].playerScoreText = playerScorePanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            playerListArray[i].playerName = "";
        }

        scoreScreen.SetActive(false);

        scoreHandler = HandleScore;
        healthHandler = HandleHealth;
		playerNameHandler = HandlePlayerNames;
        Wrapper.SetFuncPlayerUpdateScore(scoreHandler);
        Wrapper.SetFuncPlayerUpdateHealth(healthHandler);
		Wrapper.SetFuncPlayerData(playerNameHandler);

        objControl = GameObject.FindGameObjectWithTag("CONTROL").GetComponent<ObjectManager>();
    }

    public void HandleScore(ulong time, int playerID, int score)
    {
        playerListArray[playerID].playerScore.incrementScore(score);
    }

    public void HandleHealth(ulong time, int playerID, int health)
    {
        playerListArray[playerID].playerHealth.health = health;
        playerListArray[playerID].playerHealth.CheckHealth();
    }

	public void HandlePlayerNames(ulong time, int objectID,
		string name, int score, int health)
	{
		playerListArray[objectID].playerName = name;
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Tab))
        {
            //display
            UpdateScores();
            scoreScreen.SetActive(true);
            //Debug.Log("tab active");
        }
        else
            scoreScreen.SetActive(false);

        WinStateActive();
    }

    void WinStateActive()
    {
        int highestScore = playerListArray[0].playerScore.score;

        if (objControl.tick)
        {
            if (everybodysDead() || boss.GetComponent<BossScript>().hp <= 0)
            {
                Debug.Log("ERRYBODY DEAD - network this");

                for (int i = 1; i < playerListArray.Length; ++i)
                {
                    if (playerListArray[i].playerScore.score > highestScore)
                    {
                        highestScore = playerListArray[i].playerScore.score;
                        winnerIndex = i;
                    }
                }

                Wrapper.NetworkingPlugin_SendGameState(false);
            }
            // put a send message here for a win game screen that 
            //will kick people back to main sceneand end the game
        }
        
    }

    void UpdateScores()
    {
        for (int i = 0; i < playerListArray.Length; ++i)
        {
            playerListArray[i].playerScoreText.text = playerListArray[i].playerName + "                         " + playerListArray[i].playerHealth.health + "                   " + playerListArray[i].playerScore.score;
        }
    }
    bool everybodysDead()
    {
        bool allDed = true;
        for (int i = 0; i < playerListArray.Length; ++i)
        {
            if (playerListArray[i].playerHealth.health != 0 && playerListArray[i].playerName != "")
            {
                allDed = false;
                Debug.Log("ytho");
            }
        }
        return allDed;
    }

}
