#include "list.h"

struct listStuff::ListVertex
{
    ListVertex *next;

    std::pair<std::string, int> value;
};

struct listStuff::List
{
    ListVertex *begin = nullptr;

    int size = 0;
};

listStuff::List *listStuff::createList()
{
    listStuff::List *newList = new listStuff::List;

    return newList;
}

void listStuff::deleteList(List *list)
{
    auto currentElement = list->begin;
    while (currentElement != nullptr)
    {
        auto temp = currentElement->next;
        delete currentElement;
        currentElement = temp;
    }

    delete list;
}

void listStuff::insert(List *list,
                       const std::pair<std::string, int> &value,
                       ListVertex *previousVertex)
{
    ListVertex *newVertex = new ListVertex;
    newVertex->value = value;

    if (previousVertex == nullptr)
    {
        newVertex->next = list->begin;
        list->begin = newVertex;
    }
    else
    {
        newVertex->next = previousVertex->next;
        previousVertex->next = newVertex;
    }

    ++list->size;
}

std::pair<std::string, int> &listStuff::getValue(ListVertex *element)
{
    return element->value;
}

listStuff::ListVertex *listStuff::iterate(const List *list)
{
    return list->begin;
}

listStuff::ListVertex *listStuff::iterate(const ListVertex *vertex)
{
    return vertex->next;
}

int listStuff::getSize(const List *list)
{
    return (list == nullptr ? 0 : list->size);
}
