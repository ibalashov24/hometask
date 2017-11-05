#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <iterator>
#include <algorithm>

#include "sort.h"

using namespace std;

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

    Sorting::quicksort(inputVector.begin(), inputVector.end());

    cout << "Sorted array from " << inputFileName << ':' << endl;
    copy(inputVector.begin(), inputVector.end(), ostream_iterator<int>(cout, " "));

    return 0;
}
