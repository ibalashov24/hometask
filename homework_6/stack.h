#pragma once

namespace stackStuff {
    const int DEFAULT_SIZE = 20;

    struct Stack;

    void push(Stack *inputStack, int value);
    int pop(Stack *inputStack);
    bool isEmpty(const Stack *inputStack);
    int top(const Stack *inputStack);

    Stack *createStack(int maxSize);
    void deleteStack(Stack *toDeleteStack);
}
