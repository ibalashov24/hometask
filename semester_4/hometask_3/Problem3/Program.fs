namespace Problem3

module Main =
    /// Types of operation which could be handled by the calculator
    type OperationType = Multiplication | Addition | Substraction | Division

    /// Represents simple parse tree
    type ParseTree =
        | Operation of ParseTree * ParseTree * OperationType
        | Tip of double

    /// Applies arithmetic operation to 2 real operands
    let applyOperation (operation : OperationType) operandLeft operandRight =
        match operation with
            | Multiplication -> operandLeft * operandRight
            | Addition -> operandLeft + operandRight
            | Substraction -> operandLeft - operandRight
            | Division -> operandLeft / operandRight

    /// Calculates the value of the parse tree
    let calculateTree (tree : ParseTree) = 
        let rec calculateTreeRecursive tree cont =
            match tree with
                | Tip value -> cont(value)
                | Operation (leftOperand, rightOperand, operation) -> 
                    calculateTreeRecursive 
                        leftOperand
                        (fun accLeft -> calculateTreeRecursive 
                                            rightOperand
                                            (fun accRight -> cont(applyOperation operation accLeft accRight)))

        calculateTreeRecursive tree id

    let tree = Operation(Tip(5.0), Operation(Tip(7.0), Tip(2.0), Division), Multiplication)
    calculateTree tree |> printfn "%f"