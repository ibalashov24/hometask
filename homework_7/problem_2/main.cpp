#include <iostream>
#include <fstream>

#include "operationTree.h"

using namespace std;

treeStuff::TreeVertex *parseInputTree(ifstream &input);

treeStuff::TreeVertex *parseOperand(ifstream &input)
{
    treeStuff::TreeVertex *operand = nullptr;
    if (input.get() == '(')
    {
        input.unget();
        operand = parseInputTree(input);
    }
    else
    {
        input.unget();

        int vertexValue = 0;
        input >> vertexValue;
        operand = treeStuff::constructVertex(vertexValue);
    }

    return operand;
}

treeStuff::TreeVertex *parseInputTree(ifstream &input)
{
    input.get();    // Reads '('
    char operation = input.get();
    input.get();    // Reads space

    auto leftVertex = parseOperand(input);
    input.get();    // Reads space
    auto rightVertex = parseOperand(input);
    input.get();    // Reads ')'

    return treeStuff::constructVertex(leftVertex, rightVertex, operation);
}


int main()
{
    ifstream inputFile("input.txt");
    if (!inputFile.is_open())
    {
    	cerr << "Unknown file!!!" << endl;
    	return 1;
    }

    auto topOfNewTree = parseInputTree(inputFile);
    inputFile.close();

    auto operationTree = treeStuff::createTree(topOfNewTree);

    cout << "Parsed tree:" << endl;
    treeStuff::printTree(operationTree);
    cout << endl;

    cout << "Calculation result: ";
    cout << treeStuff::calculateExpression(operationTree) << endl;

    treeStuff::deleteTree(operationTree);

    return 0;
}
