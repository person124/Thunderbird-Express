using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCleanup : MonoBehaviour {
	
    private void OnDisable()
    {
        Wrapper.NetworkingPlugin_DeletePeer();
    }
}
