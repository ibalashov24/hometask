#include <iostream>

#include "sort.h"

using namespace std;

bool isIncludes(vector<int>::const_iterator begin,
                vector<int>::const_iterator end,
                int element)
{
    while (end - begin > 1)
    {
        auto median = begin + (end - begin) / 2;

        if (*median > element)
        {
            end = median;
        }
        else
        {
            begin = median;
        }
    }

    return (*begin == element || *end == element);
}

void printVector(const vector<int> &vectorToPrint, char delimiter = ' ')
{
    for (auto e : vectorToPrint)
    {
        cout << e << delimiter;
    }
    cout << endl;
}

void generateRandom(vector<int> &container)
{
    for (int &e : container)
    {
        e = rand();
    }
}

int main()
{
    srand(time(nullptr));

    int n = 0;
    int k = 0;
    cout << "Enter n and k: ";
    cin >> n >> k;

    vector<int> elements(n);
    generateRandom(elements);

    vector<int> checkList(k);
    generateRandom(checkList);

    cout << "Array of elements: ";
    printVector(elements);

    cout << "Checklist: ";
    printVector(checkList);

    sorting::quicksort(elements.begin(), elements.end());

    cout << "First array includes: ";
    bool isEmpty = true;
    for (int e : checkList)
    {
        if (isIncludes(elements.begin(), elements.end(), e))
        {
            cout << e << ' ';
            isEmpty = false;
        }
    }
    if (isEmpty)
    {
        cout << "nothing" << endl;
    }

    return 0;
}

