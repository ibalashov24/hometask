#include <iostream>
#include <string>

#include "../stack.h"

using namespace std;

const int MAX_SIZE = 100;

bool isOpeningBracket(char c)
{
    return (c == '[' || c == '{' || c == '(' || c == '<');
}

char getOpeningBracket(char c)
{
    switch (c)
    {
    case ']':
    case '}':
    case '>':
        {
            // See ASCII for more details
            return (c - 2);
        }
    case ')':
        {
            return '(';
        }
    }
}

bool isGoodSequence(const string &example)
{
    auto stack = stackStuff::createStack(MAX_SIZE);

    for (auto c : example)
    {
        if (isOpeningBracket(c))
        {
            stackStuff::push(stack, c);
        }
        else
        {
            if (stackStuff::isEmpty(stack))
            {
                stackStuff::deleteStack(stack);
                return false;
            }

            const char opposite = getOpeningBracket(c);
            const char lastBracket = stackStuff::pop(stack);

            if (lastBracket != opposite)
            {
                stackStuff::deleteStack(stack);
                return false;
            }
        }
    }
    if (!stackStuff::isEmpty(stack))
    {
        stackStuff::deleteStack(stack);
        return false;
    }

    stackStuff::deleteStack(stack);

    return true;
}

int main()
{
    cout << "Enter bracket sequence (length <= 100): ";
    string example = "";
    cin >> example;

    if (isGoodSequence(example))
    {
        cout << "All is OK" << endl;
    }
    else
    {
        cout << "Bad sequence!" << endl;
    }

    return 0;
}
