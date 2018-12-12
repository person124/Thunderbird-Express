using System.Collections;
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

    private bool tick = false;

    private void Start()
    {
        funcTransform = HandleTransform;
        funcColor = HandleColor;
        setPlayerNumberFunc = SetPlayerNumber;
        funcGameState = OnGameStateChange;

        Wrapper.NetworkingPlugin_FuncPlayerNumber(setPlayerNumberFunc);
        Wrapper.NetworkingPlugin_FuncTransform(funcTransform);
        Wrapper.NetworkingPlugin_FuncColor(funcColor);
        Wrapper.NetworkingPlugin_FuncGameState(funcGameState);
    }

    private void Update()
    {
        if (tick && Wrapper.NetworkingPlugin_IsServer())
        {
            // Send attack positions
            for (int i = 4; i < objects.Length; ++i)
            {
                Vector3 tmpPos = objects[i].transform.position;
                Quaternion tmpRot = objects[i].transform.rotation;
                Vector3 tmpVel = objects[i].GetComponent<Attack>().velocity;

                Debug.Log(i + ", " + tmpPos + ", " + tmpRot + ", " + tmpVel);
                Wrapper.NetworkingPlugin_SendTransform(i,
                    tmpPos.x, tmpPos.y, tmpPos.z,
                    tmpRot.x, tmpRot.y, tmpRot.z,
                    tmpVel.x, tmpVel.y, tmpVel.z
                    );
            }
        }
        else
        {
            // If client:
            // (ignore for now) Dead Reckon
        }
    }

    public void HandleTransform(ulong time, int objectID,
        float x, float y, float z,
        float rX, float rY, float rZ,
        float vX, float vY, float vZ)
    {
        // Set object position
        objects[objectID].transform.position = new Vector3(x, y, z);
        objects[objectID].transform.rotation = Quaternion.Euler(rX, rY, rZ);
    }

    public void HandleColor(ulong time, int objectID, int color)
    {

    }


    public void SetPlayerNumber(ulong time, int num)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (i != num)
            {
                Destroy(objects[i].transform.GetChild(0).gameObject);
                objects[i].GetComponent<PlayerInput>().enabled = false;
                objects[i].GetComponent<PlayerMovementFunctions>().enabled = false;
                objects[i].GetComponent<VGSControls>().enabled = false;
                objects[i].GetComponent<PlayerScore>().enabled = false;
            }
            else
            {
                objects[i].GetComponent<PlayerMovementFunctions>().ID = num;

            }

            objects[i].SetActive(true);
        }
    }

    public void OnGameStateChange(ulong time, bool value)
    {
        tick = value;
    }
}
