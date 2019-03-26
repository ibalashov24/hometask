namespace Problem3

module Main =
    /// Type of variable name in lambda expressions
    type VariableNameType = char

    /// Represents lambda term (by the definition)
    type LambdaTerm =
        | Variable of VariableNameType
        | Application of LambdaTerm * LambdaTerm
        | LambdaAbstraction of VariableNameType * LambdaTerm 

    /// Beta reduces lamda expressions
    let performBetaReduction (expression : LambdaTerm) =
        let availableVariableName = Set.ofSeq['a'..'z']

        /// Returns the set of all free variables in given lambda expression
        let rec getFreeVariables expression =
            match expression with
                | Variable x -> Set.empty.Add(x)
                | Application (leftTerm, rightTerm) ->
                    (getFreeVariables leftTerm) |> Set.union (getFreeVariables rightTerm)
                | LambdaAbstraction (variable, term) ->
                    getFreeVariables term |> Set.filter (fun x -> x <> variable)  

        /// Returns unused name for the free variable
        let getNewVariable alreadyInUse = 
            let allAvailable = availableVariableName |> Set.difference alreadyInUse
            (allAvailable |> Set.toList).Head

        /// Performs term substitution instead of variable
        let rec performReplacement oldVariable newTerm expression = 
            match expression with
                | Variable x -> 
                    if (x = oldVariable) then
                        newTerm
                    else
                        Variable (x)
                | Application (termLeft, termRight) ->
                    let replacementFunction = performReplacement oldVariable newTerm 
                    let leftTransformed = termLeft |> replacementFunction
                    let rightTransformed = termRight |> replacementFunction

                    Application(leftTransformed, rightTransformed)
                | LambdaAbstraction (variable, term) ->
                    let freeVariablesInTerm = getFreeVariables term
                    let freeVariablesInExpression = getFreeVariables expression
                    if ((freeVariablesInTerm |> Set.contains variable) && 
                        (freeVariablesInExpression |> Set.contains oldVariable)) then
                            let freeName = getNewVariable 
                                            (freeVariablesInTerm |> Set.union freeVariablesInExpression)
                            let alphaTransformed = newTerm |> performReplacement variable (Variable(freeName))
                            let transformedTerm = term |> performReplacement oldVariable alphaTransformed
                            LambdaAbstraction (variable, transformedTerm)
                    else
                            LambdaAbstraction (variable, term)

        /// Beta reduces expression
        let rec betaReductionRec expression = 
            match expression with
                | Variable x -> Variable(x)
                | LambdaAbstraction (variable, term) -> 
                    term |> betaReductionRec
                | Application (termLeft, termRight) ->
                    match termLeft with
                        | LambdaAbstraction (variable, term) -> 
                            performReplacement variable termRight term
                        | _ -> 
                            let leftReduced = termLeft |> betaReductionRec
                            let rightReduced = termRight |> betaReductionRec
                            Application(leftReduced, rightReduced)

        betaReductionRec expression

    performBetaReduction (Application(LambdaAbstraction('x', Variable('x')), Variable('y'))) |> printfn "%A" 