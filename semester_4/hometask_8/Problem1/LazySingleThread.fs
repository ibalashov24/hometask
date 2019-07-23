module LazySync

/// Represents lazy calculator with single thread work guarantee
type LazySingleThread<'a>(supplier : unit -> 'a) =
    let mutable result = None

    interface LazyInterface.ILazy<'a> with
        /// Launches the calculation and returns the result
        member this.Get () =
            match result with
            | None -> 
                let value = supplier ()
                result <- Some value
                value
            | Some value ->
                value
