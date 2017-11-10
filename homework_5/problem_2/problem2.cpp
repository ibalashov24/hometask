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
    auto prevSoldier = list.top;   // Last soldier (last + 1 == first)s

    while (list.size > 1)
    {
        if (fromLastCount == m)
        {
            fromLastCount = 1;
            listStuff::deleteFromCyclicList(list, prevSoldier);
        }
        else
        {
            fromLastCount++;
            prevSoldier = prevSoldier->next;
        }
    }

    cout << "The last position is: " << list.top->value << endl;

    listStuff::clearCyclicList(list);

    return 0;
}
