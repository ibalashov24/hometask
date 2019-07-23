namespace Problem2

module Main =
    /// Converts string to number (or returns None if not possible)
    let convertToNumber (str : string) =
        match System.Double.TryParse(str) with
            | true, value -> Some value
            | false, _ -> None 

    /// Calculates expression with strings as numbers
    type CalculationWorkflow() =
        member this.Bind(x : string, f) =
            let converted = x |> convertToNumber 
            match converted with
                | Some value -> value |> f
                | None -> None
        member this.Return(x) =
            Some x

    let calc = new CalculationWorkflow()
    let result = calc {
        let! x = "1"
        let! y = "Ъ"
        let z = x + y
        return z
    }