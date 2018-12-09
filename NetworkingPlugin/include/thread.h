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

// This file was copied from class with slight modifications
#ifndef _THREAD_H_
#define	_THREAD_H_

#ifdef __cplusplus
extern "C"
{
#else
typedef struct Thread Thread;
#endif

typedef void(*ThreadFunc)(void*);

struct Thread
{
	void* handle;
	unsigned int id;

	int running;
	ThreadFunc func;
	void* args;
};

// Creates a thread that starts at the specified function
int Thread_Create(Thread* out, ThreadFunc func, void* args);
// Termiates the specified thread
int Thread_Terminate(Thread* thread);

#ifdef __cplusplus
}
#endif

#endif