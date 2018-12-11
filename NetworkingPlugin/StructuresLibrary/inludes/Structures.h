#ifndef STRUCTURES_H
#define STRUCTURES_H


#include <stdio.h>
#include <string.h>
#include <ctype.h> // needed for strlen
#include "RakNet/RakPeerInterface.h" //include changed to correct directory
#include "RakNet/MessageIdentifiers.h" //include changed to correct directory
#include "RakNet/BitStream.h" // include changed to correct directory
#include "RakNet/RakNetTypes.h"  // MessageID // include changed to correct directory
#include <iostream>
#include <Vector>

#include <connection.hpp>
#include <packet.h>

//MessageTypes for the master server
enum GameMessages
{
	ID_GAME_MESSAGE_ClientConnecting = PACKET_END, // for a client connecting
	ID_GAME_MESSAGE_ServerConnecting, // for a server connecting
	ID_GAME_MESSAGE_MessageSending,   // client sending a message to people / person
	ID_GAME_MESSAGE_ClientDisconnect, // client disconnecting from game
	ID_GAME_MESSAGE_ServerDisconnect, // server disconnecting/ closing to master
	ID_GAME_MESSAGE_ClientServerConn, // client requesting a connection to the server
	ID_GAME_MESSAGE_ClientServerCan,  // client could connect
	ID_GAME_MESSAGE_ClientServerCant, // client cant connect
};

//Client structure and call location used primarilly for messaging 
struct Client
{
	char typeID = ID_GAME_MESSAGE_ClientConnecting;
	char mName[NAME_SIZE];
	Connection mClientsGuid;
};

//keeps a server's info for use in finding a 
struct Server
{
	char typeID = ID_GAME_MESSAGE_ServerConnecting;
	char mName[NAME_SIZE];
	char mSysAddress[NAME_SIZE];
	//char mIpAddress[14];
	int mCurrentClientNum, mMaxConnections;

	Connection mAddress;

	unsigned int mPort;
};

//super simple message storing and sending
struct ChatMessage
{
	char typeID = ID_GAME_MESSAGE_MessageSending;
	char mMessage[256];
};

struct ServerLocation
{
	char typeID = ID_GAME_MESSAGE_ClientServerConn;
	Connection mAddress;
};

struct ConnectionPossible
{
	char typeID = ID_GAME_MESSAGE_ClientServerCan;

	char mAddress[NAME_SIZE];
	unsigned int mPort;
};



#endif // !STRUCTURES_H

