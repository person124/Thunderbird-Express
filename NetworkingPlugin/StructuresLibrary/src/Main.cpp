/*
* Alex Rader
* 82-1023256
*
* EGP-405-02
* Final Project
* 12/10/18
*
* Certificate of Authenticity:
* We certify that this work is entirely our own. The assessor of this project may reproduce this
* project and provide copies to other academic staff, and/or communicate a copy of this project
* to a plagiarism-checking service, which may retain a copy of the project on its database.
*
*/
#include "../inludes/Structures.h"
#include "../inludes/MessageFunctions.h"

// needed for RakNet Defined classes, and structs 
using namespace RakNet;

int main(void)
{
	//constant IP and port for master server
	const unsigned short SERVER_PORT = 11111;
	const unsigned short MAX_CLIENTS = 10000;
	RakNet::RakPeerInterface *peer = RakNet::RakPeerInterface::GetInstance();
	RakNet::Packet *packet;

	// creating the server port and startup
	RakNet:SocketDescriptor sd(SERVER_PORT, 0);
	peer->Startup(MAX_CLIENTS, &sd, 1);

	peer->SetMaximumIncomingConnections(MAX_CLIENTS);


	std::vector<Client> *myClientList;
	std::vector<Server> *myServerList;

	while (1)
	{
		for (packet = peer->Receive(); packet; peer->DeallocatePacket(packet), packet = peer->Receive())
		{
			switch (packet->data[0])
			{
			case ID_GAME_MESSAGE_ClientConnecting:
				AddClient(packet, peer, myClientList);
				break;
			case ID_GAME_MESSAGE_ServerConnecting:
				AddServer(packet, peer, myServerList);
				break;
				//ToDo
			case ID_GAME_MESSAGE_MessageSending:
				break;
			case ID_GAME_MESSAGE_ClientDisconnect:
				ClientDC(packet, myClientList);
				break;
			case ID_GAME_MESSAGE_ServerDisconnect:
				ServerDC(packet, myServerList);
				break;
			case ID_GAME_MESSAGE_ClientServerConn:
				RequestConnection(packet, peer, myServerList);
				break;
			}
		}
	}
}