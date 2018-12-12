using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoutPlayer : MonoBehaviour
{
    private Wrapper.FuncInt funcSound;
    public AudioClip[] shouts;
    AudioSource audioSource;
    GameObject Boss;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        Boss = GameObject.Find("Boss");
        funcSound = HandleSound;
        Wrapper.NetworkingPlugin_FuncShout(funcSound);
    }

    public void HandleSound(ulong time, int numShout)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(SendAudio(time, numShout));
    }

	void PlayShout(int shoutToPlay)
    {
        if (shoutToPlay >= 3)
        {
            Boss.SendMessage("SubtractFromHP", shoutToPlay);
        }
        
        audioSource.PlayOneShot(shouts[shoutToPlay]);
    }

    public IEnumerator SendAudio(ulong time, int shoutToPlay)
    {
        yield return null;

        // Set object position
        PlayShout(shoutToPlay);
    }

}
