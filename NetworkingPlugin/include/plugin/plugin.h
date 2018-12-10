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

#ifndef _PLUGIN_H_
#define _PLUGIN_H_

#include "plugin/config.h"

#ifdef __cplusplus
extern "C"
{
#endif

	PLUGIN_SYMBOL
		void NetworkingPlugin_SendShout(int shout);

#ifdef __cplusplus
}
#endif

#endif