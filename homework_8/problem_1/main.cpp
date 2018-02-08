#include <iostream>
#include <string>
#include <fstream>

#include "tree.h"

using namespace std;

void printHelp()
{
    cout << "Enter 0 to exit" << endl;
    cout << "Enter 1 to add element (key, value) to list" << endl;
    cout << "Enter 2 to print value of element with given key" << endl;
    cout << "Enter 3 to check if element with given key is in set" << endl;
    cout << "Enter 4 to delete element with given key" << endl;
    cout << "Enter anything else to print this help" << endl;
    cout << endl;
}

int main()
{
    printHelp();

    auto tree = treeStuff::createSplayTree();

    int currentCommand = -1;
    while (currentCommand != 0)
    {
        cout << "Enter command: ";
        cin >> currentCommand;

        switch (currentCommand)
        {
        case 0:
            {
                cout << "Good bye!" << endl;
                break;
            }
        case 1:
            {
                string newKey = "";
                string newValue = "";
                cout << "Enter key and value: ";
                cin >> newKey >> newValue;

                treeStuff::insert(tree, newKey, newValue);

                break;
            }
        case 2:
            {
                string key = "";
                cout << "Enter key: ";
                cin >> key;

                cout << "The value is: " << treeStuff::getValue(tree, key) << endl;

                break;
            }
        case 3:
            {
                string key = "";
                cout << "Enter key: ";
                cin >> key;

                cout << (treeStuff::isInTree(tree, key) ? "In set" : "Not in set");
                cout << endl;

                break;
            }
        case 4:
            {
                string key = "";
                cout << "Enter key: ";
                cin >> key;

                treeStuff::deleteElement(tree, key);

                break;
            }
        default:
            {
                printHelp();
                break;
            }
        }

    }

    treeStuff::deleteSplayTree(tree);

    return 0;
}
