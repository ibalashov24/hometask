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
* If `findParent` flag is set then parent of the vertex is always returned
*/
TreeVertex *findPosition(const setStuff::CustomSet *set,
                         int value,
                         bool findParent = false)
{
    auto nextPosition = set->top;
    TreeVertex *currentPosition = nullptr;
    while (nextPosition != nullptr)
    {
        if (findParent && nextPosition->value == value)
        {
            return currentPosition;
        }

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
    newVertex->value = value;

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
    if (set->top == nullptr)
    {
        std::cout << "Set is empty" << std::endl;
        return;
    }

    printSubtree(set->top, isAscendingOrder);
    std::cout << std::endl;
}

/**
    Prepare a vertex that will be in the tree at the site of the removed vertex
*/
TreeVertex *prepareSubstituteVertex(TreeVertex *erasedElement)
{
    auto currentElement = erasedElement;
    auto lastElement = currentElement;

    if (currentElement->leftSon != nullptr)
    {
        currentElement = currentElement->leftSon;
        while (currentElement->rightSon != nullptr)
        {
            lastElement = currentElement;
            currentElement = currentElement->rightSon;
        }

        if (erasedElement == lastElement)
        {
            lastElement->leftSon = currentElement->leftSon;
        }
        else
        {
            lastElement->rightSon = currentElement->leftSon;
        }
    }
    else if (currentElement->rightSon != nullptr)
    {
        currentElement = currentElement->rightSon;
        while (currentElement->leftSon != nullptr)
        {
            lastElement = currentElement;
            currentElement = currentElement->leftSon;
        }

        if (erasedElement == lastElement)
        {
            lastElement->rightSon = currentElement->rightSon;
        }
        else
        {
            lastElement->leftSon = currentElement->rightSon;
        }
    }

    return currentElement;
}

void setStuff::deleteElement(CustomSet *set, int value)
{
    auto erasedElementParent = findPosition(set, value, true);
    auto erasedElement = erasedElementParent;
    if (erasedElementParent == nullptr)
    {
        erasedElement = set->top;
    }
    else
    {
        erasedElement = (value > erasedElementParent->value) ?
                                        erasedElementParent->rightSon:
                                        erasedElementParent->leftSon;
    }

    if (erasedElement == nullptr || erasedElement->value != value)
    {
        return;
    }

    auto currentElement = prepareSubstituteVertex(erasedElement);

    if (currentElement == erasedElement)
    {
        currentElement = nullptr;
    }
    else
    {
        currentElement->rightSon = erasedElement->rightSon;
        currentElement->leftSon = erasedElement->leftSon;
    }

    if (erasedElementParent == nullptr)
    {
        set->top = currentElement;
    }
    else if (erasedElement->value > erasedElementParent->value)
    {
        erasedElementParent->rightSon = currentElement;
    }
    else
    {
        erasedElementParent->leftSon = currentElement;
    }

    delete erasedElement;
}
