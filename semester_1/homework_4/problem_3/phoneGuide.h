#include <string>
#include <vector>

namespace Guide
{
	// Phonebook entry
    struct Record
    {
        std::string name;
        std::string phoneNumber;
    };

    // Array of all phonebook entries
    struct PhoneGuide
    {
        std::vector<Record> array;
    };

    // Reads phonebook entries from input stream (cin)
    void initializeGuide(PhoneGuide &guide,
                         const std::string &fileName);
    // Dumps phonebook from memory to file
    void saveToFile(const PhoneGuide &guide,
                    const std::string &fileName);

    // Finds entry with given name and returns it's phone
    std::string findNameByPhone(const PhoneGuide &guide,
                          const std::string &phoneNumber);
    // Finds entry with given phone number and returns it's name
    std::string findPhoneByName(const PhoneGuide &guide,
                          const std::string &personName);

    // Adds entry to phonebook
    void addRecord(PhoneGuide &guide,
                   const std::string &personName,
                   const std::string &phoneNumber);

    // Dumps phonebook from memory to cout (stdout)
    void printAllToCout(const PhoneGuide &guide);
}

