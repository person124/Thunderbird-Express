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

#include <RakNet/GetTime.h>
#include <RakNet/RakPeerInterface.h>

#include "connection.hpp"
#include "packet.hpp"

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

	packet->timeStamp = RakNet::GetTime();

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

// Starts the peer as a server, opening it on the specified
// port, with the specified number of max connections
// Returns false if the startup failed
bool Peer::startServer(unsigned int port, unsigned int maxClients)
{
	mIP = "";
	mPort = port;
	mMaxClients = maxClients;

	RakNet::SocketDescriptor sd(mPort, 0);
	RakNet::StartupResult result = mPeer->Startup(mMaxClients, &sd, 1);

	if (result != RakNet::StartupResult::RAKNET_STARTED)
		return false;

	mPeer->SetMaximumIncomingConnections(mMaxClients);

	return true;
}

// Starts the peer as a client, connecting it to the specified
// ip and port
// Returns false if the startup failed
bool Peer::startClient(const std::string& ip, unsigned int port)
{
	mIP = ip;
	mPort = port;
	mMaxClients = 1;

	RakNet::SocketDescriptor sd;

	mPeer->Startup(1, &sd, 1);

	RakNet::ConnectionAttemptResult result =
		mPeer->Connect(mIP.c_str(), mPort, 0, 0);

	if (result != RakNet::ConnectionAttemptResult::CONNECTION_ATTEMPT_STARTED)
		return false;

	RakNet::Packet* packet = NULL;
	while (packet == NULL)
		packet = mPeer->Receive();
	if (packet->data[0] != ID_CONNECTION_REQUEST_ACCEPTED)
		return false;

	return true;
}

// friend function that calls the internal networking loop
// This function is out of order in the cpp to satisfy the
// compilier
void loopHandler(Peer* peer)
{
	while (peer->mRunning)
	{
		peer->internalNetworkingLoop();
	}
}

// Starts the threaded networking loop
void Peer::startNetworkingLoop()
{
	assert(!(mThread && mThread->running));

	mRunning = true;

	mThread = new Thread();
	mThread->handle = NULL;
	Thread_Create(mThread, (ThreadFunc)loopHandler, this);
}

// This function handles packet recieving and event processing.
// NOTE: this function does not contain a loop,
// it must be called by one.
void Peer::internalNetworkingLoop()
{
	RakNet::Packet* incommming;
	
	for
	(
		incommming = mPeer->Receive();
		incommming;
		mPeer->DeallocatePacket(incommming), incommming = mPeer->Receive()
	)
	{
		if (incommming->data[2] >= PACKET_BASE_ID)
		{
			Packet* packet = (Packet*)incommming->data;
			Connection connection = Connection(incommming->guid.g);
			handlePacket(packet, &connection);
		}
	}
}