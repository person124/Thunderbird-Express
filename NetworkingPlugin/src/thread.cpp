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

#include "thread.h"

#include <Windows.h>

// Internal command to handle thread launching
long __stdcall Thread_Launcher(Thread* arg)
{
	arg->running = 1;
	arg->func(arg->args);
	arg->running = 0;

	return 1;
}

// Creates a thread that starts at the specified function
int Thread_Create(Thread* out, ThreadFunc func, void* args)
{
	if (out && !out->handle && func)
	{
		out->running = 0;
		out->func = func;
		out->args = args;

		out->handle = CreateThread
		(
			0,
			0,
			(LPTHREAD_START_ROUTINE)Thread_Launcher,
			out,
			0,
			(LPDWORD)&out->id
		);

		return out->id;
	}

	return 0;
}

int Thread_Terminate(Thread* thread)
{
	if (thread && thread->handle)
	{
		int success = TerminateThread(thread->handle, -1);
		if (success)
		{
			success = CloseHandle(thread->handle);

			if (success)
			{
				thread->handle = 0;
				thread->running = 0;
				return 1;
			}
		}
	}

	return 0;
}