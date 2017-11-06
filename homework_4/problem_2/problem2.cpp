#include <iostream>
#include <fstream>
#include <string>
#include <vector>

#include "sort.h"

using namespace std;

int findMostFrequent(vector<int>::iterator begin,
                     vector<int>::iterator end)
{
    int mostFrequent = *begin;
    int maxFrequency = 0;
    int currentFrequency = 1;
    for (auto pos = begin + 1; pos < end; ++pos)
    {
        if (*pos != *(pos - 1))
        {
            if (currentFrequency > maxFrequency)
            {
                mostFrequent = *(pos - 1);
                maxFrequency = currentFrequency;
            }

            currentFrequency = 0;
        }

        currentFrequency++;
    }

    if (currentFrequency > maxFrequency)
    {
        mostFrequent = *(end - 1);
    }

    return mostFrequent;
}

int main()
{
    string inputFileName = "";
    cout << "Enter input file name: ";
    cin >> inputFileName;

    ifstream inputFile(inputFileName);

    vector<int> inputVector;
    int current = 0;
    while (inputFile >> current)
    {
        inputVector.push_back(current);
    }

    inputFile.close();

    sorting::quicksort(inputVector.begin(), inputVector.end());

    cout << "The most frequent element is: ";
    cout << findMostFrequent(inputVector.begin(), inputVector.end()) << endl;

    return 0;
}
