module LazyAsync

open System
open System.Threading

/// Represents lazy calculator with multi thread work guarantee
type LazyMultiThread<'a>(supplier : unit -> 'a) =
    let mutable result = None
    let lockObject = new Object()

    interface LazyInterface.ILazy<'a> with
        /// Launches the calculation and returns the result
        member this.Get () =
            match result with
            | None -> 
                lock lockObject (fun () ->
                        match result with
                        | Some x -> x
                        | None ->
                            let value = supplier ()
                            result <- Some value
                            value
                    )
            | Some value ->
                value
