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

#include "packet.hpp"

unsigned int PACKET_SIZES[PACKET_COUNT] =
{
	sizeof(Packet),
	sizeof(PacketTransform),
	sizeof(PacketColor),
	sizeof(PacketShout),
	sizeof(PacketBossHP),
	sizeof(PacketPlayerNumber),
	sizeof(PacketPlayerData),
	sizeof(PacketPlayerHealth),
	sizeof(PacketPlayerScore),
	sizeof(PacketGameState),

	// Non Unity Packets
	sizeof(PacketClientJoin),
	sizeof(PacketServerShutdown),
	sizeof(PacketClientDisconnect)
};