using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public Material red;
    public Material yellow;
    public Material blue;

    public Vector3 velocity;

    Material currentMaterial;

    enum AttackType
    {
        RED,
        YELLOW,
        BLUE
    };

    AttackType type;

	// Use this for initialization
	void Start () {

        type = (AttackType)Random.Range(0, 2);

        currentMaterial = GetComponent<Renderer>().material;

        switch (type)
        {
            case AttackType.RED:
                currentMaterial = red;
                break;
            case AttackType.YELLOW:
                currentMaterial = yellow;
                break;
            case AttackType.BLUE:
                currentMaterial = blue;
                break;
            default:
                break;
        }

        //velocity = new Vector3(Random.Range(0.0f, 5.0f), 0.0f, Random.Range(5.0f, 0.0f));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
