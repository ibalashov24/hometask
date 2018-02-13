#include <iostream>

using namespace std;

int main()
{
    int arraySize = 0;
    cout << "Enter the size of the array: ";
    cin >> arraySize;

    cout << "Enter an array: " << endl;

    int zeroCount = 0;
    for (int i = 0; i < arraySize; i++)
    {
        int currentElement = 0;
        cin >> currentElement;

        if (currentElement == 0)
            zeroCount++;
    }

    cout << "The count of zeroes is: " << zeroCount << endl;

    return 0;
}
