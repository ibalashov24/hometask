#include <string>
#include <vector>

namespace Guide
{

    struct Record
    {
        std::string name;
        std::string phoneNumber;
    };

    struct PhoneGuide
    {
        std::vector<Record> array;
    };

    void initializeGuide(PhoneGuide &guide,
                         const std::string &fileName);
    void saveToFile(const PhoneGuide &guide,
                    const std::string &fileName);

    std::string findNameByPhone(const PhoneGuide &guide,
                          const std::string &phoneNumber);
    std::string findPhoneByName(const PhoneGuide &guide,
                          const std::string &personName);

    void addRecord(PhoneGuide &guide,
                   const std::string &personName,
                   const std::string &phoneNumber);

    void printAllToCout(const PhoneGuide &guide);
}

