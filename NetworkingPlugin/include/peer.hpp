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

#ifndef _PEER_H_
#define _PEER_H_

#include "thread.h"

// This is so we don't have to include raknet in every file
namespace RakNet
{
	class RakPeerInterface;
}

// This is an easier way to reference RakPeerInterface
typedef RakNet::RakPeerInterface RakPeer;

struct Packet;
struct Connection;

// This class acts as a base class that encapsulates RakPeerInterface
class Peer
{
public:
	Peer();
	~Peer();

	// Sends a packet to all connected clients
	void sendPacketToAll(Packet* packet);
	// Sends a packet to all clients but the specified one
	void sendPacketBut(Packet* packet, Connection* dest);
	// Sends a packet to the destination, with the option to send to all
	void sendPacket(Packet* packet, Connection* dest, bool sendToAll = false);

	// Starts the threaded networking loop
	void startNetworkingLoop();

protected:
	// Pure virtual method that incoming packets are sent to, conn is the
	// where the packet came from
	virtual void handlePacket(Packet* packet, Connection* conn) = 0;

	bool mRunning = false;

private:
	// friend function that calls the internal networking loop
	// This function is out of order in the cpp to satisfy the
	// compilier
	friend void loopHandler(Peer* peer);

	// This function handles packet recieving and event processing.
	// NOTE: this function does not contain a loop,
	// it must be called by one.
	void internalNetworkingLoop();

	RakPeer* mPeer;

	Thread* mThread;
};

#endif