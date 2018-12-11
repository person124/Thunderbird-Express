#ifndef MESSAGE_FUNCTIONS
#define MESSAGE_FUNCTIONS

#include "Structures.h"

//Add a new Client
void AddClient(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Client> *clientList);

// server is created or updated and added to the Master server
void AddServer(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Server> *serverList);

// client sends a request to join a server
void RequestConnection(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Server> *serverList);

// remove Client from list of clients
void ClientDC(RakNet::Packet *packet, std::vector<Client> *clientList);

// remove Server from list of server
void ServerDC(RakNet::Packet *packet, std::vector<Server> *serverList);



#endif // !MESSAGE_FUNCTIONS

