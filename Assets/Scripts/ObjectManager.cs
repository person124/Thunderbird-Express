using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //insert dead reckoning here
    public GameObject[] attacks;
    public GameObject[] players;

    private Wrapper.FuncTransform funcTransform;
    private Wrapper.FuncColor funcColor;

    private void Start()
    {
        funcTransform = HandleTransform;
        funcColor = HandleColor;

        Wrapper.NetworkingPlugin_FuncTransform(funcTransform);
        Wrapper.NetworkingPlugin_FuncColor(funcColor);
    }

    public void HandleTransform(ulong time, int objectID,
        float x, float y, float z,
        float rX, float rY, float rZ)
    {

    }

    public void HandleColor(ulong time, int objectID, int color)
    {

    }
}
