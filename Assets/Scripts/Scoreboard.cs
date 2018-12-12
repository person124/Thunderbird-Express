using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    public GameObject playerList;

    public PlayerScore player1Score;
    public PlayerScore player2Score;
    public PlayerScore player3Score;
    public PlayerScore player4Score;

    public PlayerHealth player1Health;
    public PlayerHealth player2Health;
    public PlayerHealth player3Health;
    public PlayerHealth player4Health;

    public string player1Name = "dorkus1";
    public string player2Name = "dorkus2";
    public string player3Name = "dorkus3";
    public string player4Name = "dorkus4";

    public GameObject scoreScreen;

    float[] scores;

    public TextMesh player1ScoreText;
    public TextMesh player2ScoreText;
    public TextMesh player3ScoreText;
    public TextMesh player4ScoreText;

    // Use this for initialization
    void Start()
    {
        scores = new float[4];
        playerList = GameObject.Find("PlayerList");

        player1Score = playerList.transform.GetChild(0).gameObject.GetComponent<PlayerScore>();
        player2Score = playerList.transform.GetChild(1).gameObject.GetComponent<PlayerScore>();
        player3Score = playerList.transform.GetChild(2).gameObject.GetComponent<PlayerScore>();
        player4Score = playerList.transform.GetChild(3).gameObject.GetComponent<PlayerScore>();

        player1Health = playerList.transform.GetChild(0).gameObject.GetComponent<PlayerHealth>();
        player2Health = playerList.transform.GetChild(1).gameObject.GetComponent<PlayerHealth>();
        player3Health = playerList.transform.GetChild(2).gameObject.GetComponent<PlayerHealth>();
        player4Health = playerList.transform.GetChild(3).gameObject.GetComponent<PlayerHealth>();

        scoreScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Tab))
        {
            //display
            UpdateScores();
            scoreScreen.SetActive(true);
        }

	}

    void UpdateScores()
    {
        //get networked point values or something
        player1ScoreText.text = player1Name + "                         " + player1Health.health.ToString() + "               " + player1Score.ToString();
        player2ScoreText.text = player2Name + "                         " + player2Health.health.ToString() + "               " + player2Score.ToString();
        player3ScoreText.text = player3Name + "                         " + player3Health.health.ToString() + "               " + player3Score.ToString();
        player4ScoreText.text = player4Name + "                         " + player4Health.health.ToString() + "               " + player4Score.ToString();
    }

}
