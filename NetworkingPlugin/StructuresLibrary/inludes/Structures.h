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
	char Name[32];
	RakNet::RakNetGUID clientsGuid;
};

struct Server
{

};


#endif // !STRUCTURES_H

