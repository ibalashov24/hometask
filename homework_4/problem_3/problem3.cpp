#include <iostream>
#include <string>

#include "phoneGuide.h"

using namespace std;

void printHelp()
{
	cout << "Available commands: \n"
			"0 - exit \n"
			"1 - add new entry \n"
			"2 - print all entries \n"
			"3 - find phone by name \n"
			"4 - find name by phone \n"
			"5 - dump phonebook to file \n";
	cout << endl;
}

void mainLoop()
{
    int currentCommand = 0;
    Guide::PhoneGuide guide;

    cout << "Enter database file name: ";
    std::string fileName = "";
    cin >> fileName;

    Guide::initializeGuide(guide, fileName);

    printHelp();

    do
    {
        cout << "Enter command: ";
        cin >> currentCommand;

        switch (currentCommand)
        {
        case 0:
            {
                return;
            }
        case 1:
            {
                cout << "Enter name and phoneNumber: ";
                std::string name = "";
                std::string phoneNumber = "";
                cin >> name >> phoneNumber;

                Guide::addRecord(guide, name, phoneNumber);

                break;
            }
        case 2:
            {
                cout << "All elements: " << std::endl;
                Guide::printAllToCout(guide);

                break;
            }
        case 3:
            {
                string name = "";
                cout << "Enter name: ";
                cin >> name;

                cout << "Phone number: " << Guide::findPhoneByName(guide, name);
                cout << std::endl;

                break;
            }
        case 4:
            {
                string phoneNumber = "";
                cout << "Enter phone number: ";
                cin >> phoneNumber;

                cout << "Name: " << Guide::findNameByPhone(guide, phoneNumber);
                cout << std::endl;

                break;

            }
        case 5:
            {
                Guide::saveToFile(guide, fileName);
                break;
            }
        }
    } while (true);

}

int main()
{
    mainLoop();

    cout << "Good bye!" << endl;

    return 0;
}
