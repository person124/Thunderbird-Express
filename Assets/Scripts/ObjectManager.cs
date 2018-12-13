﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //insert dead reckoning here
    public GameObject[] objects;

    private Wrapper.FuncTransform funcTransform;
    private Wrapper.FuncColor funcColor;
    private Wrapper.FuncInt setPlayerNumberFunc;
    private Wrapper.FuncGameState funcGameState;

    public int localPlayerID;

    private bool tick = false;

    public GameObject hostScreen;
    public GameObject clientScreen;
    public GameObject activeScreen;

    public Camera preGameCamera;

    private void Start()
    {
        funcTransform = HandleTransform;
        funcColor = HandleColor;
        setPlayerNumberFunc = SetPlayerNumber;
        funcGameState = OnGameStateChange;

        Wrapper.SetFuncTransform(funcTransform);
        Wrapper.SetFuncPlayerNumber(setPlayerNumberFunc);
        Wrapper.SetFuncColor(funcColor);
        Wrapper.SetFuncGameState(funcGameState);

        hostScreen.SetActive(false);
        clientScreen.SetActive(false);

        if (Wrapper.NetworkingPlugin_IsServer())
        {
            hostScreen.SetActive(true);
        }
        else
        {
            clientScreen.SetActive(true);
        }

        

    }

    private void Update()
    {
        if (tick && Wrapper.NetworkingPlugin_IsServer())
        {
            ////send player positions
            //for (int j = 0; j < 4; ++j)
            //{
            //    Vector3 tmpPos = objects[j].transform.position;
            //    Quaternion tmpRot = objects[j].transform.rotation;
            //    Vector3 tmpVel = objects[j].GetComponent<PlayerMovementFunctions>().mRB.velocity;

            //    Wrapper.NetworkingPlugin_SendTransform(j,
            //        tmpPos.x, tmpPos.y, tmpPos.z,
            //        tmpRot.x, tmpRot.y, tmpRot.z,
            //        tmpVel.x, tmpVel.y, tmpVel.z);
            //}

            // Send attack positions
            for (int i = 4; i < objects.Length; ++i)
            {
                Vector3 tmpPos = objects[i].transform.position;
                Quaternion tmpRot = objects[i].transform.rotation;
                Vector3 tmpVel = objects[i].GetComponent<Attack>().velocity;
            
                Wrapper.NetworkingPlugin_SendColor(i, (int)objects[i].GetComponent<Attack>().type);
            
                Wrapper.NetworkingPlugin_SendTransform(i,
                    tmpPos.x, tmpPos.y, tmpPos.z,
                    tmpRot.x, tmpRot.y, tmpRot.z,
                    tmpVel.x, tmpVel.y, tmpVel.z);
            }
        }
        else
        {
            // If client:
            // (ignore for now) Dead Reckon
            //for (int j = 0; j < 4; ++j)
            //{
            //    if (j != localPlayerID)
            //    {
            //        Vector3 tmpPos = objects[j].transform.position;
            //        Quaternion tmpRot = objects[j].transform.rotation;

                    


            //    }

            //}

        }
    }

    public void HandleTransform(ulong time, int objectID,
        float x, float y, float z,
        float rX, float rY, float rZ,
        float vX, float vY, float vZ)
    {
        // Set object position

        if (objectID < 4)
        {
            objects[objectID].transform.position = new Vector3(x, y, z);
            objects[objectID].transform.rotation = Quaternion.Euler(rX, rY, rZ);

            if (objectID != localPlayerID)
            {
                //Debug.Log(vX + ", " + vY + ", " + vZ);

                Vector3 tmpPos = objects[objectID].transform.position;
               // Quaternion tmpRot = objects[j].transform.rotation;

                //DeadReckoning DRInstance = objects[objectID].transform.GetComponent<DeadReckoning>();

                objects[objectID].transform.GetComponent<DeadReckoning>().recievedPosition = new Vector3(x, y, z);
                objects[objectID].transform.GetComponent<DeadReckoning>().recievedVelocity = new Vector3(vX, vY, vZ);

                objects[objectID].transform.GetComponent<DeadReckoning>().dt = (Time.time - objects[objectID].transform.GetComponent<DeadReckoning>().timestamp);
                objects[objectID].transform.GetComponent<DeadReckoning>().timestamp = Time.time;
            }

            //players
        }
        else
        {
            objects[objectID].transform.position = new Vector3(x, y, z);
            objects[objectID].transform.rotation = Quaternion.Euler(rX, rY, rZ);

            //attack
            objects[objectID].GetComponent<Attack>().velocity = new Vector3(vX, vY, vZ);
        }

        //UnityMainThreadDispatcher.Instance().Enqueue(Works(time, objectID, x, y, z, rX, rY, rZ, vX, vY, vZ));
    }

    //public IEnumerator Works(ulong time, int objectID,
    //    float x, float y, float z,
    //    float rX, float rY, float rZ,
    //    float vX, float vY, float vZ)
    //{
    //    yield return null;
    //
    //    // Set object position
    //    objects[objectID].transform.position = new Vector3(x, y, z);
    //    objects[objectID].transform.rotation = Quaternion.Euler(rX, rY, rZ);
    //}

    public void HandleColor(ulong time, int objectID, int color)
    {
        if (objectID < 4)
            objects[objectID].GetComponent<MeshMutator>().SendMessage("setColor", color);
        else
            objects[objectID].SendMessage("SetAttackType", color);
        //UnityMainThreadDispatcher.Instance().Enqueue(Color(time, objectID, color));
    }

    //public IEnumerator Color(ulong time, int objectID, int color)
    //{
    //    yield return null;
    //
    //}
    public void SetPlayerNumber(ulong time, int num)
    {
        for (int i = 0; i < 4; ++i)
        {
            objects[i].GetComponent<PlayerMovementFunctions>().ID = i;

            if (i != num)
            {
                objects[i].transform.GetChild(0).gameObject.SetActive(false);
                objects[i].GetComponent<PlayerInput>().enabled = false;
                objects[i].GetComponent<PlayerMovementFunctions>().enabled = false;
                objects[i].GetComponent<VGSControls>().enabled = false;
                //objects[i].GetComponent<PlayerScore>().enabled = false;
                
            }
            else
            {
                objects[i].GetComponent<DeadReckoning>().enabled = false;

            }

            objects[i].SetActive(true);
        }

        localPlayerID = num;

        hostScreen.SetActive(false);
        clientScreen.SetActive(false);
        activeScreen.SetActive(true);

        //spawn players
        preGameCamera.enabled = false;
        //UnityMainThreadDispatcher.Instance().Enqueue(yuppers(time, num));
    }

    void SwitchColor(int color)
    {
        for (int i = 0; i < 4; ++i)
        {
            objects[i].GetComponent<MeshMutator>().SendMessage("setColor", color);
            Wrapper.NetworkingPlugin_SendColor(i, color);
        }
    }
    //public IEnumerator yuppers(ulong time, int num)
    //{
    //    yield return null;
    //
    //}

    public void OnGameStateChange(ulong time, bool value)
    {
        tick = value;

        if (value)
        {
            hostScreen.SetActive(false);
            clientScreen.SetActive(false);
            activeScreen.SetActive(true);

            //spawn players
            preGameCamera.enabled = false;
        }
        //UnityMainThreadDispatcher.Instance().Enqueue(Blarg(time, value));
    }

    //public IEnumerator Blarg(ulong time, bool value)
    //{
    //    yield return null;
    //
    //}

    public void StartGame()
    {
        SetPlayerNumber(0, 0);
        Wrapper.NetworkingPlugin_SendPlayerIDs();
        Wrapper.NetworkingPlugin_SendGameState(true);
    }
}
