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

Peer* peerInstance = NULL;

FuncTransform fTransform;
FuncColor fColor;
FuncInt fShout;
FuncInt fBossHP;
FuncPlayerData fPlayerData;
FuncPlayerUpdate fPlayerUpdate;
FuncGameState fGameState;

#pragma region FUNCTION_SETTERS

void NetworkingPlugin_FuncTransform(FuncTransform func)
{
	fTransform = func;
}

void NetworkingPlugin_FuncColor(FuncColor func)
{
	fColor = func;
}

void NetworkingPlugin_FuncShout(FuncInt func)
{
	fShout = func;
}

void NetworkingPlugin_FuncBossHP(FuncInt func)
{
	fBossHP = func;
}

void NetworkingPlugin_FuncPlayerData(FuncPlayerData func)
{
	fPlayerData = func;
}

void NetworkingPlugin_FuncPlayerUpdate(FuncPlayerUpdate func)
{
	fPlayerUpdate = func;
}

void NetworkingPlugin_FuncGameState(FuncGameState func)
{
	fGameState = func;
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