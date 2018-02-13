#include "list.h"

#include <iostream>

struct listStuff::ListVertex
{
    ListVertex *prev;
    ListVertex *next;

    std::pair<std::string, std::string> value;
};

struct listStuff::PairStringList
{
    ListVertex *begin = nullptr;

    int size = 0;
};

listStuff::PairStringList *listStuff::createList()
{
    listStuff::PairStringList *newList = new listStuff::PairStringList;

    return newList;
}

void listStuff::printList(const PairStringList *list)
{
    for (auto pos = list->begin; pos; pos = pos->next)
    {
        std::cout << pos->value.first << ' ' << pos->value.second << std::endl;
    }
}

int listStuff::getSize(const PairStringList *list)
{
    return list->size;
}

bool listStuff::isEmpty(const PairStringList *list)
{
    return (list->begin == nullptr);
}

/**
 * Ejects first element from the list without deleting it
 */
listStuff::ListVertex *listStuff::ejectFirst(listStuff::PairStringList *list)
{
    auto oldFirstElement = list->begin;

    if (list->size != 0)
    {
        list->begin = list->begin->next;
        --list->size;
    }

    return oldFirstElement;
}

void listStuff::eraseElement(PairStringList *list, ListVertex *vertex)
{
    if (vertex == nullptr)
    {
        std::cerr << "Do not erase nullptr, please" << std::endl;
        return;
    }

    auto prevElement = vertex->prev;
    auto nextElement = vertex->next;
    if (vertex == list->begin)
    {
        list->begin = nextElement;

        if (nextElement != nullptr)
        {
        	nextElement->prev = nullptr;
        }
    }
    else
    {
    	if (nextElement != nullptr)
    	{
    		nextElement->prev = prevElement;
    	}
        prevElement->next = nextElement;
    }

    delete vertex;

    --list->size;
}

void listStuff::cleanList(PairStringList *list)
{
    while  (!isEmpty(list))
    {
        eraseElement(list, list->begin);
    }
}

void listStuff::deleteList(PairStringList *list)
{
    cleanList(list);

    delete list;
}

void listStuff::insert(listStuff::PairStringList *list,
            listStuff::ListVertex *newVertex,
            listStuff::ListVertex *previousVertex)
{
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

void listStuff::insert(PairStringList *list,
                       const std::pair<std::string, std::string> &value,
                       ListVertex *previousVertex)
{
    ListVertex *newVertex = new ListVertex;
    newVertex->value = value;

    insert(list, newVertex, previousVertex);
}

std::pair<std::string, std::string> listStuff::getValue(const ListVertex *element)
{
    return element->value;
}

listStuff::ListVertex *listStuff::iterate(const PairStringList *list)
{
    return list->begin;
}

listStuff::ListVertex *listStuff::iterate(const ListVertex *vertex)
{
    return vertex->next;
}
