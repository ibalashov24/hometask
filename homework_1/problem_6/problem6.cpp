#include <iostream>

using namespace std;


/**
    Knuth-Morris-Pratt algorithm
**/
void calcPrefix(char *s, int lengthS, char *s1,
                    int lengthS1, int *prefixOccurenceLength)
{
    char *concatedString = new char[lengthS + lengthS1 + 1];
    for (int i = 0; i < lengthS1; i++)
    {
        concatedString[i] = s1[i];
    }
    concatedString[lengthS1] = '#';
    for (int i = 0; i < lengthS; i++)
    {
        concatedString[i + lengthS1 + 1] = s[i];
    }

    prefixOccurenceLength[0] = 0;
    for (int i = 1; i <= lengthS + lengthS1; i++)
    {
        int lastPrefix = prefixOccurenceLength[i - 1];
        while (lastPrefix > 0 && concatedString[i] != concatedString[lastPrefix])
        {
            lastPrefix = prefixOccurenceLength[lastPrefix - 1];
        }

        if (concatedString[i] == concatedString[lastPrefix])
        {
            lastPrefix++;
        }
        prefixOccurenceLength[i] = lastPrefix;
    }

    delete [] concatedString;
}


/**
    Counts occurrences of s1 into s
**/
int countOccurrences(char *s, int lengthS, char *s1, int lengthS1)
{
    int *prefixOccurenceLength = new int[lengthS1 + lengthS + 1];
    calcPrefix(s, lengthS, s1, lengthS1, prefixOccurenceLength);

    int occurrenceCount = 0;
    for (int i = lengthS1; i < lengthS + lengthS1 + 1; i++)
        if (prefixOccurenceLength[i] == lengthS1)
        {
            occurrenceCount++;
        }

    delete [] prefixOccurenceLength;

    return occurrenceCount;
}

int main()
{
    int lengthS = 0;
    int lengthS1 = 0;
    cout << "Введите длины строк S и S1: " << endl;
    cin >> lengthS >> lengthS1;

    char *s = new char[lengthS + 1];
    char *s1 = new char[lengthS1 + 1];
    cout << "Введите S и S1: " << endl;
    cout << "S: ";
    cin >> s;
    cout << "S1: ";
    cin >> s1;

    cout << "The answer is: ";
    cout << countOccurrences(s, lengthS, s1, lengthS1) << endl;

    delete [] s;
    delete [] s1;

    return 0;
}
