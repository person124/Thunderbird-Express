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

	PLUGIN_SYMBOL
		bool NetworkingPlugin_StartClient(char ip[128], int port);

	PLUGIN_SYMBOL
		void NetworkingPlugin_StartLoop();

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
		void NetworkingPlugin_FuncPlayerData(FuncPlayerData func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncPlayerUpdate(FuncPlayerUpdate func);

	PLUGIN_SYMBOL
		void NetworkingPlugin_FuncGameState(FuncGameState func);

#pragma endregion

#pragma region SEND_PACKETS

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendTransform(int objectID,
			float x, float y, float z,
			float rX, float rY, float rZ);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendColor(int objectID, int color);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendShout(int shout);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendBossHP(int bossHP);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendPlayerData(int objectID,
			char name[33], int score, int health);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendPlayerUpdate(int objectID,
			int score, int health);

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendGameState(bool state);

#pragma endregion

#ifdef __cplusplus
}
#endif

#endif