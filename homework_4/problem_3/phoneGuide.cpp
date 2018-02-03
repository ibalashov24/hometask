#include <fstream>
#include <iostream>

#include "phoneGuide.h"


void Guide::initializeGuide(PhoneGuide &guide,
                            const std::string &fileName)
{
    std::ifstream inputFile(fileName);

    if (inputFile.is_open())
    {
        Record tempRecord;
        while (inputFile >> tempRecord.name)
        {
            inputFile >> tempRecord.phoneNumber;

            guide.array.push_back(tempRecord);
        }

        inputFile.close();
    }
}

void Guide::saveToFile(const PhoneGuide &guide,
                       const std::string &fileName)
{
    std::ofstream outputFile(fileName);

    for (auto e : guide.array)
    {
        outputFile << e.name << ' ' << e.phoneNumber << std::endl;
    }

    outputFile.close();
}

std::string Guide::findNameByPhone(const PhoneGuide &guide,
                            const std::string &phoneNumber)
{
    for (auto e : guide.array)
    {
        if (e.phoneNumber == phoneNumber)
        {
            return e.name;
        }
    }

    return "Not found!";
}

std::string Guide::findPhoneByName(const PhoneGuide &guide,
                            const std::string &personName)
{
    for (auto e : guide.array)
    {
        if (e.name == personName)
        {
            return e.phoneNumber;
        }
    }

    return "Not found!";
}

void Guide::addRecord(PhoneGuide &guide,
                      const std::string &personName,
                      const std::string &phoneNumber)
{
    Record tempRecord;
    tempRecord.name = personName;
    tempRecord.phoneNumber = phoneNumber;

    guide.array.push_back(tempRecord);
}

void Guide::printAllToCout(const PhoneGuide &guide)
{
    for (int i = 0; i < static_cast<int>(guide.array.size()); i++)
    {
        std::cout << "Record " << i + 1 << std::endl;
        std::cout << "Name: " << guide.array[i].name << std::endl;
        std::cout << "Phone number: " << guide.array[i].phoneNumber << std::endl;
        std::cout << "*****" << std::endl;
    }

    if (guide.array.empty())
    {
        std::cout << "empty" << std::endl;
    }
}
