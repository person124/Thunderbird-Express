using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private Wrapper.FuncInt shoutFunc;

	void Start()
	{
		shoutFunc = Merpa;
		Wrapper.NetworkingPlugin_FuncShout(shoutFunc);

		bool worked = Wrapper.NetworkingPlugin_StartClient("localhost", 25565);

		Debug.Log(worked);

		if (worked)
		{
			Debug.Log("Client started!");

			Wrapper.NetworkingPlugin_StartLoop();
		}
	}

	public static void Merpa(ulong time, int num)
	{
		Debug.Log(time + ": " + num);
	}
}
