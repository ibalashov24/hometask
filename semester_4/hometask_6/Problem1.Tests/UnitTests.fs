namespace Problem1

module Tests =
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``Simulator should work correctly with overall probability = 1`` () =
        let computers = [| for i in 1..6 -> (new OsSieve() :> IOperationSystem) |]
        let graph = 
            [|
                [1]
                [0]
                [3]
                [2; 4; 5]
                [3; 5]
                [3; 4]
            |]

        let simulator = new Simulator(computers, graph, new LoggerOneProbability())
        simulator.Start() |> ignore

    [<Test>]
    let ``Simulator should work correctly with overall probability = 0`` () =
        let computers = [| for i in 1..6 -> (new OsInvincible() :> IOperationSystem) |]
        let graph = 
            [|
                [1]
                [0]
                [3]
                [2; 4; 5]
                [3; 5]
                [3; 4]
            |]

        let simulator = new Simulator(computers, graph, new LoggerZeroProbability())
        try
            simulator.Start() |> ignore
        with
            | Failure("Test success!") -> Assert.Pass()

        Assert.Fail("The simulation stopped, although it shouldn't have to!")