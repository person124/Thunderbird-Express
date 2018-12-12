using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoutPlayer : MonoBehaviour
{
    private Wrapper.FuncInt funcSound;
    public AudioClip[] shouts;
    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        funcSound = HandleSound;
        Wrapper.NetworkingPlugin_FuncShout(funcSound);
    }

    public void HandleSound(ulong time, int numShout)
    {
        PlayShout(numShout);
    }

	void PlayShout(int shoutToPlay)
    {
        audioSource.PlayOneShot(shouts[shoutToPlay]);
    }
}
