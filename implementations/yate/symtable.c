/*
 * symtable.c
 *
 *  Created on: Nov 3, 2011
 *  Author: Mike
 */

#include "symtable.h"

int hash(char *key) {

	int result = 0;
	int i = 0;

	while (key[i] != '\0') {
		result = ((result << 4) + key[i]) % SIZE;
		i++;
	}
	return result;
}

