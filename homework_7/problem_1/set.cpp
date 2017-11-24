#include "set.h"

#include <iostream>

struct TreeVertex
{
    TreeVertex *leftSon = nullptr;
    TreeVertex *rightSon = nullptr;

    int value = 0;
};

struct setStuff::CustomSet
{
    TreeVertex *top = nullptr;
};

setStuff::CustomSet *setStuff::createSet()
{
    auto newSet = new CustomSet;

    return newSet;
}

void clearTree(TreeVertex *vertex)
{
    if (vertex == nullptr)
    {
        return;
    }

    clearTree(vertex->leftSon);
    clearTree(vertex->rightSon);

    delete vertex;
}

void setStuff::deleteSet(CustomSet *set)
{
    clearTree(set->top);
    delete set;
}

/**
* Returns position of new parent (or `nullptr` if top) to insert
* if element was not found
* Or returns element's position if it was found
*/
TreeVertex *findPosition(const setStuff::CustomSet *set, int value)
{
    auto currentPosition = set->top;
    auto nextPosition = set->top;
    while (nextPosition != nullptr)
    {
        currentPosition = nextPosition;

        if (value > currentPosition->value)
        {
            nextPosition = currentPosition->rightSon;
        }
        else if (value < currentPosition->value)
        {
            nextPosition = currentPosition->leftSon;
        }
        else
        {
            break;
        }
    }

    return currentPosition;
}


void setStuff::push(CustomSet *set, int value)
{
    auto currentPosition = findPosition(set, value);
    if (currentPosition != nullptr && currentPosition->value == value)
    {
        return;
    }

    TreeVertex *newVertex = new TreeVertex;

    if (currentPosition == nullptr)
    {
        set->top = newVertex;
    }
    else if (value > currentPosition->value)
    {
        currentPosition->rightSon = newVertex;
    }
    else
    {
        currentPosition->leftSon = newVertex;
    }
}

bool setStuff::isInSet(const CustomSet *set, int value)
{
    auto elementPosition = findPosition(set, value);

    return (elementPosition != nullptr && elementPosition->value == value);
}

void printSubtree(const TreeVertex *currentVertex, bool isAscendingOrder)
{
    if (currentVertex == nullptr)
    {
        return;
    }

    auto nextFirst = currentVertex->leftSon;
    auto nextSecond = currentVertex->rightSon;
    if (!isAscendingOrder)
    {
        std::swap(nextFirst, nextSecond);
    }

    printSubtree(nextFirst, isAscendingOrder);
    std::cout << currentVertex->value << ' ';
    printSubtree(nextSecond, isAscendingOrder);
}

void setStuff::printSet(const CustomSet *set, bool isAscendingOrder)
{
    printSubtree(set->top, isAscendingOrder);
}
