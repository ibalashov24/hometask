#pragma once

#include <string>

namespace listStuff
{
    struct ListVertex;
    struct PairStringList;

    PairStringList *createList();

    void eraseElement(PairStringList *list, ListVertex *vertex);
    void cleanList(PairStringList *list);
    void deleteList(PairStringList *list);

    // If you want to insert value into the beginning then
    // previousVertex == nullptr
    void insert(PairStringList *list,
                ListVertex *newVertex,
                ListVertex *previousVertex = nullptr);
    void insert(PairStringList *list,
                const std::pair<std::string, std::string> &value,
                ListVertex *previousVertex = nullptr);

    ListVertex *ejectFirst(PairStringList *list);
    bool isEmpty(const PairStringList *list);
    int getSize(const PairStringList *list);
    std::pair<std::string, std::string> getValue(const ListVertex *element);

    // Returns first element of `list`
    ListVertex *iterate(const PairStringList *list);
    // Returns next element after `vertex`
    ListVertex *iterate(const ListVertex *vertex);

    void printList(const PairStringList *list);
}
