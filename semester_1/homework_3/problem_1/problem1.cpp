#include <iostream>

#include "sort.h"

using namespace std;

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

    cout << "Result: ";
    for (int e : elements)
    {
        cout << e << ' ';
    }

    return 0;
}
