#include <iostream>

#include "list.h"

struct listStuff::ListVertex
{
    ListVertex *next;
    int value;
};

struct listStuff::SortedList
{
    ListVertex *top = nullptr;
    int size = 0;
};

listStuff::SortedList *listStuff::makeList()
{
    SortedList *newList = new SortedList;

    return newList;
}

/**
    Finds the pointer to last position `pos` where (value[pos] < value)
**/
listStuff::ListVertex *findPosition(listStuff::SortedList *list, int value)
{
    listStuff::ListVertex *currentPos = nullptr;
    auto nextPos = list->top;

    while (nextPos != nullptr)
    {
        if (nextPos->value >= value)
        {
            return currentPos;
        }

        currentPos = nextPos;
        nextPos = nextPos->next;
    }

    return currentPos;
}

void listStuff::insert(SortedList *list, int value)
{
    ListVertex *newVertex = new ListVertex;
    newVertex->value = value;

    auto prevPosition = findPosition(list, value);

    if (prevPosition == nullptr)
    {
        newVertex->next = list->top;
        list->top = newVertex;
    }
    else
    {
        newVertex->next = prevPosition->next;
        prevPosition->next = newVertex;
    }

    ++list->size;
}

void listStuff::deleteListElement(SortedList *list, int value)
{
    auto prevPosition = findPosition(list, value);

    if (prevPosition == nullptr)
    {
        if (list->size == 0 || list->top->value != value)
        {
            return;
        }

        const auto newTop = list->top->next;
        delete list->top;
        list->top = newTop;
    }
    else
    {
        if (prevPosition->next->value != value || prevPosition->next == nullptr)
        {
            return;
        }

        const auto deleteVertex = prevPosition->next;
        prevPosition->next = deleteVertex->next;
        delete deleteVertex;
    }

    --list->size;
}

void listStuff::printList(SortedList const * const list)
{
    if (list->size == 0)
    {
        std::cout << "nothing";
    }

    auto currentVertex = list->top;
    while (currentVertex != nullptr)
    {
        std::cout << currentVertex->value << ' ';
        currentVertex = currentVertex->next;
    }
    std::cout << std::endl;
}

void listStuff::deleteList(listStuff::SortedList *list)
{
    auto currentVertex = list->top;
    while (currentVertex != nullptr)
    {
        auto tempVertex = currentVertex;
        currentVertex = currentVertex->next;
        delete tempVertex;
    }

    delete list;
}
