namespace Problem1

module Tests = 
    open NUnit.Framework
    open FsUnit

    open Main

    [<Test>]
    let ``Calculator should handle smoke test correctly`` () =
        let calculator = new CalculationWorkflow(3)
        let result = calculator {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }

        result |> should (equalWithin 0.001) 0.048

    [<Test>]
    let ``Calculator should correctly handle calculations, which are within accuaracy`` () =
        let calculator = new CalculationWorkflow(3)
        let result = calculator {
            let! a = 1.2 + 1.2
            let! b = a * 2.0
            return a + b
        }

        result |> should (equalWithin 0.001) 7.2

    [<Test>]
    let ``Calculator should handle zero correctly`` () =
        let calculator = new CalculationWorkflow(5)
        let result = calculator {
            let! a = 5.0 - 5.0
            let! b = a * 15.0
            return a * b
        }

        result |> should (equalWithin 0.001) 0.0

    [<Test>]
    let ``Calculator should handle zero accuracy correctly`` () =
        let calculator = new CalculationWorkflow(0)
        let result = calculator {
            let! a = 1.2 + 1.2
            let! b = a * 2.0
            return a + b
        }

        result |> should equal 6