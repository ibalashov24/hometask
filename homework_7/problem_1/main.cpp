#include <iostream>

#include "set.h"

using namespace std;

// Available commands (see printHelp() body)
enum command
{
	COMMAND_EXIT,
	COMMAND_ADD,
	COMMAND_FIND,
	COMMAND_DELETE,
	COMMAND_PRINT_ASC,
	COMMAND_PRINT_DESC
};

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

command getNextCommand()
{
	int nextCommand = 0;
	cin >> nextCommand;

	return static_cast<command>(nextCommand);
}

int main()
{
    auto set = setStuff::createSet();

    printHelp();

    command currentCommand = COMMAND_EXIT;
    currentCommand = getNextCommand();
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

        currentCommand = getNextCommand();
    }

    return 0;
}
