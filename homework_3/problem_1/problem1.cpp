#include <iostream>
#include <vector>
#include <utility>

using namespace std;

void insertionSort(vector<int>::iterator begin,
                   vector<int>::iterator end)
{
    for (auto pos_i = begin + 1; pos_i != end; ++pos_i)
    {
        for (auto pos_j = pos_i; pos_j >= begin + 1; --pos_j)
        {
            if (*pos_j < *(pos_j - 1))
            {
                iter_swap(pos_j, pos_j - 1);
            }
        }
    }
}

vector<int>::iterator partition(vector<int>::iterator begin,
                                vector<int>::iterator end)
{
    int median = *(begin + (end - begin) / 2);

    auto insertionPos = begin;
    for (auto pos = begin; pos != end; ++pos)
    {
        if (*pos <= median)
        {
            iter_swap(pos, insertionPos);
            ++insertionPos;
        }
    }

    return insertionPos;
}

void quicksort(vector<int>::iterator begin,
               vector<int>::iterator end)
{
    if (end - begin <= 1)
    {
        return;
    }

    if (end - begin <= 10)
    {
        insertionSort(begin, end);
        return;
    }

    auto median = partition(begin, end);

    quicksort(begin, median - 1);
    quicksort(median, end);
}

int main()
{
    int size = 0;
    cout << "Enter size of array: ";
    cin >> size;

    vector<int> elements(size);
    cout << "Enter array: ";
    for (int &e : elements)
    {
        cin >> e;
    }

    quicksort(elements.begin(), elements.end());

    cout << "Result: ";
    for (int e : elements)
    {
        cout << e << ' ';
    }

    return 0;
}
