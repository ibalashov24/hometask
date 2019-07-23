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
            let allAvailable = alreadyInUse |> Set.difference availableVariableName
            (allAvailable |> Set.toList).Head

        /// Performs term substitution instead of variable
        let rec performReplacement oldVariable newTerm expression = 
            match expression with
                | Variable x when x = oldVariable -> newTerm
                | Variable x -> Variable (x) 
                | Application (termLeft, termRight) ->
                    let replacementFunction = performReplacement oldVariable newTerm 
                    let leftTransformed = termLeft |> replacementFunction
                    let rightTransformed = termRight |> replacementFunction

                    Application(leftTransformed, rightTransformed)
                | LambdaAbstraction (variable, term) when variable = oldVariable -> expression
                | LambdaAbstraction (variable, term) ->    
                    let freeVariablesInTerm = getFreeVariables term
                    let freeVariablesInSubstitution = getFreeVariables newTerm

                    if ((freeVariablesInSubstitution |> Set.contains variable) && 
                        (freeVariablesInTerm |> Set.contains oldVariable)) then
                            let freeName = getNewVariable 
                                            (freeVariablesInTerm |> Set.union freeVariablesInSubstitution)
                            let alphaTransformed = term |> performReplacement variable (Variable(freeName))
                            let transformedTerm = alphaTransformed |> performReplacement oldVariable newTerm
                            LambdaAbstraction (freeName, transformedTerm)
                    else
                            LambdaAbstraction (variable, term |> performReplacement oldVariable newTerm)

        /// Beta reduces expression
        let rec betaReductionRec expression = 
            match expression with
                | Variable x -> Variable(x)
                | LambdaAbstraction (variable, term) -> 
                    LambdaAbstraction(variable, term |> betaReductionRec)
                | Application (LambdaAbstraction (variable, term), termRight) ->
                    let transformed = term |> performReplacement variable termRight
                    transformed |> betaReductionRec
                | Application (termLeft, termRight) ->
                            let leftReduced = termLeft |> betaReductionRec
                            match leftReduced with
                                | LambdaAbstraction(_) -> Application(leftReduced, termRight) |> betaReductionRec
                                | _ -> Application(leftReduced, termRight |> betaReductionRec)

        betaReductionRec expression

    performBetaReduction (Application(LambdaAbstraction('x', Variable('x')), Variable('y'))) |> printfn "%A" 