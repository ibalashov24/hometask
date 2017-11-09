// This program implements a sorted list

#include <iostream>

#include "list.h"

using namespace std;

void actionInsertValue(listStuff::SortedList &list)
{
    int value = 0;
    cout << "Enter value to insert: ";
    cin >> value;

    listStuff::insert(list, value);
}

void actionDeleteValue(listStuff::SortedList &list)
{
    int value = 0;
    cout << "Enter value to delete: ";
    cin >> value;

    listStuff::delete(list, value);
}

void actionPrintList(const listStuff::SortedList &list)
{
    cout << "Full list:" << endl;
    listStuff::printList(list);
}

void printHelp()
{
    cout << "0 - exit" << endl;
    cout << "1 - insert value" << endl;
    cout << "2 - delete value" << endl;
    cout << "3 - print full list" << endl;
    cout << endl;
}

int main()
{
    listStuff::SortedList list;

    int action = 0;
    do
    {
        cout << "Enter command (enter x > 3 || x < 0 to print help)" << endl;

        cin >> action;
        switch (action)
        {
        case 0:
            {
                return 0;
            }
        case 1:
            {
                actionInsertValue(list);
                break;
            }
        case 2:
            {
                actionDeleteValue(list);
                break;
            }
        case 3:
            {
                actionPrintList(list);
                break;
            }
        default:
            {
                printHelp();
                break;
            }
        }
    } while (true);

    cout << "Good Bye!" << endl;

    return 0;
}
