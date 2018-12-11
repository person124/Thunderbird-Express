
#include "../inludes/MessageFunctions.h"

//Add a new Client
void AddClient(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Client> *clientList)
{
	const ChatMessage *NewClientName = (ChatMessage *)packet->data;

	Client *temp = new Client;

	strcpy_s(temp->mName, NewClientName->mMessage);
	temp->mClientsGuid = packet->guid;

	clientList->push_back(*temp);
}

// server is created or updated and added to the Master server
void AddServer(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Server> *serverList)
{
	const Server *NewServer = (Server *)packet->data;
	bool exist = false;
	Server *temp = new Server;

	for (unsigned int i = 0; i < serverList->size() && !exist; ++i)
	{
		if (serverList->at(i).mAddress.getID() == packet->guid.g
			|| serverList->at(i).mPort == NewServer->mPort)
		{
			serverList->at(i).mCurrentClientNum = NewServer->mCurrentClientNum;
			exist = !exist;
		}
	}
	if (!exist) // hey this is creating a new server
	{
		strcpy_s(temp->mName, NewServer->mName);
		temp->mCurrentClientNum = NewServer->mCurrentClientNum;
		strcpy_s(temp->mSysAddress, NewServer->mSysAddress);
		temp->mAddress = packet->guid;
		temp->mMaxConnections = NewServer->mMaxConnections;
		temp->mPort = NewServer->mPort;

		serverList->push_back(*temp);
	}

	// TO-DO send back that you connected
}
// client sends a request to join a server
void RequestConnection(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Server> *serverList)
{
	ConnectionPossible myModifyMessage[1];

	bool exist = false;

	for (unsigned int i = 0; i < serverList->size() && !exist; ++i)
		if (serverList->at(i).mAddress.getID() == packet->guid.g)
		{
			exist = !exist;
			strcpy_s(myModifyMessage->mAddress, serverList->at(i).mSysAddress);
			myModifyMessage->mPort = serverList->at(i).mPort;
		}

	if (!exist)
		myModifyMessage->typeID = ID_GAME_MESSAGE_ClientServerCant;


	peer->Send((char*)myModifyMessage, sizeof(ConnectionPossible), HIGH_PRIORITY, RELIABLE_ORDERED, 0, packet->guid, false);
}
// remove Client from list of clients
void ClientDC(RakNet::Packet *packet, std::vector<Client> *clientList)
{
	const Client *RemoveClient = (Client *)packet->data;
	for (unsigned int i = 0; i < clientList->size(); ++i)
	{
		if (clientList->at(i).mName == RemoveClient->mName)
		{
			clientList->erase(clientList->begin() + i);
			break;
		}
	}
}

// remove Server from list of server
void ServerDC(RakNet::Packet *packet, std::vector<Server> *serverList)
{
	const Server *RemoveServer = (Server *)packet->data;
	for (unsigned int i = 0; i < serverList->size(); ++i)
	{
		if (serverList->at(i).mName == RemoveServer->mName)
		{
			serverList->erase(serverList->begin() + i);
			break;
		}
	}
}

// send messages over the master server
void SendChatMessage(RakNet::Packet *packet, RakNet::RakPeerInterface *peer, std::vector<Client> *clientList)
{
	const ChatMessage *MessageToSend = (ChatMessage *)packet->data;
	for (unsigned int i = 0; i < clientList->size(); ++i)
	{
		peer->Send((char*)MessageToSend, sizeof(ChatMessage), HIGH_PRIORITY, RELIABLE_ORDERED, 0, packet->guid, true);
	}
}
