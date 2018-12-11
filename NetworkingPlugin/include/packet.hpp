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
#include <string.h>

// This is the same definition that RakNet uses for time
typedef unsigned long long Time;

enum PacketTypes
{
	PACKET_BASE_ID = ID_USER_PACKET_ENUM,
	PACKET_TRANSFORM,
	PACKET_COLOR,
	PACKET_SHOUT,
	PACKET_BOSS_HP,
	PACKET_PLAYER_DATA,
	PACKET_GAME_STATE,
	PACKET_END
};

// Use this as #pragma BIT_START before a the section you want to be aligned
#define BIT_START pack(push, 1)

// Use this as #pragma BIT_END after the section you want to be aligned
#define BIT_END pack(pop)

#define NAME_SIZE 30

#pragma BIT_START
struct Packet
{
	Packet(const unsigned int id):packetID(id) {}

	// Time stamp things
	const unsigned char useTimeStamp = ID_TIMESTAMP;
	Time timeStamp;

	// TO-DO ADD TIME CODE STUFF
	const unsigned char packetID;
};

struct PacketTransform : public Packet
{
	PacketTransform(unsigned int id,
		float x, float y, float z,
		float rX, float rY, float rZ):Packet(PACKET_TRANSFORM)
	{
		objectID = id;
		posX = x;
		posY = y;
		posZ = z;
		rotX = rX;
		rotY = rY;
		rotZ = rZ;
	}

	unsigned int objectID;
	float posX, posY, posZ;
	float rotX, rotY, rotZ;
};

struct PacketColor : public Packet
{
	PacketColor(unsigned int id,
		unsigned char r, unsigned char g, unsigned char b,
		unsigned char a) :Packet(PACKET_COLOR)
	{
		objectID = id;
		red = r;
		green = g;
		blue = b;
		alpha = a;
	}

	unsigned int objectID;
	unsigned char red;
	unsigned char green;
	unsigned char blue;
	unsigned char alpha;
};

struct PacketShout : public Packet
{
	PacketShout(unsigned int id):Packet(PACKET_SHOUT)
	{
		shoutID = id;
	}

	unsigned int shoutID;
};

struct PacketBossHP : public Packet
{
	PacketBossHP(unsigned int bossHP):Packet(PACKET_BOSS_HP)
	{
		hp = bossHP;
	}

	unsigned int hp;
};

struct PacketPlayerData : public Packet
{
	PacketPlayerData(unsigned int id, char playerName[NAME_SIZE],
		unsigned int playerScore, unsigned int playerHealth):Packet(PACKET_PLAYER_DATA)
	{
		objectID = id;
		strcpy_s(name, NAME_SIZE, playerName);
		score = playerScore;
		health = playerHealth;
	}

	unsigned int objectID;
	char name[NAME_SIZE];
	unsigned int score;
	unsigned int health;
};

struct PacketGameState : public Packet
{
	PacketGameState(bool gameStarted):Packet(PACKET_GAME_STATE)
	{
		trueForStartFalseForEnd = gameStarted;
	}

	bool trueForStartFalseForEnd;
};
#pragma BIT_END



// This definition is to get the toal number of packets
#define PACKET_COUNT (PACKET_END - PACKET_BASE_ID)

// This array contains the size of the packets, just pass the packet's id
// As the index parameter
extern unsigned int PACKET_SIZES[PACKET_COUNT];

#endif