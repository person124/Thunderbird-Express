using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public Material red;
    public Material yellow;
    public Material blue;

    public Vector3 velocity;
    public float maxSpeed;

    public float lifeTimer;
    public float lifeTimerMax = 4;


    MeshRenderer mesh;

    enum AttackType
    {
        RED,
        YELLOW,
        BLUE
    };

    AttackType type;

    // Use this for initialization
    void Start()
    {

        lifeTimer = lifeTimerMax;

        type = (AttackType)Random.Range(0, 3);

        mesh = GetComponent<MeshRenderer>();

        switch (type)
        {
            case AttackType.RED:
                mesh.material = red;
                break;
            case AttackType.YELLOW:
                mesh.material = yellow;
                break;
            case AttackType.BLUE:
                mesh.material = blue;
                break;
            default:
                break;
        }

        maxSpeed = .5f;

        velocity = new Vector3(Random.Range(-maxSpeed, maxSpeed), 0.0f, Random.Range(-maxSpeed, maxSpeed));

    }
	
	// Update is called once per frame
	void Update () {

        this.transform.position += velocity;
        //TO DO: REPLACE WITH NETWORKED MOVEMENT

        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("We made it!");
        if (collision.gameObject.CompareTag("WALL") == true)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
