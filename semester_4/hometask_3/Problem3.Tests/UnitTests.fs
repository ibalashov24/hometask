namespace Problem3

module Tests =
    open NUnit.Framework
    open FsUnit

    let correctTestCases = 
        [
            // Smoke test
            Main.Operation(Main.Tip(5.0), Main.Tip(5.0), Main.OperationType.Addition), 10.0
            // Branched tree
            Main.Operation(
                Main.Operation(
                    Main.Tip(5.0), 
                    Main.Operation(Main.Tip(1.0), Main.Tip(-2.5), Main.OperationType.Substraction),
                    Main.OperationType.Multiplication),
                Main.Operation(
                        Main.Tip(5.0), 
                        Main.Operation(Main.Tip(1.0), Main.Tip(-2.5), Main.OperationType.Substraction),
                        Main.OperationType.Multiplication),
                Main.OperationType.Division), 1.0
            // Chain-type tree
            Main.Operation(
                Main.Operation(
                    Main.Operation(Main.Tip(1.3), Main.Tip(2.0), Main.OperationType.Multiplication),
                    Main.Tip(5.0), 
                    Main.OperationType.Division),
                Main.Tip(3.0),
                Main.OperationType.Division), 0.173
        ] |> List.map (fun (a, b) -> TestCaseData(a, b))
        
    [<Test>]
    [<TestCaseSource("correctTestCases")>]
    let ``Correct trees should be handled correctly`` test (expectedResult : double) =
        Main.calculateTree test |> should (equalWithin 0.01) expectedResult

    [<Test>]
    let ``Calculator should handle tree with one vertex correctly`` = 
        Main.Tip(5.0) |> Main.calculateTree |> should (equalWithin 0.01) 5.0