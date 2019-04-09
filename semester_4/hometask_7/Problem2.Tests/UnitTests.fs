namespace Problem2

module Tests =
    open NUnit.Framework
    open FsUnit

    open Main

    [<Test>]
    let ``Calculator should handle calculatable expression correctly`` () =
        let calculator = new CalculationWorkflow()
        let result = calculator {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }

        result |> should equal (Some 3.0)

    [<Test>]
    let ``Calculator should handle expressions with non-number variables correctly`` () =
        let calculator = new CalculationWorkflow()
        let result = calculator {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        }

        result |> should equal None

    [<Test>]
    let ``Calculator should handle expression with real numbers correctly`` () =
        let calculator = new CalculationWorkflow()
        let result = calculator {
            let! a = "1.56"
            let! b = "2.4"
            let! c = "3.1"
            return (a * b) / c
        }

        match result with 
            | Some value -> value |> should (equalWithin 0.001) 1.207
            | None -> Assert.Fail()
