module LazyLockFree

open System.Threading

/// Represents lock-free lazy calculator with multi thread work guarantee
type LazyLockFree<'a>(supplier : unit -> 'a) =
    let mutable result = None
    let emptyResult = result

    interface LazyInterface.ILazy<'a> with
        /// Launches the calculation and returns the result
        member this.Get () =
            match result with
            | None -> 
                let value = supplier ()
                Interlocked.CompareExchange(&result, Some value, emptyResult) |> ignore

                match result with
                | Some x -> x
                | None -> value
            | Some value ->
                value
                
