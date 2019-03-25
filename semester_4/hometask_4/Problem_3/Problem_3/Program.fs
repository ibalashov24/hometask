namespace Problem3

module Main =
    type VariableNameType = char
    type LambdaTerm =
        | Variable of VariableNameType
        | Application of LambdaTerm * LambdaTerm
        | LambdaAbstraction of VariableNameType * LambdaTerm 

    let performBetaReduction (expression : LambdaTerm) =
        let availableVariableName = Set.ofSeq['a'..'z']

        let rec getFreeVariables expression =
            match expression with
                | Variable x -> Set.empty.Add(x)
                | Application (leftTerm, rightTerm) ->
                    (getFreeVariables leftTerm) |> Set.union (getFreeVariables rightTerm)
                | LambdaAbstraction (variable, term) ->
                    getFreeVariables term |> Set.filter (fun x -> x <> variable)  

        let getNewVariable alreadyInUse = 
            let allAvailable = availableVariableName |> Set.difference alreadyInUse
            (allAvailable |> Set.toList).Head

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

        let rec betaReductionRec closedVariables expression = 
            match expression with
                | Variable x -> Variable(x)
                | LambdaAbstraction (variable, term) -> 
                    term |> betaReductionRec closedVariables
                | Application (termLeft, termRight) ->
                    match termLeft with
                        | LambdaAbstraction (variable, term) -> 
                            performReplacement variable termRight term
                        | _ -> 
                            let leftReduced = termLeft |> betaReductionRec closedVariables
                            let rightReduced = termRight |> betaReductionRec closedVariables
                            Application(leftReduced, rightReduced)

        betaReductionRec Set.empty expression

    performBetaReduction (Application(LambdaAbstraction('x', Variable('x')), Variable('y'))) |> printfn "%A" 