#pragma once

#include <string>

// Lite version of the list from 6.4
namespace listStuff
{
    struct ListVertex;
    struct List;

    List *createList();
    void deleteList(List *list);

    // If you want to insert value into the beginning then
    // previousVertex == nullptr
    void insert(List *list,
                const std::pair<std::string, int> &value,
                ListVertex *previousVertex = nullptr);

    std::pair<std::string, int> &getValue(ListVertex *element);

    // Returns first element of `list`
    ListVertex *iterate(const List *list);
    // Returns next element after `vertex`
    ListVertex *iterate(const ListVertex *vertex);
}
