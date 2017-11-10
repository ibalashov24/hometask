#include <iostream>

#include "cycleList.h"

using namespace std;

int main()
{
    cout << "Enter n and m: ";
    int n, m;
    cin >> n >> m;

    listStuff::CyclicList list;
    for (int i = 1; i <= n; ++i)
    {
        listStuff::insertToCyclicList(list, i);
    }

    int fromLastCount = 1;
    auto currentSoldier = list.top->next;   // First soldier
    while (list.size > 1)
    {
        if (fromLastCount == m)
        {
            fromLastCount = 1;
            listStuff::deleteFromCyclicList(list, currentSoldier);
        }
        else
        {
            fromLastCount++;
        }

        currentSoldier = currentSoldier->next;
    }

    cout << "The position is: " << list.top->value << endl;

    listStuff::clearCyclicList(list);

    return 0;
}
