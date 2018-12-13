using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public GameObject first;
    public GameObject second;
    public GameObject third;

    public Camera mainCamera;
    public Camera deadCamera;

    public PlayerInput input;
    public PlayerScore scorekeeper;

    public bool dead = false;

    GameObject objManager;

    enum ShieldType
    {
        RED,
        YELLOW,
        BLUE
    };

    ShieldType shield;

    public int health;

    // Use this for initialization
    void Start ()
    {
        health = 3;
        dead = false;
        mainCamera = GetComponentInChildren<Camera>();
        input = GetComponent<PlayerInput>();
        scorekeeper = GetComponent<PlayerScore>();
        deadCamera = GameObject.Find("DEADCamera").GetComponent<Camera>();
        deadCamera.enabled = false;
        objManager = GameObject.FindGameObjectWithTag("CONTROL");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");

        if (other.gameObject.CompareTag("ATTACK"))
        {

            if ((int)shield != (int)other.gameObject.GetComponent<Attack>().type)
            {
                Debug.Log("wrong block");


                DamagePlayer();
            }
            else
            {
                scorekeeper.incrementScore(5000);
                Debug.Log("nice block!");

            }

        }
    }


    bool DamagePlayer()
    {
        --health;
        objManager.SendMessage("SendHealthLoss", GetComponent<PlayerMovementFunctions>().ID);
        if (health <= 0)
        {
            mainCamera.enabled = false;
            deadCamera.enabled = true;
            input.enabled = false;

            dead = true;

            return false;
        }
        else
        {
            switch (health)
            {
                case 2:
                    first.SetActive(false);
                    break;
                case 1:
                    second.SetActive(false);
                    break;
                default:
                    break;
            }

            return true;
        }
        
    }
}
