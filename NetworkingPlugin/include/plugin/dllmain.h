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

// Required file for a DLL, copied from class
#if (defined _WINDOWS || defined _WIN32)


#include <Windows.h>


int APIENTRY DllMain(
	HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		// dispatched when another process (e.g. application) consumes this library
		break;
	case DLL_THREAD_ATTACH:
		// dispatched when another thread consumes this library
		break;
	case DLL_THREAD_DETACH:
		// dispatched when another thread releases this library
		break;
	case DLL_PROCESS_DETACH:
		// dispatched when another process releases this library
		break;
	}
	return TRUE;
}


#endif	// (defined _WINDOWS || defined _WIN32)