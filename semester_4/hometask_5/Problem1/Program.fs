namespace Problem1

module Main =
    open Stack

    // Checks bracket sequence for correctness
    let isBracketsCorrect (seq : string) =
        let rec checker seq openBrackets =
            match seq with
                | [] when (openBrackets |> Stack.isEmpty) -> true
                | '(' :: tail -> checker tail (openBrackets |> Stack.push '(')
                | '{' :: tail -> checker tail (openBrackets |> Stack.push '{')
                | '<' :: tail -> checker tail (openBrackets |> Stack.push '<')
                | _ when (openBrackets |> Stack.isEmpty) -> false
                | ')' :: tail when (openBrackets |> Stack.top) = '(' ->
                        checker tail (openBrackets |> Stack.pop) 
                | '}' :: tail when (openBrackets |> Stack.top) = '{' ->
                        checker tail (openBrackets |> Stack.pop) 
                | '>' :: tail when (openBrackets |> Stack.top) = '<' ->
                        checker tail (openBrackets |> Stack.pop) 
                | _ -> false

        checker (seq |> Seq.toList) Stack.EmptyStack

    "{}" |> isBracketsCorrect |> printfn "%b"
    