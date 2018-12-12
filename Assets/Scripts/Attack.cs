using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public Material red;
    public Material yellow;
    public Material blue;

    public Vector3 velocity;
    public float maxSpeed;
    float minSpeed;

    bool isUsed;
    Rigidbody rb;

    public Vector3 startPos;

    MeshRenderer mesh;

    public enum AttackType
    {
        RED,
        YELLOW,
        BLUE
    };

    public AttackType type;

    // Use this for initialization
    void Start()
    {
        isUsed = false;
        rb = GetComponent<Rigidbody>();

        mesh = GetComponent<MeshRenderer>();

        maxSpeed = 6.5f;
        minSpeed = 4.0f;

    }
	
	// Update is called once per frame
	void Update () {

        //this.transform.position += velocity;
        //TO DO: REPLACE WITH NETWORKED MOVEMENT

        
    }

    void SetVelocity(Vector3 pos)
    {
        int x;
        transform.position = pos;
        isUsed = true;
        velocity = new Vector3(Random.Range(-maxSpeed, maxSpeed), 0.0f, Random.Range(-maxSpeed, maxSpeed));
        if (velocity.x < 0)
            velocity.x -= minSpeed;
        else
            velocity.x += minSpeed;

        if (velocity.z < 0)
            velocity.z -= minSpeed;
        else
            velocity.z += minSpeed;

        rb.velocity = velocity;
        type = (AttackType)Random.Range(0, 3);

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
    }

    void ResetPos()
    {
        rb.velocity = Vector3.zero;
        transform.position = startPos;
        SetUsed();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("WALL") == true)
        {
            //ResetPos();
        }
    }
    //getter and setter for movement
    public bool ReturnUsed() { return isUsed;}
    public void SetUsed() { isUsed = !isUsed; }
}
