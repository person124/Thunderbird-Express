using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private Wrapper.FuncTransform funcTransform;

	public GameObject[] objects;

	private void Start()
	{
		funcTransform = HandleTransformPacket;

		Wrapper.NetworkingPlugin_FuncTransform(funcTransform);
	}

	public void HandleTransformPacket(ulong time, int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ)
	{
		Transform t = objects[objectID].transform;

		t.position = new Vector3(x, y, z);
		t.rotation = Quaternion.Euler(rX, rY, rZ);

		if (objects[objectID].GetComponent<Player>() != null)
		{
			// Handle player dead rekoning
		}
	}

	// THIS IS IN PLAYER:
	private void Update()
	{
		Vector3 pos = transform.position;
		Quaternion rot = transform.rotation;
		Wrapper.NetworkingPlugin_SendTransform(objectID, pos.x, pos.y, pos.z,
			rot.x, rot.y, rot.z);
	}
}
