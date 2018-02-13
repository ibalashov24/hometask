#include <iostream>

#include "cycleList.h"

using namespace std;

int main()
{
    cout << "Enter n and m: ";

    int n = 0;
    int m = 0;
    cin >> n >> m;

    auto list = listStuff::makeCyclicList();
    for (int i = 1; i <= n; ++i)
    {
        listStuff::insertToCyclicList(list, i);
    }

    int fromLastCount = 1;
    // Last soldier (last + 1 == first)
    auto prevSoldier = listStuff::getListTop(list);

    while (getListSize(list) > 1)
    {
        if (fromLastCount == m)
        {
            fromLastCount = 1;
            listStuff::deleteFromCyclicList(list, prevSoldier);
        }
        else
        {
            fromLastCount++;
            prevSoldier = listStuff::getNextVertex(prevSoldier);
        }
    }

    cout << "The last position is: ";
    cout << listStuff::getVertexValue(listStuff::getListTop(list)) << endl;

    listStuff::clearCyclicList(list);

    return 0;
}
