namespace Problem1

/// Simulates local network infection proccess
type Simulator(
                computers   : IOperationSystem array, 
                graph       : int list array, 
                logger      : ILogger) = 
    let random = System.Random()

    /// Launches simulation
    member this.Start () =
        /// Carries out the next step
        let rec nextStep (handleQueue : int list) (used : bool array)=
            match handleQueue with
            | head :: tail when 
                        not used.[head] && 
                        (random.NextDouble() < computers.[head].GetInfectionProbability) ->
                used.[head] <- true
                used |> logger.LogState |> ignore

                nextStep (handleQueue @ graph.[head]) used
            | head :: tail when not used.[head] -> 
                used |> logger.LogState |> ignore
                nextStep (handleQueue @ [head]) used
            | head :: tail -> 
                nextStep tail used
            | [] -> used

        /// Running infection proccess in each connectivity component
        let rec makeStepFromEachComponent i (state : bool array) =
            if i = computers.Length then
                true
            else
                match state.[i] with
                | true -> makeStepFromEachComponent (i + 1) state
                | false -> 
                    let newState = nextStep [i] state
                    makeStepFromEachComponent (i + 1) newState
                
        let startState = [| for i in 1 .. computers.Length -> false |]
        makeStepFromEachComponent 0 startState

