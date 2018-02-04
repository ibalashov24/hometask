#pragma once

#include <string>

#include "list.h"

namespace hashStuff
{
    const int DEFAULT_CAPACITY = 100;

    struct HashSet;
    struct HashSetStats
    {
    	int setSize;
    	double avgBucketSize;
    };

    HashSet *createHashSet(int capacity = DEFAULT_CAPACITY);
    void deleteHashSet(HashSet *set);

    // Returns reference to the element in hash table with given key
    int &value(HashSet *set, const std::string &key);

    int getCapacity(const HashSet *set);
    int getSize(const HashSet *set);

    // Returns max and average size of bucket in `set`
    HashSetStats getStatistics(const HashSet *set);

    // Pointer to the position in set
    struct HashSetPosition
    {
        int bucketNo = -1;
        listStuff::ListVertex *listPosition = nullptr;
    };
    // Implements iteration over a set
    HashSetPosition getNext(const HashSet *set,
                            HashSetPosition lastPosition = HashSetPosition());
    // Get key of element on which `position` points
    std::string getKey(HashSetPosition position);
}
