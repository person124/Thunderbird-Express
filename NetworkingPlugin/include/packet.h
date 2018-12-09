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

#ifndef _PACKET_H_
#define _PACKET_H_

// Needs to be included for packet id's
#include <RakNet/MessageIdentifiers.h>

// This is the same definition that RakNet uses for time
typedef uint64_t Time;

enum PacketTypes
{
	PACKET_BASE_ID = ID_USER_PACKET_ENUM,
	PACKET_END
};

// Use this as #pragma BIT_START before a the section you want to be aligned
#define BIT_START pack(push, 1)

// Use this as #pragma BIT_END after the section you want to be aligned
#define BIT_END pack(pop)

#pragma BIT_START
struct Packet
{
	// Time stamp things
	const unsigned char useTimeStamp = ID_TIMESTAMP;
	Time timeStamp;

	// TO-DO ADD TIME CODE STUFF
	unsigned char packetID;
};
#pragma BIT_END



// This definition is to get the toal number of packets
#define PACKET_COUNT (PACKET_END - PACKET_BASE_ID)

// This array contains the size of the packets, just pass the packet's id
// As the index parameter
unsigned int PACKET_SIZES[PACKET_COUNT] =
{
	sizeof(Packet)
};

#endif