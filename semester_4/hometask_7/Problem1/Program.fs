namespace Problem1

module Main =
    open System

    /// Performs mathematical calculations with given accuracy
    type CalculationWorkflow(accuracy : int) =
        let calculationAccuracy = accuracy
        member this.Bind(x : double, f : double -> double) =
            f (Math.Round(x, accuracy))
        member this.Return(x : double) =
            Math.Round(x, accuracy)