using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour {

    public GameObject hostScreen;
    public GameObject clientScreen;
    public GameObject activeScreen; 


    // Use this for initialization
    void Start () {

        hostScreen.SetActive(false);
        clientScreen.SetActive(false);

        if (Wrapper.NetworkingPlugin_IsServer())
        {
            hostScreen.SetActive(true);
        }
        else
            clientScreen.SetActive(true);



    }

    // Update is called once per frame
    void Update () {
		
	}


    public void StartGame()
    {
        //yup

        hostScreen.SetActive(false);
        clientScreen.SetActive(false);
        activeScreen.SetActive(true);

        //spawn players

    }
}
