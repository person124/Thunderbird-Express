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
#include "server.hpp"

#include "plugin/plugin_functions.hpp"

#include <assert.h>
#include <string>

Peer* peerInstance = NULL;

FuncTransform Plugin::fTransform;
FuncColor Plugin::fColor;
FuncInt Plugin::fShout;
FuncInt Plugin::fBossHP;
FuncInt Plugin::fPlayerNumber;
FuncPlayerData Plugin::fPlayerData;
FuncPlayerUpdate Plugin::fPlayerUpdate;
FuncGameState Plugin::fGameState;

// Non packet functions
FuncVoid Plugin::fServerShutdown;
FuncInt Plugin::fClientLeave;

#pragma region MISC_FUNCTIONS

bool NetworkingPlugin_StartClient(const char* ip, int port)
{
	assert(!peerInstance);

	std::string IP = std::string(ip);

	peerInstance = new Client();

	bool value = peerInstance->startClient(IP, port);

	if (!value)
	{
		delete peerInstance;
		peerInstance = NULL;
	}

	return value;
}

bool NetworkingPlugin_StartServer(int port, int maxClients)
{
	assert(!peerInstance);

	peerInstance = new Server();

	bool value = peerInstance->startServer(port, maxClients);

	if (!value)
	{
		delete peerInstance;
		peerInstance = NULL;
	}

	return value;
}

void NetworkingPlugin_DeletePeer()
{
	assert(peerInstance);

	delete peerInstance;

	peerInstance = NULL;
}

void NetworkingPlugin_StartLoop()
{
	assert(peerInstance);

	peerInstance->startNetworkingLoop();
}

bool NetworkingPlugin_IsServer()
{
	assert(peerInstance);

	return peerInstance->isServer();
}

void NetworkingPlugin_SendPlayerIDs()
{
	assert(peerInstance);
	assert(peerInstance->isServer());

	((Server*)peerInstance)->sendClientIds();
}

#pragma endregion

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

void NetworkingPlugin_FuncPlayerNumber(FuncInt func)
{
	Plugin::fPlayerNumber = func;
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

// Non Packet Functions

void NetworkingPlugin_FuncOnServerShutdown(FuncVoid func)
{
	Plugin::fServerShutdown = func;
}

void NetworkingPlugin_FuncOnClientLeave(FuncInt func)
{
	Plugin::fClientLeave = func;
}

#pragma endregion

#pragma region SEND_PACKETS

void NetworkingPlugin_SendTransform(int objectID,
	float x, float y, float z,
	float rX, float rY, float rZ,
	float vX, float vY, float vZ)
{
	if (peerInstance == NULL)
		return;

	PacketTransform packet(objectID, x, y, z, rX, rY, rZ, vX, vY, vZ);

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
	const char* name, int score, int health)
{
	if (peerInstance == NULL)
		return;

	char temp[33];
	strcpy_s(temp, name);
	PacketPlayerData packet(objectID, temp, score, health);

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

	Plugin::fGameState(0, state);
}

#pragma endregion