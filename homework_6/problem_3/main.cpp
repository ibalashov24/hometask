#include <iostream>
#include <string>
#include <cctype>

#include "stack.h"

using namespace std;

const int MAX_TOKEN_COUNT = 100;

bool isOperation(const char token)
{
    return (token == '*' || token == '/' || token == '+' || token == '-');
}

/**
 * Does `left` has higher or equal priority than `right`?
 */
bool hasNotLowerPriority(char left, char right)
{
	const int leftPriority = (left == '*' || left == '/') ? 1 : 0;
	const int rightPriority = (right == '*' || right == '/') ? 1 : 0;

	return leftPriority >= rightPriority;
}

void putIntoResult(string &resultString, char token)
{
    resultString.push_back(token);
    resultString.push_back(' ');
}

/**
 * Converts expression in prefix form to postfix form
 * (Dijkstra's sorting station)
 */
string convertToPostfix(istream &inputStream)
{
    string result = "";
    auto tokenStack = stackStuff::createStack(MAX_TOKEN_COUNT);

    char token = '\0';
    while ((token = inputStream.get()) != '\n')
    {
        if (isdigit(token))
        {
            putIntoResult(result, token);
        }
        else if (isOperation(token))
        {
            while (!stackStuff::isEmpty(tokenStack) &&
            		isOperation(stackStuff::top(tokenStack)) &&
            		hasNotLowerPriority(stackStuff::top(tokenStack), token))
            {
                putIntoResult(result, stackStuff::pop(tokenStack));
            }
            stackStuff::push(tokenStack, token);
        }
        else if (token == '(')
        {
            stackStuff::push(tokenStack, token);
        }
        else if (token == ')')
        {
            char currentToken = '\0';
            while ((currentToken = stackStuff::pop(tokenStack)) != '(')
            {
                putIntoResult(result, currentToken);
            }
        }
    }

    while (!stackStuff::isEmpty(tokenStack))
    {
        putIntoResult(result, stackStuff::pop(tokenStack));
    }

    stackStuff::deleteStack(tokenStack);

    return result;
}

int main()
{
    cout << "Enter expression in infix form: ";

    auto postfixExpression = convertToPostfix(cin);

    cout << "Postfix form: " << postfixExpression << endl;

    return 0;
}
