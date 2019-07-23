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

        let logger = new LoggerOneProbability()
        let simulator = new Simulator(computers, graph, logger)
        simulator.Start()

        logger.IsSomeoneHealthy() |> should be False

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
        let expectedState = [| for i in 1..6 -> false |]

        let simulator = new Simulator(computers, graph, new LoggerZeroProbability(6))
        try
            simulator.Start() |> should be True
        with
            | Failure("Test success!") -> Assert.Pass()
            | Failure("Test failure!") -> 
                Assert.Fail("Probabiliy == 0, but some computers were infected")
                
        Assert.Fail("The simulation stopped, although it shouldn't have to!")