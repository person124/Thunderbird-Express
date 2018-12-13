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

#ifndef _FUNCTION_TEMP_HPP_
#define _FUNCTION_TEMP_HPP_

struct Temp
{
	static void FuncTransform(unsigned long long, int, float, float, float, float, float, float, float, float, float)
	{}

	static void FuncColor(unsigned long long, int, int)
	{}

	static void FuncInt(unsigned long long, int)
	{}

	static void FuncPlayerData(unsigned long long, int, const char*, int, int)
	{}

	static void FuncPlayerUpdate(unsigned long long, int, int)
	{}

	static void FuncGameState(unsigned long long, bool)
	{}

	static void FuncVoid()
	{}
};

#endif