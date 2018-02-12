#include "cycleList.h"

struct listStuff::ListVertex
{
	ListVertex *next;
	int value;
};

struct listStuff::CyclicList
{
    ListVertex *top = nullptr;
    int size = 0;
};

listStuff::CyclicList *listStuff::makeCyclicList()
{
    CyclicList *newList = new CyclicList;

    return newList;
}

listStuff::ListVertex *listStuff::getListTop(CyclicList *list)
{
    return list->top;
}

int listStuff::getListSize(const CyclicList *list)
{
    return list->size;
}

listStuff::ListVertex *listStuff::getNextVertex(ListVertex *vertex)
{
    return vertex->next;
}

int listStuff::getVertexValue(const ListVertex *vertex)
{
    return vertex->value;
}

void listStuff::insertToCyclicList(listStuff::CyclicList *list,
                                   int value)
{
    ListVertex *newVertex = new ListVertex;
    newVertex->value = value;

    if (list->top == nullptr)
    {
        newVertex->next = newVertex;
        list->top = newVertex;
    }
    else
    {
        newVertex->next = list->top->next;
        list->top->next = newVertex;
        list->top = newVertex;
    }

    ++list->size;
}

void listStuff::deleteFromCyclicList(listStuff::CyclicList *list,
                                     ListVertex *position)
{
    if (position == nullptr)
    {
        return;
    }

    auto deletePosition = position->next;
    position->next = deletePosition->next;

    if (deletePosition == list->top)
    {
        list->top = position->next;
    }

    delete deletePosition;
    --list->size;

    if (list->size == 0)
    {
        list->top = nullptr;
    }
}

void listStuff::clearCyclicList(listStuff::CyclicList *list)
{
    while (list->size != 0)
    {
        deleteFromCyclicList(list, list->top);
    }

    delete list;
}

