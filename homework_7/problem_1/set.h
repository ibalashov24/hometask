#pragma once

namespace setStuff
{
    struct CustomSet;

    // Creates new empty set
    CustomSet *createSet();

    void push(CustomSet *set, int value);
    bool isInSet(const CustomSet *set, int value);
    void deleteElement(CustomSet *set, int value);

    // Prints all elements of `set` to cout in ascending (or descending) order
    void printSet(const CustomSet *set, bool isAscendingOrder = true);

    void deleteSet(CustomSet *set);
}
