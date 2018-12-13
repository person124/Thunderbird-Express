using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMutator : MonoBehaviour
{
    //private Wrapper.FuncColor funcColor;
    
    MeshRenderer mesh;
    
    //public Material red;
    //public Material yellow;
    //public Material blue;
    // Use this for initialization
    void Start ()
    {
        //funcColor = handleMeshColor;
        //Wrapper.SetFuncColor(funcColor);
        mesh = GetComponent<MeshRenderer>();
        setColor(0);
    }
    
    //void handleMeshColor(ulong time, int objectID, int color)
    //{
    //    setColor(color);
    //}
    
    public void setColor(int color)
    {
        switch (color)
        {
            case 0:
                mesh.material.color = Color.red;
                break;
            case 1:
                mesh.material.color = Color.yellow;
                break;
            case 2:
                mesh.material.color = Color.blue;
                break;
        }
    }
}
