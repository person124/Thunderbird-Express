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

using System.Runtime.InteropServices;

using UnityEngine;

public class Wrapper
{
	public const string DLL = "NetworkingPlugin_x64";

	[DllImport(DLL)]
	public static extern bool NetworkingPlugin_StartClient(char[] ip, int port);

	// ==========================================================
	// FUNCTION SETTERS
	// ==========================================================

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncTransform(ulong time, int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncColor(ulong time, int objectID, int color);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncInt(ulong time, int num);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncPlayerData(ulong time, int objectID,
		char[] name, int score, int health);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncPlayerUpdate(ulong time, int objectID, int score, int health);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void FuncGameState(ulong time, bool state);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncTransform(FuncTransform func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncColor(FuncColor func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncShout(FuncInt func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncBossHP(FuncInt func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncPlayerData(FuncPlayerData func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncPlayerUpdate(FuncPlayerUpdate func);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_FuncGameState(FuncGameState func);

	// ==========================================================
	// SEND PACKET FUNCTIONS
	// ==========================================================

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendTransform(int objectID,
		float x, float y, float z,
		float rX, float rY, float rZ);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendColor(int objectID, int color);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendShout(int shout);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendBossHP(int bossHP);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendPlayerData(int objectID,
		char[] name, int score, int health);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendPlayerUpdate(int objectID,
		int score, int health);

	[DllImport(DLL)]
	public static extern void NetworkingPlugin_SendGameState(bool state);
}
