namespace Problem1

type LoggerOneProbability() =
    let mutable stepNumber = 0

    interface ILogger with
        member this.LogState state =
            if stepNumber >= 500 then
                failwith "Simulation was not stopped, but probability == 1!"
            else
                stepNumber <- stepNumber + 1

