namespace Problem1

module Tests = 
    open System
    open NUnit.Framework
    open FsUnit

    let tests = 
        [
            String.Empty,   true
            "()",           true
            "{}",           true
            "<>",           true
            "({<>})",       true
            "(){}<>",       true
            "((<{<>}>{}))", true
            "({<{<{}>}>})", true
            "((}",          false
            "(<>))",        false
            ">>",           false
            "({<{<>>}})",   false
        ] |> List.map (fun (a, b) -> TestCaseData(a, b))


    [<Test>]
    [<TestCaseSource("tests")>]
    let ``Checker should handle tests correctly`` test expected =
        test |> Main.isBracketsCorrect |> should equal expected