#include <iostream>
#include <string>

#include "phoneGuide.h"

using namespace std;

void main_loop()
{
    int currentCommand = 0;
    Guide::PhoneGuide guide;

    cout << "Enter database file name: ";
    std::string fileName = "";
    cin >> fileName;

    Guide::initializeGuide(guide, fileName);

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
    main_loop();

    cout << "Good bye!" << endl;

    return 0;
}
