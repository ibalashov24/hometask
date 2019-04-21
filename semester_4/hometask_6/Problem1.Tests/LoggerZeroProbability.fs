namespace Problem1

type LoggerZeroProbability() =
    let mutable stepNumber = 0

    interface ILogger with
        member this.LogState state =
            if stepNumber >= 500 then
                failwith "Test success!"
            else
                stepNumber <- stepNumber + 1
