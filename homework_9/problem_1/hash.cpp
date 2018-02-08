#include "hash.h"

#include <vector>
#include <algorithm>

#include <iostream>

struct hashStuff::HashSet
{
    std::vector<listStuff::List *> bucket;
    int size = 0;
};

hashStuff::HashSet *hashStuff::createHashSet(int capacity)
{
    auto newSet = new HashSet{std::vector<listStuff::List *>(capacity, nullptr)};
    return newSet;
}

void hashStuff::deleteHashSet(HashSet *set)
{
    for (auto ptr : set->bucket)
    {
        if (ptr != nullptr)
        {
            listStuff::deleteList(ptr);
        }
    }

    delete set;
}

int hashStuff::getCapacity(const HashSet *set)
{
    return set->bucket.size();
}

int hashStuff::getSize(const HashSet *set)
{
    return set->size;
}

hashStuff::HashSetStats hashStuff::getStatistics(const HashSet *set)
{
    int maxSize = 0;
    int nonEmptyCount = 0;
    for (auto b : set->bucket)
    {
        maxSize = std::max(listStuff::getSize(b), maxSize);
        if (b != nullptr)
        {
            ++nonEmptyCount;
        }
    }

    if (nonEmptyCount == 0)
    {
        return HashSetStats{0, 0.0};
    }
    return HashSetStats{maxSize,
                          set->size / static_cast<double>(nonEmptyCount)};
}

const unsigned int MODULO = 1190494759; // Prime number #60000000
unsigned int getHash(const std::string &input, int modulo = MODULO)
{
    const unsigned int PRIME = 541; // Prime number #100

    unsigned int result = 0;
    unsigned int power = 1;
    for (auto e : input)
    {
        result = (result + ((power * e) % modulo)) % modulo;
        power = (power * PRIME) % modulo;
    }

    return result;
}

int &hashStuff::value(HashSet *set, const std::string &key)
{
    auto hash = getHash(key, getCapacity(set));

    if (set->bucket[hash] == nullptr)
    {
        set->bucket[hash] = listStuff::createList();
    }

    auto currentPosition = listStuff::iterate(set->bucket[hash]);
    while (currentPosition != nullptr &&
           listStuff::getValue(currentPosition).first != key)
    {
        currentPosition = listStuff::iterate(currentPosition);
    }

    if (currentPosition == nullptr)
    {
        listStuff::insert(set->bucket[hash], std::make_pair(key, 0), nullptr);
        set->size++;
        currentPosition = listStuff::iterate(set->bucket[hash]);
    }

    return std::get<1>(listStuff::getValue(currentPosition));
}

hashStuff::HashSetPosition
hashStuff::getNext(const HashSet *set, HashSetPosition lastPosition)
{
    // Check if we want non-first element
    if (lastPosition.listPosition != nullptr)
    {
        lastPosition.listPosition =
                                listStuff::iterate(lastPosition.listPosition);
    }

    if (lastPosition.listPosition == nullptr)
    {
        ++lastPosition.bucketNo;
        while (lastPosition.bucketNo < hashStuff::getCapacity(set) &&
               set->bucket[lastPosition.bucketNo] == nullptr)
        {
            ++lastPosition.bucketNo;
        }

        if (lastPosition.bucketNo >= hashStuff::getCapacity(set))
        {
            lastPosition.listPosition = nullptr;
        }
        else
        {
            lastPosition.listPosition =
                        listStuff::iterate(set->bucket[lastPosition.bucketNo]);
        }
    }

    return lastPosition;
}

std::string hashStuff::getKey(HashSetPosition position)
{
    if (position.listPosition == nullptr)
    {
        return "";
    }
    else
    {
        return listStuff::getValue(position.listPosition).first;
    }
}
