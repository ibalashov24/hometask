#include <iostream>
#include <string>
#include <fstream>
#include <list>
#include <algorithm>
#include <vector>
#include <array>

#include "search.h"

using namespace std;

bool readSymbols(ifstream &input, list<char> buffer, int count)
{
    int readCount = 0;
    char temp = '\0';
    while (readCount < count && input >> temp)
    {
        buffer.push_back(temp);
        ++readCount;
    }

    return (readCount >= count);
}


// Returns first (from the end) position where example do not equal
int compare(const list<char> &buffer, const std::string &example)
{
    int examplePos = example.size() - 1;
    auto bufferPos = buffer.rbegin();

    while (examplePos >= 0 &&
           bufferPos != buffer.rend() &&
           examplePos[examplePos] == *bufferPos)
    {
        --examplePos;
        ++bufferPos;
    }

    return examplePos;
}

// Makes precalc for Boyer–Moore string search algorithm
pair<vector<int>, array<int, sizeof(char)> >
    make_precalc(const string &example)
{
    // Stop-symbol heuristic
    array<int, sizeof(char)> stopSymbol;
    fill(stopSymbol.begin(), stopSymbol.end(), example.size());
    for (int i = example.size() - 2; i >= 0; --i)
    {
        if (stopSymbol[example[i]] == example.size())
        {
            stopSymbol[example[i]] -= i - 1;
        }
    }

    // Suffix heuristic

    ...
}

int findFirstEntry(ifstream &input, const string &example)
{
    auto precalc = makePrecalc(example);

    // Buffer
    list<char> lastTextSymbols;

    // Last read symbol position in the text
    int currentTailPosition = 0;
    int newTailPosition = example.size() - 1;
    // Replenishes buffer
    while (readSymbols(input, lastTextSymbols, newTailPosition - currentTailPosition))
    {
        lastTextSymbols.erase(lastTextSymbols.begin(),
                              lastTextSymbols.begin() + newTailPosition - currentTailPosition));
        currentTailPosition = newTailPosition;

        int failPosition = compare(lastTextSymbols, example);

        if (failPosition < 0)
        {
            break;
        }

        newTailPosition += max(precalc.first[failPosition + 1],     // Suffix
                               precalc.second[lastTextSymbols.back()]); // Stop
    }

    return (newTailPosition != currentTailPosition ?
                                return -1 :
                                return currentTailPosition - example.size() + 1);
}

int main()
{
    cout << "Enter example: ";
    string example;
    cin >> example;

    ifstream inputFile("input.txt");
    int firstEntry = findFirstEntry(inputFile, example);
    inputFile.close();

    if (firstEntry < 0)
    {
        cout << "Not found!" << endl;
    }
    else
    {
        cout << "First entry is: " << firstEntry << endl;
    }

    return 0;
}
