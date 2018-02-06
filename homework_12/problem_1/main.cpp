#include <iostream>
#include <string>
#include <fstream>
#include <list>
#include <algorithm>
#include <vector>
#include <array>
#include <limits>

#include "search.h"

using namespace std;

using StopSymbolTable = array<int, numeric_limits<char>::max() + 1>;
using SuffixTable = vector<int>;
using PrefixTable = vector<int>;

// Returns first (from the end) position of string where example do not equal
int findDiscrepancy(const string &searchString,
					const std::string &example,
					int beginPosition)
{
    int examplePos = example.size() - 1;
    int searchStringPos = beginPosition + example.size() - 1;

    while (examplePos >= 0 &&
           searchStringPos >= beginPosition &&
           example[examplePos] == searchString[searchStringPos])
    {
        --examplePos;
        --searchStringPos;
    }

    return examplePos;
}

// Stop-symbol heuristic
StopSymbolTable calcStopSymbols(const string &example)
{
    StopSymbolTable stopSymbol;
    fill(stopSymbol.begin(), stopSymbol.end(), -1);

    for (size_t i = 0; i < example.size() - 1; ++i)
    {
    	stopSymbol[example[i]] = i;
    }

    return stopSymbol;
}

PrefixTable reversedPrefixFunciton(string example,
								   bool needReverse = false)
{
	PrefixTable result(example.size(), 0);

	if (needReverse)
	{
		reverse(example.begin(), example.end());
	}

	for (int i = example.size() - 2; i >= 0; --i)
	{
		unsigned int j = example.size() - result[i + 1];
		while (j != example.size() &&
			   example[j - 1] != example[i])
		{
			j = example.size() - result[j];
		}

		result[i] = example.size() - j;
		if (example[j - 1] == example[i])
		{
			result[i]++;
		}
	}

	if (needReverse)
	{
		reverse(result.begin(), result.end());
	}

	return result;
}

// Suffix heuristic (suffixTable[i] == minimal shift for suffix with length i)
SuffixTable calcSuffixes(const string &example)
{
	auto reversePrefixTable = reversedPrefixFunciton(example);
	auto prefixTable = reversedPrefixFunciton(example, true);

	SuffixTable result(example.size() + 1, example.size());
	for (size_t i = 0; i < reversePrefixTable.size(); ++i)
	{
		if (i == 0 || reversePrefixTable[i] == 0 ||
			reversePrefixTable[i - 1] <= reversePrefixTable[i])
		{
			const auto suffixPosition = example.size() - reversePrefixTable[i];
			result[reversePrefixTable[i]] = suffixPosition - i;
		}
	}

	for (size_t i = 1; i < result.size(); ++i)
	{
		if (static_cast<size_t>(result[i]) == example.size())
		{
			result[i] = example.size() - prefixTable.back();
		}
	}

	return result;
}

// Returns position of the first entry in string or -1 if not found
int findInString(const string &searchString, const string &example)
{
	auto stopSymbols = calcStopSymbols(example);
	auto suffixShifts = calcSuffixes(example);

	size_t currentPosition = 0;
	while (currentPosition + example.size() <= searchString.size())
	{
		const int firstDiscrepancy = findDiscrepancy(searchString,
													 example,
													 currentPosition);
		const char discrepancySymbol = searchString[firstDiscrepancy + currentPosition];

		if (firstDiscrepancy < 0)
		{
			return currentPosition;
		}


		int stopSymbolMove = firstDiscrepancy - stopSymbols[discrepancySymbol];
		int suffixMove = suffixShifts[example.size() - firstDiscrepancy - 1];

		currentPosition += max(stopSymbolMove, suffixMove);
	}

	return -1;
}

// Returns position of the first entry in stream or -1 if not found
int findFirstEntry(ifstream &input, const string &example)
{
	// Position of the first character of current line in the text
	int positionOfFirst = 0;

	string currentLine;
	while (getline(input, currentLine))
	{
		const int firstEncounter = findInString(currentLine, example);

		if (firstEncounter != -1)
		{
			return firstEncounter + positionOfFirst;
		}

		positionOfFirst += currentLine.size();
	}

	return -1;
}

int main()
{
    cout << "Enter example: ";
    string example;
    cin >> example;

    ifstream inputFile("input.txt");
    if (!inputFile.is_open())
    {
    	cerr << "Unknown file!!!" << endl;
    	return 0;
    }

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
