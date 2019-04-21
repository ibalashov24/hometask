namespace Problem1

type LoggerOneProbability() =
    let mutable stepNumber = 0
    let mutable isSomeoneHealthy = true
    member this.IsSomeoneHealthy() = isSomeoneHealthy

    interface ILogger with
        member this.LogState state =
            let rec prettyPrintList number list =
                match list with
                | [] -> printf "\n"
                | head :: tail ->
                    match head with
                    | true -> printfn "Computer #%d: Infected" number
                    | false -> printfn "Computer #%d: Uninfected" number

                    prettyPrintList (number + 1) tail

            printfn "Step %d: " stepNumber
            prettyPrintList 1 (state |> List.ofArray)

            if stepNumber >= 500 then
                failwith "Simulation was not stopped, but probability == 1!"
            else
                isSomeoneHealthy <- Array.exists ((=) false) state
                stepNumber <- stepNumber + 1

            stepNumber

