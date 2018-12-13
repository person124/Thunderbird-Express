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

#ifndef _PLUGIN_H_
#define _PLUGIN_H_

#include "plugin/config.h"

#ifdef __cplusplus
extern "C"
{
#endif

#include "plugin/functionTypes.h"

#pragma region MISC_FUNCTIONS

	PLUGIN_SYMBOL
		bool NetworkingPlugin_StartClient(const char* ip, int port);

	PLUGIN_SYMBOL
		bool NetworkingPlugin_StartServer(int port, int maxClients);

	PLUGIN_SYMBOL
		void NetworkingPlugin_DeletePeer();

	PLUGIN_SYMBOL
		void NetworkingPlugin_StartLoop();

	PLUGIN_SYMBOL
		bool NetworkingPlugin_IsServer();

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendPlayerIDs();

#pragma endregion

#pragma region FUNCTION_SETTERS

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncTransform(FuncTransform func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncColor(FuncColor func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncShout(FuncInt func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncBossHP(FuncInt func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncPlayerNumber(FuncInt func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncPlayerData(FuncPlayerData func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncPlayerUpdateHealth(FuncPlayerUpdate func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncPlayerUpdateScore(FuncPlayerUpdate func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncGameState(FuncGameState func);

	// Non Packet Functions
	
	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncOnServerShutdown(FuncVoid func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncOnClientLeave(FuncInt func);

#pragma endregion

#pragma region SEND_PACKETS

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendTransform(int objectID,
			float x, float y, float z,
			float rX, float rY, float rZ,
			float vX, float vY, float vZ);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendColor(int objectID, int color);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendShout(int shout);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendBossHP(int bossHP);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendPlayerData(int objectID,
			const char* name, int score, int health);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendPlayerHealth(int objectID,
			int health);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendPlayerScore(int objectID,
			int score);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendGameState(bool state);

#pragma endregion

#ifdef __cplusplus
}
#endif

#endif