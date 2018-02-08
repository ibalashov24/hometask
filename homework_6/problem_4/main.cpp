#include <iostream>
#include <string>
#include <fstream>

#include "list.h"
#include "sort.h"

using namespace std;

int main()
{
    ifstream inputFile = ifstream("input.txt");

    auto data = listStuff::createList();

    string currentName = "";
    while (inputFile >> currentName)
    {
        string currentPhoneNumber = "";
        inputFile >> currentPhoneNumber;

        listStuff::insert(data, make_pair(currentName, currentPhoneNumber));
    }

    cout << "Enter 1 if you want to sort by name" << endl;
    cout << "Else enter 0" << endl;

    bool sortByName = false;
    cin >> sortByName;

    if (sortByName)
    {
        sortingStuff::mergeSort(data, [](auto a, auto b)
                                        {
                                           return (a.first < b.first);
                                        });
    }
    else
    {
        sortingStuff::mergeSort(data, [](auto a, auto b)
                                        {
                                           return (a.second < b.second);
                                        });
    }

    cout << "Result: " << endl;
    listStuff::printList(data);

    listStuff::cleanList(data);

    return 0;
}
