#include "stack.h"

#include <iostream>

struct stackStuff::Stack
{
    int *storage = nullptr;

    int size = 0;
    int capacity = 0;
};

stackStuff::Stack *stackStuff::createStack(int maxSize = DEFAULT_SIZE)
{
    Stack *newStack = new Stack;
    newStack->capacity = maxSize;
    newStack->storage = new int[maxSize];

    return newStack;
}

void stackStuff::deleteStack(Stack *toDeleteStack)
{
    delete[] toDeleteStack->storage;
    delete toDeleteStack;
}

bool stackStuff::isEmpty(const Stack *inputStack)
{
    return (inputStack->size == 0);
}

int stackStuff::top(const Stack *inputStack)
{
    if (isEmpty(inputStack))
    {
        std::cerr << "The stack is empty!!!" << std::endl;
        return -1;
    }

    return inputStack->storage[inputStack->size - 1];
}

void stackStuff::push(Stack *inputStack, int value)
{
    if (inputStack->size == inputStack->capacity)
    {
        std::cerr << "The stack is full!!!" << std::endl;
        return;
    }

    inputStack->storage[inputStack->size] = value;
    inputStack->size++;
}

int stackStuff::pop(Stack *inputStack)
{
    if (isEmpty(inputStack))
    {
        std::cerr << "The stack is empty!!!" << std::endl;
        return -1;
    }

    return inputStack->storage[--inputStack->size];
}
