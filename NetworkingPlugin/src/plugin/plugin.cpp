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

#include "plugin/plugin.h"

#include "packet.hpp"
#include "peer.hpp"
#include "client.hpp"

#include "plugin/plugin_functions.hpp"

#include <assert.h>
#include <string>

Peer* peerInstance = NULL;

FuncTransform Plugin::fTransform;
FuncColor Plugin::fColor;
FuncInt Plugin::fShout;
FuncInt Plugin::fBossHP;
FuncPlayerData Plugin::fPlayerData;
FuncPlayerUpdate Plugin::fPlayerUpdate;
FuncGameState Plugin::fGameState;

bool NetworkingPlugin_StartClient(char ip[128], int port)
{
	assert(peerInstance);

	std::string IP = std::string(ip);

	peerInstance = new Client();

	return peerInstance->startClient(IP, port);
}

#pragma region FUNCTION_SETTERS

void NetworkingPlugin_FuncTransform(FuncTransform func)
{
	Plugin::fTransform = func;
}

void NetworkingPlugin_FuncColor(FuncColor func)
{
	Plugin::fColor = func;
}

void NetworkingPlugin_FuncShout(FuncInt func)
{
	Plugin::fShout = func;
}

void NetworkingPlugin_FuncBossHP(FuncInt func)
{
	Plugin::fBossHP = func;
}

void NetworkingPlugin_FuncPlayerData(FuncPlayerData func)
{
	Plugin::fPlayerData = func;
}

void NetworkingPlugin_FuncPlayerUpdate(FuncPlayerUpdate func)
{
	Plugin::fPlayerUpdate = func;
}

void NetworkingPlugin_FuncGameState(FuncGameState func)
{
	Plugin::fGameState = func;
}

#pragma endregion

#pragma region SEND_PACKETS

void NetworkingPlugin_SendTransform(int objectID,
	float x, float y, float z,
	float rX, float rY, float rZ)
{
	if (peerInstance == NULL)
		return;

	PacketTransform packet(objectID, x, y, z, rX, rY, rZ);

	peerInstance->sendPacketToAll(&packet);
}

void NetworkingPlugin_SendColor(int objectID, int color)
{
	if (peerInstance == NULL)
		return;

	PacketColor packet(objectID, color);

	peerInstance->sendPacketToAll(&packet);
}

void NetworkingPlugin_SendShout(int shout)
{
	if (peerInstance == NULL)
		return;

	PacketShout packet(shout);

	peerInstance->sendPacketToAll(&packet);
}

void NetworkingPlugin_SendBossHP(int bossHP)
{
	if (peerInstance == NULL)
		return;

	PacketBossHP packet(bossHP);

	peerInstance->sendPacketToAll(&packet);
}

void NetworkingPlugin_SendPlayerData(int objectID,
	char name[33], int score, int health)
{
	if (peerInstance == NULL)
		return;

	PacketPlayerData packet(objectID, name, score, health);

	peerInstance->sendPacketToAll(&packet);
}

void NetworkingPlugin_SendPlayerUpdate(int objectID, int score, int health)
{
	if (peerInstance == NULL)
		return;

	PacketPlayerUpdate packet(objectID, score, health);

	peerInstance->sendPacketToAll(&packet);
}

void NetworkingPlugin_SendGameState(bool state)
{
	if (peerInstance == NULL)
		return;

	PacketGameState packet(state);

	peerInstance->sendPacketToAll(&packet);
}

#pragma endregion