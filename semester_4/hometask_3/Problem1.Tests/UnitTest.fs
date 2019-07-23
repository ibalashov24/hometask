namespace Problem1

module Tests = 
    open NUnit.Framework
    open FsUnit

    let testCases = [
        [], 0 // Empty list
        [1; 5; 15; 67; 193; -5; -153; -1], 0   // Odd only
        [2; 4; 6; 8; -12; 0; 12; 10; 42], 9   // Even only
        [3; 45; 32; 15; 0; -234; -257; 0; 12], 5 // Mixed case
                    ] |> List.map (fun (a, b) -> TestCaseData(a, b))

    [<Test>]
    [<TestCaseSource("testCases")>]
    let ``getEvenUsingMapAndFold() should work correctly`` test expected =
        Main.getEvenUsingMapAndFold test |> should equal expected

    [<Test>]
    [<TestCaseSource("testCases")>]
    let ``getEvenUsingFilter() should work correctly`` test expected =
        Main.getEvenUsingFilter test |> should equal expected

    [<Test>]
    [<TestCaseSource("testCases")>]
    let ``getEvenUsingFold() should work correctly`` test expected =
        Main.getEvenUsingFold test |> should equal expected

