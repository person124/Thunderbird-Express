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

struct Client
{
	char mName[32];
	RakNet::RakNetGUID mClientsGuid;
};

struct Server
{
	char mName[32];
	char mIpAddress[14];
	int mMaxClients, mCurrentClientNumber;
};

struct ChatMessage
{
	char mMessage[256];
};



#endif // !STRUCTURES_H

