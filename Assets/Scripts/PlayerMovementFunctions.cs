﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFunctions : MonoBehaviour
{
    [Tooltip("this is the id of the player, should be useful for deadreckoning")]
    public int ID;

    private GameObject cam;
    Rigidbody mRB;

    public const float MOVE_SPEED = 20.0f;
    public const float CAM_SPEED = 12.0f;

    float forwardMove, sideMove;

    Vector3 velocity;


    private void Start()
    {
        mRB = GetComponent<Rigidbody>();
        cam = transform.GetChild(0).gameObject;

    }

    public void SetVelocity(float forward, float horizontal)
    {
        velocity = new Vector3(horizontal, 0, forward).normalized * MOVE_SPEED;

        mRB.velocity = transform.forward * velocity.z + transform.right * velocity.x;
    }

    public void SetRotation(float pitch, float yaw)
    {
        pitch *= CAM_SPEED; 
        yaw *= CAM_SPEED;

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }


}

