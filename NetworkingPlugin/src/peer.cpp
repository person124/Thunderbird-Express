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

#include "peer.hpp"

#include <RakNet/RakPeerInterface.h>

#include "connection.hpp"
#include "packet.h"

Peer::Peer()
{
	mPeer = RakPeer::GetInstance();
}

Peer::~Peer()
{
	RakPeer::DestroyInstance(mPeer);
}

// Sends a packet to all connected clients
void Peer::sendPacketToAll(Packet* packet)
{
	sendPacket(packet, NULL, true);
}

// Sends a packet to all clients but the specified one
void Peer::sendPacketBut(Packet* packet, Connection* dest)
{
	sendPacket(packet, dest, false);
}

// Sends a packet to the destination, with the option to send to all
void Peer::sendPacket(Packet* packet, Connection* dest, bool sendToAll)
{
	RakGUID d;
	
	// If dest is null then set the value of d accordingly
	if (!dest)
		d = RakNet::UNASSIGNED_RAKNET_GUID;
	else
		d.g = dest->getID();

	// TO-DO Set the packet timestamp here

	mPeer->Send
	(
		(const char*)packet,
		PACKET_SIZES[packet->packetID],
		HIGH_PRIORITY,
		RELIABLE_ORDERED,
		0,
		d,
		sendToAll
	);
}