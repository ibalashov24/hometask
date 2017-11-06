#include "sort.h"

#include <utility>

void sorting::insertionSort(std::vector<int>::iterator begin,
                            std::vector<int>::iterator end)
{
    for (auto pos_i = begin + 1; pos_i != end; ++pos_i)
    {
        for (auto pos_j = pos_i; pos_j >= begin + 1; --pos_j)
        {
            if (*pos_j < *(pos_j - 1))
            {
                std::iter_swap(pos_j, pos_j - 1);
            }
        }
    }
}

std::vector<int>::iterator partition(std::vector<int>::iterator begin,
                                              std::vector<int>::iterator end)
{
    int median = *(begin + (end - begin) / 2);

    auto insertionPos = begin;
    for (auto pos = begin; pos != end; ++pos)
    {
        if (*pos <= median)
        {
            std::iter_swap(pos, insertionPos);
            ++insertionPos;
        }
    }

    return insertionPos;
}

void sorting::quicksort(std::vector<int>::iterator begin,
                        std::vector<int>::iterator end)
{
    if (end - begin <= 1)
    {
        return;
    }

    if (end - begin <= MAX_TOP_BORDER_ELEMENT)
    {
        insertionSort(begin, end);
        return;
    }

    auto median = partition(begin, end);

    quicksort(begin, median - 1);
    quicksort(median, end);
}
