#include "fmod.h"
#include "fmod_errors.h"
#include <iostream>
#include <cstdlib>
#include <fmod.h>
#include <fmod.hpp>
#include <windows.h>
using namespace std;

FMOD::System* System;	// the global variable for talking to FMOD

void FmodErrorCheck(FMOD_RESULT result)	// this is an error handling function
{						// for FMOD errors
	if (result != FMOD_OK)
	{
		printf("FMOD error! (%d) %s\n", result, FMOD_ErrorString(result));
		exit(-1);
	}
}


int main() {
	FMOD_RESULT result;
	result = FMOD::System_Create(&System);
	FmodErrorCheck(result);

	result = System->init(32, FMOD_INIT_NORMAL, 0);
	FmodErrorCheck(result);

	return 0;

}