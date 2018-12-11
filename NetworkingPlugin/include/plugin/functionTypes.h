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

// objectID, x, y, z, rX, rY, rZ
typedef void(*FuncTransform)
(int, float, float, float, float, float, float);

// objectID, color
typedef void(*FuncColor)(int, int);

// Used for shout, bossHP
typedef void(*FuncInt)(int);

// objectID, name, score, health
typedef void(*FuncPlayerData)(int, char[33], int, int);

// objectID, score, health
typedef void(*FuncPlayerUpdate)(int, int, int);

// state
typedef void(*FuncGameState)(bool);

#endif