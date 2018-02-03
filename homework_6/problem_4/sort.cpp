#include "sort.h"

#include <functional>

/**
 * Merges (both) sorted `sourceList` into `destList`
 */
void merge(listStuff::PairStringList *destList,
           listStuff::PairStringList *sourceList,
		   std::function<bool(const std::pair<std::string, std::string> &,
                   	   	   	  const std::pair<std::string, std::string> &)> cmp)
{
    auto nextToPush = listStuff::ejectFirst(sourceList);
    decltype(nextToPush) previousElement = nullptr;

    for (auto pos = listStuff::iterate(destList); pos; pos = listStuff::iterate(pos))
    {
        while (nextToPush != nullptr &&
               cmp(listStuff::getValue(nextToPush), listStuff::getValue(pos)))
        {
            listStuff::insert(destList, nextToPush, previousElement);
            previousElement = nextToPush;

            nextToPush = listStuff::ejectFirst(sourceList);
        }

        previousElement = pos;
    }

    while (nextToPush != nullptr)
    {
        listStuff::insert(destList, nextToPush, previousElement);
        previousElement = nextToPush;

        nextToPush = listStuff::ejectFirst(sourceList);
    }
}

/**
 * Effectively moves `count` elements from the beginning of
 * `inputList` to new list
 */
listStuff::PairStringList *split(listStuff::PairStringList *inputList,
                                 int count)
{
    listStuff::PairStringList *newList = listStuff::createList();

    for (int i = 0; i < count; ++i)
    {
        listStuff::insert(newList, listStuff::ejectFirst(inputList));
    }

    return newList;
}

void sortingStuff::mergeSort(listStuff::PairStringList *list,
                             bool (*cmp)(const std::pair<std::string, std::string> &,
                                         const std::pair<std::string, std::string> &))
{
    auto newList = split(list, listStuff::getSize(list) / 2);

    if (listStuff::getSize(list) > 1)
    {
        mergeSort(newList, cmp);
        mergeSort(list, cmp);
    }

    merge(list, newList, cmp);

    listStuff::deleteList(newList);
}
