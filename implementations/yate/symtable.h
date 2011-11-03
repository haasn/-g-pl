/*
 * symtable.h
 *
 * Our implementation for the symbol table.  For now,
 * most likely a fix size array hash table, with index chaining for collisions.
 *
 * This table will contain a list of symbols, which will have details
 * about each identifier
 *
 *  Created on: Nov 3, 2011
 *  Author: yate
 */

#ifndef SYMTABLE_H_
#define SYMTABLE_H_

/* the arbitrary size of the symbol table*/
#define SIZE 503

/* used to hash the identifier from a string, to a position in the symbol table. */
int hash(char *key);

#endif /* SYMTABLE_H_ */
