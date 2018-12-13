/*
* Calum M. Phillips
* 82-0954005
*
* EGP-405-02
* Final Project
* 12/06/18
*
* Certificate of Authenticity:
* We certify that this work is entirely our own. The assessor of this project may reproduce this
* project and provide copies to other academic staff, and/or communicate a copy of this project
* to a plagiarism-checking service, which may retain a copy of the project on its database.
*
*/

using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public class Wrapper
{
	public const string DLL = "NetworkingPlugin_x64";

	// ==========================================================
	// MISC FUNCTIONS
	// ==========================================================

	[DllImport(DLL)]
	public static extern bool NetworkingPlugin_StartClient(string ip, int port);

	[DllImport(DLL)]
	public static extern bool NetworkingPlugin_StartServer(int port, int maxClients);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_DeletePeer();

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_StartLoop();

	[DllImport(DLL)]
	public static extern bool NetworkingPlugin_IsServer();

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendPlayerIDs();

	// ==========================================================
	// FUNCTION SETTERS
	// ==========================================================

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncTransform(ulong time, int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ,
		float vX, float vY, float vZ);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncColor(ulong time, int objectID, int color);

	// Shout and Boss HP
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncInt(ulong time, int num);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncPlayerData(ulong time, int objectID,
		string name, int score, int health);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncPlayerUpdate(ulong time, int objectID, int value);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncGameState(ulong time, bool state);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncVoid();

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncTransform(FuncTransform func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncColor(FuncColor func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncShout(FuncInt func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncBossHP(FuncInt func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncPlayerNumber(FuncInt func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncPlayerData(FuncPlayerData func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncPlayerUpdateHealth(FuncPlayerUpdate func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncPlayerUpdateScore(FuncPlayerUpdate func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncGameState(FuncGameState func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncOnServerShutdown(FuncVoid func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncOnClientLeave(FuncInt func);

	// ==========================================================
	// SEND PACKET FUNCTIONS
	// ==========================================================

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendTransform(int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ,
        float vX, float vY, float vZ);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendColor(int objectID, int color);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendShout(int shout);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendBossHP(int bossHP);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendPlayerData(int objectID,
		string name, int score, int health);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendPlayerHealth(int objectID,
		int health);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendPlayerScore(int objectID,
		int score);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendGameState(bool state);

	// ==========================================================
	// Function Setters, Automatic Unity main thread redirection
	// ==========================================================

	// ==========================================================
	// PacketTransform
	// ==========================================================
	public static void SetFuncTransform(FuncTransform func)
	{
		NetworkingPlugin_FuncTransform(HandleFuncTransform);
		mainThreadFuncTransform = func;
	}

	private static FuncTransform mainThreadFuncTransform;

	public static void HandleFuncTransform(ulong time, int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ,
		float vX, float vY, float vZ)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerFuncTransformed(time, objectID, x, y, z, rX, rY, rZ, vX, vY, vZ));
	}

	private static IEnumerator HandlerFuncTransformed(ulong time, int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ,
		float vX, float vY, float vZ)
	{
		yield return null;
		mainThreadFuncTransform(time, objectID, x, y, z, rX, rY, rZ, vX, vY, vZ);
	}

	// ==========================================================
	// PacketColor
	// ==========================================================
	public static void SetFuncColor(FuncColor func)
	{
		NetworkingPlugin_FuncColor(HandleFuncColor);
		mainThreadFuncColor = func;
	}

	private static FuncColor mainThreadFuncColor;

	public static void HandleFuncColor(ulong time, int objectID, int color)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerFuncColor(time, objectID, color));
	}

	private static IEnumerator HandlerFuncColor(ulong time, int objectID, int color)
	{
		yield return null;
		mainThreadFuncColor(time, objectID, color);
	}

	// ==========================================================
	// PacketShout
	// ==========================================================
	public static void SetFuncShout(FuncInt func)
	{
		NetworkingPlugin_FuncShout(HandleFuncShout);
		mainThreadFuncShout = func;
	}

	private static FuncInt mainThreadFuncShout;

	public static void HandleFuncShout(ulong time, int shout)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerFuncShout(time, shout));
	}

	private static IEnumerator HandlerFuncShout(ulong time, int shout)
	{
		yield return null;
		mainThreadFuncShout(time, shout);
	}

	// ==========================================================
	// PacketBossHP
	// ==========================================================
	public static void SetFuncBossHP(FuncInt func)
	{
		NetworkingPlugin_FuncBossHP(HandleBossHP);
		mainThreadFuncBossHP = func;
	}

	private static FuncInt mainThreadFuncBossHP;

	public static void HandleBossHP(ulong time, int hp)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerBossHP(time, hp));
	}

	private static IEnumerator HandlerBossHP(ulong time, int hp)
	{
		yield return null;
		mainThreadFuncBossHP(time, hp);
	}

	// ==========================================================
	// Packet Set Player Number
	// ==========================================================
	public static void SetFuncPlayerNumber(FuncInt func)
	{
		NetworkingPlugin_FuncPlayerNumber(HandlePlayerNumber);
		mainTheadFuncPlayerNumber = func;
	}

	private static FuncInt mainTheadFuncPlayerNumber;

	public static void HandlePlayerNumber(ulong time, int id)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerPlayerNumber(time, id));
	}

	private static IEnumerator HandlerPlayerNumber(ulong time, int id)
	{
		yield return null;
		mainTheadFuncPlayerNumber(time, id);
	}

	// ==========================================================
	// Packet Player Data
	// ==========================================================
	public static void SetFuncPlayerData(FuncPlayerData func)
	{
		NetworkingPlugin_FuncPlayerData(HandlePlayerData);
		mainThreadFuncPlayerData = func;
	}

	private static FuncPlayerData mainThreadFuncPlayerData;

	public static void HandlePlayerData(ulong time, int objectID, string name, int score, int health)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerPlayerData(time, objectID, name, score, health));
	}

	private static IEnumerator HandlerPlayerData(ulong time, int objectID, string name, int score, int health)
	{
		yield return null;
		mainThreadFuncPlayerData(time, objectID, name, score, health);
	}

	// ==========================================================
	// Packet Player Health
	// ==========================================================
	public static void SetFuncPlayerUpdateHealth(FuncPlayerUpdate func)
	{
		NetworkingPlugin_FuncPlayerUpdateHealth(HandlePlayerUpdateHealth);
		mainThreadFuncPlayerUpdateHealth = func;
	}

	private static FuncPlayerUpdate mainThreadFuncPlayerUpdateHealth;

	public static void HandlePlayerUpdateHealth(ulong time, int objectID, int health)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerPlayerUpdateHealth(time, objectID, health));
	}

	private static IEnumerator HandlerPlayerUpdateHealth(ulong time, int objectID, int health)
	{
		yield return null;
		mainThreadFuncPlayerUpdateHealth(time, objectID, health);
	}

	// ==========================================================
	// Packet Player Score
	// ==========================================================
	public static void SetFuncPlayerUpdateScore(FuncPlayerUpdate func)
	{
		NetworkingPlugin_FuncPlayerUpdateHealth(HandlePlayerUpdateScore);
		mainThreadFuncPlayerUpdateScore = func;
	}

	private static FuncPlayerUpdate mainThreadFuncPlayerUpdateScore;

	public static void HandlePlayerUpdateScore(ulong time, int objectID, int score)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerPlayerUpdateScore(time, objectID, score));
	}

	private static IEnumerator HandlerPlayerUpdateScore(ulong time, int objectID, int score)
	{
		yield return null;
		mainThreadFuncPlayerUpdateHealth(time, objectID, score);
	}

	// ==========================================================
	// Packet Game State
	// ==========================================================
	public static void SetFuncGameState(FuncGameState func)
	{
		NetworkingPlugin_FuncGameState(HandleGameState);
		mainThreadFuncGameState = func;
	}

	private static FuncGameState mainThreadFuncGameState;

	public static void HandleGameState(ulong time, bool b)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerGameState(time, b));
	}

	private static IEnumerator HandlerGameState(ulong time, bool b)
	{
		yield return null;
		mainThreadFuncGameState(time, b);
	}

	// ==========================================================
	// Packet Server Shutdown
	// ==========================================================
	public static void SetFuncOnServerShutdown(FuncVoid func)
	{
		NetworkingPlugin_FuncOnServerShutdown(HandleServerShutdown);
		mainThreadServerShutdown = func;
	}

	private static FuncVoid mainThreadServerShutdown;

	public static void HandleServerShutdown()
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerServerShutdown());
	}

	private static IEnumerator HandlerServerShutdown()
	{
		yield return null;
		mainThreadServerShutdown();
	}

	// ==========================================================
	// Packet Client Leave
	// ==========================================================
	public static void SetFuncOnClientLeave(FuncInt func)
	{
		NetworkingPlugin_FuncOnClientLeave(HandleClientLeave);
		mainThreadClientLeave = func;
	}

	private static FuncInt mainThreadClientLeave;

	public static void HandleClientLeave(ulong time, int objectID)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(HandlerClientLeave(time, objectID));
	}

	private static IEnumerator HandlerClientLeave(ulong time, int objectID)
	{
		yield return null;
		mainThreadClientLeave(time, objectID);
	}
}
