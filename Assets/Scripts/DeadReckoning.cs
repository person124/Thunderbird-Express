using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadReckoning : MonoBehaviour {

    //last known states, update these in packets
    public Vector3 lastKnownPosition;
    public Vector3 lastKnownVelocity;
    public Vector3 lastKnownAcceleration;

    public Vector3 estimatedPosition;
    public Vector3 actualPosition;

    public Vector3 recievedPosition;
    public Vector3 recievedVelocity;

    public Vector3 blendedVelocity;

    public float timestamp;
    public float dt;

    // Use this for initialization
    void Start () {
        lastKnownPosition = Vector3.zero;
        lastKnownVelocity = Vector3.zero;
        lastKnownAcceleration = Vector3.one;
        estimatedPosition = Vector3.zero;
        actualPosition = Vector3.zero;
        recievedPosition = Vector3.zero;
        recievedVelocity = Vector3.zero;
        blendedVelocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {

        if (dt <= 0.005)
        {
            dt = 0.01f;
        }
        //blend recieved and last known velocity - projective velocity blending
        lastKnownAcceleration = (lastKnownVelocity - recievedVelocity) / dt;
        //blendedVelocity = Vector3.Lerp(recievedVelocity, lastKnownVelocity, .5f);
        blendedVelocity = lastKnownVelocity + ((recievedVelocity - lastKnownVelocity) * dt);

        //use last known data to predict new position
        estimatedPosition = deadReckon(lastKnownPosition, lastKnownVelocity, lastKnownAcceleration, dt);

        //might be unnecessary, may just need to pass in new position instead and then compare them
        actualPosition = deadReckon(recievedPosition, blendedVelocity, lastKnownAcceleration, Time.deltaTime);

        //current projection uses blended velocity
        transform.position = actualPosition + ((estimatedPosition - actualPosition) * dt);

        lastKnownPosition = transform.position;
        lastKnownVelocity = recievedVelocity;

    }

    public Vector3 deadReckon(Vector3 cPos, Vector3 cVel, Vector3 cAcc, float time)
    {
        Vector3 updatedPosition;

        updatedPosition = cPos + (cVel * time) + (.5f * cAcc * (time * time));

        //Debug.Log(updatedPosition);

        return updatedPosition;
    }
}
