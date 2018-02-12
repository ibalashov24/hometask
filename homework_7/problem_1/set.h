#pragma once

namespace setStuff
{
    struct CustomSet;

    // Creates new empty set
    CustomSet *createSet();

    // Inserts element to the set
    void push(CustomSet *set, int value);

    // Checks if given value is in set
    bool isInSet(const CustomSet *set, int value);

    // Erases element with given value from the set (element must exist!)
    void deleteElement(CustomSet *set, int value);

    // Prints all elements of `set` to cout in ascending (or descending) order
    void printSet(const CustomSet *set, bool isAscendingOrder = true);

    // Fully deletes the set
    void deleteSet(CustomSet *set);
}
