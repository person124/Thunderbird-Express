﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VGSControls : MonoBehaviour {

    public GameObject introText;
    public GameObject leftText;
    public GameObject rightText;


    bool shoutStarted;
    bool? isTrashTalk;

    enum ShoutType
    {
        SHIELD_RED,
        SHIELD_YELLOW,
        SHIELD_BLUE,
        TRASHTALK_DAD,
        TRASHTALK_DORK,
        TRASHTALK_SHORTS
    };

    ShoutType type;

	// Use this for initialization
	void Start () {

        resetShout();
      
        introText.SetActive(true);
        leftText.SetActive(false);
        rightText.SetActive(false);

    }

    // Update is called once per frame
    void Update () {

        //poll for input to initialize shout
        if (!shoutStarted)
        {
        //start shout when either click is pressed, give it according type
            if (Input.GetMouseButtonDown(0)) //left
            {
                shoutStarted = true;
                isTrashTalk = false;

                introText.SetActive(false);
                leftText.SetActive(true);
                rightText.SetActive(false);

            }
            else if (Input.GetMouseButtonDown(1)) //right
            {
                shoutStarted = true;
                isTrashTalk = true;

                introText.SetActive(false);
                leftText.SetActive(false);
                rightText.SetActive(true);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) //left
            {
                //change type 
                if (isTrashTalk == true)
                    type = ShoutType.TRASHTALK_DORK;
                else
                    type = ShoutType.SHIELD_RED;

                Debug.Log(type);

                //send data with corresponding type, reset shout data, do this later
                resetShout();

            }
            else if (Input.GetMouseButtonDown(1)) //right
            {
                if (isTrashTalk == true)
                    type = ShoutType.TRASHTALK_SHORTS;
                else
                    type = ShoutType.SHIELD_BLUE;

                Debug.Log(type);

                //send data with corresponding type, reset shout data, do this later
                resetShout();

            }
            else if (Input.GetMouseButtonDown(2)) //middle
            {
                if (isTrashTalk == true)
                    type = ShoutType.TRASHTALK_DAD;
                else
                    type = ShoutType.SHIELD_YELLOW;

                Debug.Log(type);

                //send data with corresponding type, reset shout data, do this later
                resetShout();
            }
        }
        

    }

    void resetShout()
    {
        isTrashTalk = null;
        shoutStarted = false;

        introText.SetActive(true);
        leftText.SetActive(false);
        rightText.SetActive(false);
    }
}