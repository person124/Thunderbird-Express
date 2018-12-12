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

    public PlayerScore player1Score;
    public PlayerScore player2Score;
    public PlayerScore player3Score;
    public PlayerScore player4Score;

    public PlayerHealth player1Health;
    public PlayerHealth player2Health;
    public PlayerHealth player3Health;
    public PlayerHealth player4Health;

    public bool player1Dead;
    public bool player2Dead;
    public bool player3Dead;
    public bool player4Dead;

    public string player1Name = "Dorkus1";
    public string player2Name = "Dorkus2";
    public string player3Name = "Dorkus3";
    public string player4Name = "Dorkus4";

    public GameObject scoreScreen;
    public GameObject gameOverScreen;

    public GameObject player1ScoreObj;
    public GameObject player2ScoreObj;
    public GameObject player3ScoreObj;
    public GameObject player4ScoreObj;

    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI player3ScoreText;
    public TextMeshProUGUI player4ScoreText;


    // Use this for initialization
    void Start()
    {
        playerList = GameObject.Find("PlayerList");

        player1Score = playerList.transform.GetChild(0).gameObject.GetComponent<PlayerScore>();
        player2Score = playerList.transform.GetChild(1).gameObject.GetComponent<PlayerScore>();
        player3Score = playerList.transform.GetChild(2).gameObject.GetComponent<PlayerScore>();
        player4Score = playerList.transform.GetChild(3).gameObject.GetComponent<PlayerScore>();

        player1Health = playerList.transform.GetChild(0).gameObject.GetComponent<PlayerHealth>();
        player2Health = playerList.transform.GetChild(1).gameObject.GetComponent<PlayerHealth>();
        player3Health = playerList.transform.GetChild(2).gameObject.GetComponent<PlayerHealth>();
        player4Health = playerList.transform.GetChild(3).gameObject.GetComponent<PlayerHealth>();

        player1ScoreText = player1ScoreObj.GetComponent<TextMeshProUGUI>();
        player2ScoreText = player2ScoreObj.GetComponent<TextMeshProUGUI>();
        player3ScoreText = player3ScoreObj.GetComponent<TextMeshProUGUI>();
        player4ScoreText = player4ScoreObj.GetComponent<TextMeshProUGUI>();

        scoreScreen.SetActive(false);
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

    }

    void UpdateScores()
    {
        //get networked point values or something
        player1ScoreText.text = player1Name + "                         " + player1Health.health.ToString() + "                   " + player1Score.score.ToString();
        player2ScoreText.text = player2Name + "                         " + player2Health.health.ToString() + "                   " + player2Score.score.ToString();
        player3ScoreText.text = player3Name + "                         " + player3Health.health.ToString() + "                   " + player3Score.score.ToString();
        player4ScoreText.text = player4Name + "                         " + player4Health.health.ToString() + "                   " + player4Score.score.ToString();
    }

    bool everybodysDead()
    {
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
    }

}
