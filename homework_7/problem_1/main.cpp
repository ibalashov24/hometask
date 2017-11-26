#include <iostream>

#include "set.h"

using namespace std;

using command = int;
const command COMMAND_EXIT = 0;
const command COMMAND_ADD = 1;
const command COMMAND_FIND = 2;
const command COMMAND_DELETE = 3;
const command COMMAND_PRINT_ASC = 4;
const command COMMAND_PRINT_DESC = 5;

void printHelp()
{
    cout << "Enter" << endl;
    cout << COMMAND_EXIT << " to exit" << endl;
    cout << COMMAND_ADD << " to add element to set" << endl;
    cout << COMMAND_FIND << " to check if element in set" << endl;
    cout << COMMAND_DELETE << " to delete element from set" << endl;
    cout << COMMAND_PRINT_ASC << " to print set in ascending order" << endl;
    cout << COMMAND_PRINT_DESC << " to print set in descending order" << endl;
    cout << "or enter something else to print this help" << endl;
}

int main()
{
    auto set = setStuff::createSet();

    printHelp();

    command currentCommand = COMMAND_EXIT;
    cin >> currentCommand;
    while (currentCommand != COMMAND_EXIT)
    {
        switch (currentCommand)
        {
        case COMMAND_ADD:
            {
                cout << "Enter number to add: ";
                int newElement = 0;
                cin >> newElement;

                setStuff::push(set, newElement);

                break;
            }
        case COMMAND_FIND:
            {
                cout << "Enter value to find: ";
                int checkValue = 0;
                cin >> checkValue;

                cout << (setStuff::isInSet(set, checkValue) ? "In set" :
                         "Not in set");
                cout << endl;

                break;
            }
        case COMMAND_DELETE:
            {
                cout << "Enter value to delete: ";
                int deleteValue = 0;
                cin >> deleteValue;

                setStuff::deleteElement(set, deleteValue);

                break;
            }
        case COMMAND_PRINT_ASC:
            {
                setStuff::printSet(set, true);
                break;
            }
        case COMMAND_PRINT_DESC:
            {
                setStuff::printSet(set, false);
                break;
            }
        default:
            {
                printHelp();
            }
        }

        cin >> currentCommand;
    }

    return 0;
}
