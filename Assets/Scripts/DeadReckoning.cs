using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadReckoning : MonoBehaviour {

    //last known states, update these in packets
    public Vector3 lastKnownPosition = new Vector3(0, -100, 0);
    public Vector3 lastKnownVelocity;
    public Vector3 lastKnownAcceleration = new Vector3(5,5,5);

    public Vector3 estimatedPosition;
    public Vector3 actualPosition;

    public Vector3 recievedPosition;
    public Vector3 recievedVelocity;

    public Vector3 blendedVelocity;

    public float timestamp;
    public float dt;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //blend recieved and last known velocity - projective velocity blending
        blendedVelocity = Vector3.Lerp(recievedVelocity, lastKnownVelocity, .5f);

        //use last known data to predict new position
        estimatedPosition = deadReckon(lastKnownPosition, lastKnownVelocity, lastKnownAcceleration, dt);

        //might be unnecessary, may just need to pass in new position instead and then compare them
        actualPosition = deadReckon(recievedPosition, recievedVelocity, lastKnownAcceleration, dt);

        //Debug.Log(recievedPosition + ", " + recievedVelocity + ", " + dt);

        //current projection uses blended velocity
        transform.position = estimatedPosition + ((actualPosition - estimatedPosition) * dt);

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
