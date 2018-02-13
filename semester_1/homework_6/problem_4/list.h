#pragma once

#include <string>

namespace listStuff
{
	// Service list structures
    struct ListVertex;
    struct PairStringList;

    // Creates new empty list
    PairStringList *createList();

    // Erases given vertex from the list
    void eraseElement(PairStringList *list, ListVertex *vertex);

    // Erases all elements in the list (makes it empty)
    void cleanList(PairStringList *list);

    // Fully deletes the list
    void deleteList(PairStringList *list);

    // Inserts new element to the list (with given pointer or value)
    // If you want to insert value into the beginning then
    // previousVertex == nullptr
    void insert(PairStringList *list,
                ListVertex *newVertex,
                ListVertex *previousVertex = nullptr);
    void insert(PairStringList *list,
                const std::pair<std::string, std::string> &value,
                ListVertex *previousVertex = nullptr);

    // Extracts first element from the list
    ListVertex *ejectFirst(PairStringList *list);

    // Checks if list is empty
    bool isEmpty(const PairStringList *list);

    // Returns count of elements in the list
    int getSize(const PairStringList *list);

    // Returns value contained in 'element'
    std::pair<std::string, std::string> getValue(const ListVertex *element);

    // Returns first element of `list`
    ListVertex *iterate(const PairStringList *list);
    // Returns next element after `vertex`
    ListVertex *iterate(const ListVertex *vertex);

    // Prints list elements to cout (stdout)
    void printList(const PairStringList *list);
}
