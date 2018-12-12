using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public Text ipText;
    public Text portText;

    public string ip;
    public int port;

    private

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void hostGame()
    {
        port = int.Parse(portText.text);

        Debug.Log(port);
        bool worked = Wrapper.NetworkingPlugin_StartServer(port, 4);


        if (worked)
        {
            Wrapper.NetworkingPlugin_StartLoop();

            SceneManager.LoadScene("LevelScene");
        }


    }

    public void connectToGame()
    {
        port = int.Parse(portText.text);
        ip = ipText.text;

        bool worked = Wrapper.NetworkingPlugin_StartClient(ip, port);

        if (worked)
        {
            Wrapper.NetworkingPlugin_StartLoop();

            SceneManager.LoadScene("LevelScene");
        }

    }

}
