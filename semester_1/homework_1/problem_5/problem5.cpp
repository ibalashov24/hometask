#include <iostream>

using namespace std;

int main()
{
    int stringLength = 0;
    cout << "Enter the length of the bracket sequence: ";
    cin >> stringLength;

    char *bracketString = new char[stringLength + 1];
    cout << "Enter the sequence : ";
    cin >> bracketString;

    int openedBracketCount = 0;
    bool isValidString = true;
    for (int i = 0; bracketString[i]; i++)
        if (bracketString[i] == '(')
        {
            openedBracketCount++;
        }
        else if (openedBracketCount <= 0)
        {
            isValidString = false;
            break;
        }
        else
        {
            openedBracketCount--;
        }

    if (openedBracketCount != 0)
    {
        isValidString = false;
    }

    if (isValidString)
    {
        cout << "Valid string" << endl;
    }
    else
    {
        cout << "Invalid string" << endl;
    }

    delete[] bracketString;

    return 0;
}
