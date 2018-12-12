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

#ifndef _SERVER_HPP_
#define _SERVER_HPP_

#include "peer.hpp"

class Server : public Peer
{
public:
	Server();
	~Server();

protected:
	void handlePacket(Packet* packet, Connection* conn);

private:
	// Adds a connection to the list of connections, updates mConnectedClientCount
	void addConnection(Connection* conn);

	// Removes a connection from the list, updates mConnectedClientCount
	void removeConnection(Connection* conn);

	Connection** mConnections; // Instance of connected clients
	unsigned int mConnectedClientCount; // Number of connected clients
};

#endif