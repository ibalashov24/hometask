namespace Problem1

module Main =
    /// Represent available pairs of brackets
    let brackets () = Map.ofList [ (')', '('); ('}', '{'); ('>', '<') ]

    /// Checks if bracket is closing one
    let isClosingBracket bracket =
        brackets () |> Map.containsKey bracket

    // Checks bracket sequence for correctness
    let isBracketsCorrect (seq : string) =
        let rec checker seq openBrackets =  
            match seq with
                | [] when (openBrackets |> List.isEmpty) -> true
                | bracket :: tail when not (bracket |> isClosingBracket) -> 
                    checker tail (bracket :: openBrackets)
                | _ when (openBrackets |> List.isEmpty) -> false
                | bracket :: tail when (brackets ()).[bracket] = (openBrackets |> List.head) ->
                    checker tail (openBrackets |> List.tail)
                | _ -> false

        checker (seq |> Seq.toList) List.empty

    "(<>))" |> isBracketsCorrect |> printfn "%b"
    