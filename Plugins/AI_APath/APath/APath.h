#ifdef APATH_H
#else

	#include "UnityEngine.hpp"
	#include "PathModels.hpp"
	#include <iostream>

	#ifdef APATH_EXPORT_MODE
		#define FUNCTION_API __declspec(dllexport)
	#else
		#define FUNCTION_API __declspec(dllimport) 
	#endif

#endif