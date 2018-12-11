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

#ifndef _CONNECTION_H_
#define _CONNECTION_H_

// This is so we don't have to include raknet in every file
namespace RakNet
{
	struct RakNetGUID;
}

// This is to make it easier to access RakGUID
typedef RakNet::RakNetGUID RakGUID;

// This typedef is used to better access the actuall integral part of RakGUID
typedef unsigned long long RawGUID;

// This class is to encapsulate the RakGUID class
struct Connection
{
public:
	Connection();
	Connection(const RawGUID& id);
	Connection(const RakGUID& id);
	~Connection();

	// Returns the id of the connection
	RawGUID getID();

private:
	RakGUID* mGUID;
};

#endif