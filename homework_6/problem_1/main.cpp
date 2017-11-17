#include <iostream>
#include <cctype>

#include "../stack.h"

using namespace std;

const int MAX_SIZE = 100;

void useOperation(stackStuff::Stack *stack, char operationCode)
{
    const int previousOne = stackStuff::pop(stack);
    const int previousTwo = stackStuff::pop(stack);

    switch (operationCode)
    {
    case '+':
        {
            stackStuff::push(stack, previousOne + previousTwo);
            break;
        }
    case '-':
        {
            stackStuff::push(stack, previousTwo - previousOne);
            break;
        }
    case '*':
        {
            stackStuff::push(stack, previousOne * previousTwo);
            break;
        }
    case '/':
        {
            if (previousOne == 0)
            {
                cerr << "Do not divide by 0, please!" << endl;
                return;
            }

            stackStuff::push(stack, previousTwo / previousOne);
            break;
        }
    }
}

int main()
{
    auto stack = stackStuff::createStack(MAX_SIZE);

    cout << "Enter expression in postix form: ";

    char currentSign = '\0';
    while (cin.get(currentSign) && currentSign != '\n')
    {
        if (isdigit(currentSign))
        {
            stackStuff::push(stack, currentSign - '0');
        }
        else if (!isspace(currentSign))
        {
            useOperation(stack, currentSign);
        }
    }

    if (!stackStuff::isEmpty(stack))
    {
        cout << "The result is: " << stackStuff::pop(stack) << endl;
    }

    stackStuff::deleteStack(stack);

    return 0;
}
