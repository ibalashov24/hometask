#include <iostream>

#include "sort.h"

using namespace std;

/**
*   Finds the most frequent element in sorted range
**/
int findMostFrequent(vector<int>::iterator begin,
                     vector<int>::iterator end)
{
    int mostFrequent = *begin;
    int maxFrequency = 0;
    int currentFrequency = 1;
    for (auto pos = begin + 1; pos < end; ++pos)
    {
        if (*pos != *(pos - 1))
        {
            if (currentFrequency > maxFrequency)
            {
                mostFrequent = *(pos - 1);
                maxFrequency = currentFrequency;
            }

            currentFrequency = 0;
        }

        currentFrequency++;
    }

    if (currentFrequency > maxFrequency)
    {
        mostFrequent = *(end - 1);
    }

    return mostFrequent;
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

    sorting::quicksort(elements.begin(), elements.end());

    cout << "The most frequent elements is: ";
    cout << findMostFrequent(elements.begin(), elements.end()) << endl;

    return 0;
}
