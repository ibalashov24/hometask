#include <iostream>
#include <fstream>
#include <string>

#include "hash.h"

using namespace std;

int main()
{
    ifstream inputFile("input.txt");
    if (!inputFile.is_open())
    {
        cerr << "Unknown file!!!" << endl;
        return 0;
    }

    auto set = hashStuff::createHashSet();  // 100 buckets by default

    string currentWord = "";
    while (inputFile >> currentWord)
    {
        hashStuff::value(set, currentWord)++;
    }

    inputFile.close();

    cout << "All words with their count: " << endl;
    auto currentPosition = hashStuff::getNext(set);
    auto currentKey = hashStuff::getKey(currentPosition);
    while (currentKey != "")
    {
        // Implemented in not effective way to practice
        // in making functions that return a reference
        cout << currentKey << ' ' << hashStuff::value(set, currentKey) << endl;

        currentPosition = hashStuff::getNext(set, currentPosition);
        currentKey = hashStuff::getKey(currentPosition);
    }

    cout << endl;
    cout << "Statistics: " << endl;

    auto stats = hashStuff::getStatistics(set);
    cout << "Max size of bucket: " << stats.setSize << endl;
    cout << "Average size of bucket: " << stats.avgBucketSize << endl;

    cout << "Coefficient: " << static_cast<double>(hashStuff::getSize(set)) /
                                    hashStuff::getCapacity(set) << endl;

    hashStuff::deleteHashSet(set);

    return 0;
}
