#pragma once

namespace stackStuff {
	// Size of stack by default
    const int DEFAULT_SIZE = 20;

    struct Stack;

    // Pushes value to stack
    void push(Stack *inputStack, int value);
    
    // Extracts element from the stack
    int pop(Stack *inputStack);
    
    // Checks if stack is empty
    bool isEmpty(const Stack *inputStack);
    
    // Returns top element of the stack
    int top(const Stack *inputStack);

    // Constructs new stack and and initializes it with defaults
    Stack *createStack(int maxSize);
    
    // Fully deletes stack and it's content
    void deleteStack(Stack *toDeleteStack);
}
