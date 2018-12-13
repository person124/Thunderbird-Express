using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VGSControls : MonoBehaviour {

    public GameObject introText;
    public GameObject leftText;
    public GameObject rightText;

    public PlayerScore scorekeeper;
    GameObject shoutRef;

    bool shoutStarted;
    bool? isTrashTalk;

    MeshMutator myColor;
    GameObject objManager;
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
	void Start ()
    {
        objManager = GameObject.FindGameObjectWithTag("CONTROL");
        shoutRef = transform.root.gameObject;
        scorekeeper = GetComponent<PlayerScore>();
        myColor = GetComponent<MeshMutator>();

        isTrashTalk = null;
        shoutStarted = false;

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
                {
                    type = ShoutType.TRASHTALK_DORK;
                    Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, scorekeeper.score + 1000);
                }
                else
                {
                    type = ShoutType.SHIELD_RED;
                    myColor.setColor((int)type);
                    objManager.SendMessage("SwitchColor", (int)type);
                }

                Debug.Log(type);

                //send data with corresponding type, reset shout data, do this later
                resetShout();

            }
            else if (Input.GetMouseButtonDown(1)) //right
            {
                if (isTrashTalk == true)
                {
                    type = ShoutType.TRASHTALK_SHORTS;
                    Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, scorekeeper.score + 500);
                }
                else
                { 
                    type = ShoutType.SHIELD_BLUE;
                    myColor.setColor((int)type);
                    objManager.SendMessage("SwitchColor", (int)type);
                }
                Debug.Log(type);

                //send data with corresponding type, reset shout data, do this later
                resetShout();

            }
            else if (Input.GetMouseButtonDown(2)) //middle
            {
                if (isTrashTalk == true)
                {
                    type = ShoutType.TRASHTALK_DAD;
                    Wrapper.NetworkingPlugin_SendPlayerScore(GetComponent<PlayerMovementFunctions>().ID, scorekeeper.score + 2000);
                }
                else
                { 
                    type = ShoutType.SHIELD_YELLOW;
                    myColor.setColor((int)type);
                    objManager.SendMessage("SwitchColor", (int)type);
                }

                Debug.Log(type);

                //send data with corresponding type, reset shout data, do this later
                resetShout();
            }
        }
        

    }

    void resetShout()
    {
        sendShoutOverNet();
        isTrashTalk = null;
        shoutStarted = false;

        introText.SetActive(true);
        leftText.SetActive(false);
        rightText.SetActive(false);
    }

    void sendShoutOverNet()
    {
        Debug.Log("THUNDERBIRD YEULLWLO");
        Wrapper.NetworkingPlugin_SendShout((int)type);

        shoutRef.SendMessage("PlayShout", (int)type);

        Wrapper.NetworkingPlugin_SendShout((int)type);
    }
}
