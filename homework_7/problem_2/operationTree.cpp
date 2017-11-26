#include "operationTree.h"

#include <iostream>

struct treeStuff::TreeVertex
{
    TreeVertex *leftSon;
    TreeVertex *rightSon;

    char operation = '\0';
    double cachedValue = 0;
};

struct treeStuff::Tree
{
    TreeVertex *top = nullptr;
};

treeStuff::Tree *treeStuff::createTree(TreeVertex *top)
{
    auto newTree = new Tree{top};
    return newTree;
}

double useOperation(char operation, double leftOperand, double rightOperand)
{
    switch (operation)
    {
    case '+':
        {
            return leftOperand + rightOperand;
        }
    case '-':
        {
            return leftOperand - rightOperand;
        }
    case '*':
        {
            return leftOperand * rightOperand;
        }
    case '/':
        {
            return leftOperand / rightOperand;
        }
    default:
        {
            return 0;
        }
    }
}

treeStuff::TreeVertex *treeStuff::constructVertex(TreeVertex *left,
                                                  TreeVertex *right,
                                                  char operation)
{
    auto newVertex = new TreeVertex{left, right, operation, 0};

    if (left != nullptr && right != nullptr)
    {
        newVertex->cachedValue = useOperation(operation,
                                              left->cachedValue,
                                              right->cachedValue);
    }

    return newVertex;
}

treeStuff::TreeVertex *treeStuff::constructVertex(double value)
{
    auto newVertex = new TreeVertex{nullptr, nullptr, '\0', value};
    return newVertex;
}

void revalidateCache(treeStuff::TreeVertex *currentVertex)
{
    if (currentVertex->rightSon == nullptr || currentVertex->leftSon == nullptr)
    {
        return;
    }

    revalidateCache(currentVertex->rightSon);
    revalidateCache(currentVertex->leftSon);
}

double treeStuff::calculateExpression(const Tree *operationTree,
                                      bool needRevalidateCahce)
{
    if (needRevalidateCahce)
    {
        revalidateCache(operationTree->top);
    }

    return (operationTree->top == nullptr ? 0 :
                                            operationTree->top->cachedValue);
}

void printOperand(const treeStuff::TreeVertex *currentOperand)
{
    if (currentOperand->rightSon == nullptr ||
        currentOperand->leftSon == nullptr)
    {
        std::cout << currentOperand->cachedValue;
        return;
    }

    std::cout << '(' << currentOperand->operation << ' ';
    printOperand(currentOperand->leftSon);
    std::cout << ' ';
    printOperand(currentOperand->rightSon);
    std::cout << ')';
}

void treeStuff::printTree(const Tree *operationTree)
{
    if (operationTree->top == nullptr)
    {
        std::cout << "Tree is empty!!!" << std::endl;
        return;
    }
    printOperand(operationTree->top);
}

void deleteVertex(treeStuff::TreeVertex *vertex)
{
    if (vertex == nullptr)
    {
        return;
    }

    deleteVertex(vertex->leftSon);
    deleteVertex(vertex->rightSon);

    delete vertex;
}

void treeStuff::deleteTree(Tree *tree)
{
    deleteVertex(tree->top);
}
