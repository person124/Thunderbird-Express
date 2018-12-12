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

#include "packet.hpp"
#include "plugin/plugin_functions.hpp"

#include <assert.h>

Server::Server()
{
}

void Server::handlePacket(Packet* packet, Connection* conn)
{
	if (packet->packetID > PACKET_BASE_ID && packet->packetID < PACKET_COUNT)
	{
		// Send the packet to all but who sent it
		sendPacketBut(packet, conn);
	}

	switch (packet->packetID)
	{
	case PACKET_TRANSFORM:
	{
		PacketTransform* p = (PacketTransform*)packet;
		Plugin::fTransform(p->timeStamp, p->objectID,
			p->posX, p->posY, p->posZ,
			p->rotX, p->rotY, p->rotZ);
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
		Plugin::fPlayerData(p->timeStamp, p->objectID, p->name, NAME_SIZE, p->score, p->health);
		break;
	}
	case PACKET_PLAYER_UPDATE:
	{
		PacketPlayerUpdate* p = (PacketPlayerUpdate*)packet;
		Plugin::fPlayerUpdate(p->timeStamp, p->objectID, p->score, p->health);
		break;
	}
	case PACKET_GAME_STATE:
	{
		PacketGameState* p = (PacketGameState*)packet;
		Plugin::fGameState(p->timeStamp, p->trueForStartFalseForEnd);
		break;
	}
	default:
	case PACKET_BASE_ID:
		assert(false);
		break;
	}
}