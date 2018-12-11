using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovementFunctions mMove;

    public const float MAX_ROT = 2f; 
    public const float MIN_ROT = -4f;

    float mForward, mHorizontal, mYaw, mPitch;


    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mMove = GetComponent<PlayerMovementFunctions>();
        mForward = mHorizontal = mYaw = mPitch = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Inputs();
        Movement(mForward, mHorizontal);
        Rotation(mPitch, mYaw);
    }


    void Inputs()
    {
        mForward = Input.GetAxis("Vertical");
        if (mForward >= -.1f && mForward <= .1f)
            mForward = 0;

        mHorizontal = Input.GetAxis("Horizontal");
        if (mHorizontal >= -.1f && mHorizontal <= .1f)
            mHorizontal = 0;

        mYaw += Input.GetAxis("Mouse X");
        mPitch -= Input.GetAxis("Mouse Y");

        mPitch = Mathf.Clamp(mPitch, MIN_ROT, MAX_ROT);

    }

    void Movement(float forward, float horizontal)
    {
        mMove.SetVelocity(forward, horizontal);
    }

    void Rotation(float pitch, float yaw)
    {
        mMove.SetRotation(pitch, yaw);
    }

}
