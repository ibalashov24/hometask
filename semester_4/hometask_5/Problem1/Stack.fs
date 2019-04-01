namespace Problem1

module Stack = 
    /// Represents simple stack
    type Stack<'a> =
        | EmptyStack
        | StackNode of 'a * Stack<'a>

    /// Checks if stack is empty
    let isEmpty stack =
        match stack with
            | EmptyStack -> true
            | _ -> false

    /// Pushes given value to the given stack
    let push value stack = 
        StackNode(value, stack)

    /// Returns the top value of the given stack
    let top stack =
        match stack with
            | EmptyStack -> failwith "The stack is empty!"
            | StackNode(value, _) -> value

    /// Removes the top value of the given stack
    let pop stack =
        match stack with
            | EmptyStack -> EmptyStack
            | StackNode(value, next) -> next
