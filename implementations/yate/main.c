/*
 * main.c
 *
 *  Created on: Nov 3, 2011
 *      Author: yate
 */

#include <stdio.h>
#include "symtable.h"

int main(int argc, char* argv[]) {

	printf("%d\n",hash("t"));
	printf("%d\n",hash("te"));
	printf("%d\n",hash("tes"));
	printf("%d\n",hash("test"));
	printf("%d\n",hash("test1"));
	printf("%d\n",hash("test2"));
	printf("%d\n",hash("test3"));
	printf("%d\n",hash("test4"));
	return 0;
}

