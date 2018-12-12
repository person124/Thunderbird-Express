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

#ifndef _FUNCTION_TYPES_H_
#define _FUNCTION_TYPES_H_

// time, objectID, x, y, z, rX, rY, rZ
typedef void(*FuncTransform)
(unsigned long long, int, float, float, float, float, float, float);

// time, objectID, color
typedef void(*FuncColor)(unsigned long long, int, int);

// Used for shout, bossHP
typedef void(*FuncInt)(unsigned long long, int);

// time, objectID, name, score, health
typedef void(*FuncPlayerData)(unsigned long long, int, const char*, int, int);

// time, objectID, score, health
typedef void(*FuncPlayerUpdate)(unsigned long long, int, int, int);

// time, state
typedef void(*FuncGameState)(unsigned long long, bool);

#endif