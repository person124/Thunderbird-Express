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

#include "connection.hpp"

#include <assert.h>

#include <RakNet/RakNetTypes.h>

Connection::Connection()
{
	mGUID = NULL;
}

Connection::Connection(const RawGUID& id)
{
	mGUID = new RakGUID();
	mGUID->g = id;
	mGUID->systemIndex = -1;
}

Connection::Connection(const RakGUID& id)
{
	mGUID = new RakGUID();
	*mGUID = id;
}

Connection::~Connection()
{
	if (mGUID)
		delete mGUID;
}

// Returns the id of the connection
RawGUID Connection::getID()
{
	assert(mGUID);
	return mGUID->g;
}