using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour {

    public GameObject playerList;
    public GameObject boss;
    //i didnt think until too late to use arrays, shoot me
    float setupTimer;
    float setupTimerMax;

    public GameObject scoreScreen;
    public GameObject gameOverScreen;

    private Wrapper.FuncPlayerUpdate scoreHandler;
    private Wrapper.FuncPlayerUpdate healthHandler;

    struct PlayerReferences
    {
        public PlayerScore playerScore;
        public PlayerHealth playerHealth;
        public bool playerDead;
        public string playerName;
        public GameObject playerScoreObj;
        public TextMeshProUGUI playerScoreText;
    }

    PlayerReferences[] playerListArray;

    public GameObject playerScorePanel;

    // Use this for initialization
    void Start()
    {
        playerList = GameObject.Find("PlayerList");
        playerListArray = new PlayerReferences[4];

        for (int i = 0; i < playerListArray.Length; ++i)
        {
            playerListArray[i].playerDead = false;
            playerListArray[i].playerScore = playerList.transform.GetChild(i).gameObject.GetComponent<PlayerScore>();
            playerListArray[i].playerHealth = playerList.transform.GetChild(i).gameObject.GetComponent<PlayerHealth>();
            playerListArray[i].playerScoreText = playerScorePanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            playerListArray[i].playerName = "Player " + i;
        }

        scoreScreen.SetActive(false);

        scoreHandler = HandleScore;
        healthHandler = HandleHealth;
        Wrapper.SetFuncPlayerUpdateScore(scoreHandler);
        Wrapper.SetFuncPlayerUpdateHealth(healthHandler);
    }

    public void HandleScore(ulong time, int playerID, int score)
    {
        playerListArray[playerID].playerScore.score = score;
    }

    public void HandleHealth(ulong time, int playerID, int health)
    {
        playerListArray[playerID].playerHealth.health = health;
        playerListArray[playerID].playerHealth.CheckHealth();
    }
	
	// Update is called once per frame
	void Update () {

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
        /*
        float highestScore = player1Score.score;
        if (everybodysDead() == true || boss.GetComponent<BossScript>().hp <= 0)
        {
            Debug.Log("ERRYBODY DEAD - network this");
            if (player2Score.score >= highestScore)
                highestScore = player2Score.score;
            if (player3Score.score >= highestScore)
                highestScore = player3Score.score;
            if (player4Score.score >= highestScore)
                highestScore = player4Score.score;

            // put a send message here for a win game screen that 
            //will kick people back to main sceneand end the game
        }
        */
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
        /*
        bool silence = false;

        if (!player1Dead)
        {
            if (player1Health.dead)
            {
                player1Dead = true;
            }
        }

        if (!player2Dead)
        {
            if (player2Health.dead)
            {
                player2Dead = true;
            }
        }

        if (!player3Dead)
        {
            if (player3Health.dead)
            {
                player3Dead = true;
            }
        }

        if (!player4Dead)
        {
            if (player4Health.dead)
            {
                player4Dead = true;
            }
        }

        if (player1Dead && player2Dead && player3Dead && player4Dead)
        {
            silence = true;
        }

        return silence;
        */
        return false;
    }

}
