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

#include "server.hpp"

#include "connection.hpp"
#include "packet.hpp"
#include "plugin/plugin_functions.hpp"

#include <assert.h>

Server::Server()
{
	mConnectedClientCount = 0;

	mConnections = new Connection*[MAX_PLAYER_COUNT - 1];
	for (unsigned int i = 0; i < MAX_PLAYER_COUNT - 1; ++i)
		mConnections[i] = NULL;
}

Server::~Server()
{
	for (unsigned int i = 0; i < MAX_PLAYER_COUNT - 1; ++i)
	{
		if (mConnections[i] != NULL)
			delete mConnections[i];
	}

	delete[] mConnections;

	PacketServerShutdown shutdown = PacketServerShutdown();
	sendPacketToAll(&shutdown);
}

// Sends the ids to all of the connected clients
void Server::sendClientIds()
{
	unsigned int count = 1;

	for (unsigned int i = 0; i < MAX_PLAYER_COUNT - 1; ++i)
	{
		if (mConnections[i] != NULL)
		{
			PacketPlayerNumber number = PacketPlayerNumber(count);
			sendPacket(&number, mConnections[i]);
			count++;
		}
	}
}

void Server::handlePacket(Packet* packet, Connection* conn)
{
	// Make sure to only send packets that need to be sent
	if (packet->packetID > PACKET_BASE_ID && packet->packetID <= PACKET_GAME_STATE)
	{
		// Send the packet to all but who sent it
		sendPacketBut(packet, conn);
	}

	switch (packet->packetID)
	{
#pragma region UNITY_PACKETS
	case PACKET_TRANSFORM:
	{
		PacketTransform* p = (PacketTransform*)packet;
		Plugin::fTransform(p->timeStamp, p->objectID,
			p->posX, p->posY, p->posZ,
			p->rotX, p->rotY, p->rotZ,
			p->velX, p->velY, p->velZ);
		break;
	}
	case PACKET_COLOR:
	{
		PacketColor* p = (PacketColor*)packet;
		Plugin::fColor(p->timeStamp, p->objectID, p->colorID);
		break;
	}
	case PACKET_SHOUT:
	{
		PacketShout* p = (PacketShout*)packet;
		Plugin::fShout(p->timeStamp, p->shoutID);
		break;
	}
	case PACKET_BOSS_HP:
	{
		PacketBossHP* p = (PacketBossHP*)packet;
		Plugin::fBossHP(p->timeStamp, p->hp);
		break;
	}
	case PACKET_PLAYER_DATA:
	{
		PacketPlayerData* p = (PacketPlayerData*)packet;
		Plugin::fPlayerData(p->timeStamp, p->objectID, p->name, p->score, p->health);
		break;
	}
	case PACKET_PLAYER_HEALTH:
	{
		PacketPlayerHealth* p = (PacketPlayerHealth*)packet;
		Plugin::fPlayerHealth(p->timeStamp, p->objectID, p->health);
		break;
	}
	case PACKET_PLAYER_SCORE:
	{
		PacketPlayerScore* p = (PacketPlayerScore*)packet;
		Plugin::fPlayerScore(p->timeStamp, p->objectID, p->score);
		break;
	}
	case PACKET_GAME_STATE:
	{
		PacketGameState* p = (PacketGameState*)packet;
		Plugin::fGameState(p->timeStamp, p->trueForStartFalseForEnd);
		break;
	}
#pragma endregion

	case PACKET_CLIENT_JOIN:
	{
		// When a client joins
		addConnection(conn);
		break;
	}

	case PACKET_CLIENT_DISCONNECT:
	{
		// When a client leaves
		unsigned int id = removeConnection(conn);
		assert(id != 0);

		Plugin::fClientLeave(packet->timeStamp, id);
		PacketClientDisconnect disconnect = PacketClientDisconnect(id);
		sendPacketBut(&disconnect, conn);

		break;
	}

	case PACKET_BASE_ID:
	default:
		assert(false);
		break;
	}
}

void Server::addConnection(Connection* conn)
{
	for (unsigned int i = 0; i < MAX_PLAYER_COUNT - 1; ++i)
	{
		if (mConnections[i] == NULL)
		{
			mConnections[i] = new Connection(conn->getID());
			mConnectedClientCount++;
			return;
		}
	}
}

unsigned int Server::removeConnection(Connection* conn)
{
	for (unsigned int i = 0; i < MAX_PLAYER_COUNT - 1; ++i)
	{
		if (mConnections[i] && mConnections[i]->getID() == conn->getID())
		{
			delete mConnections[i];
			mConnections[i] = NULL;

			mConnectedClientCount--;

			return i + 1;
		}
	}

	return 0;
}