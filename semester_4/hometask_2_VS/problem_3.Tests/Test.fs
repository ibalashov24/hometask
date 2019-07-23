namespace Problem3

module Tests = 
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``MergeSort should sort simple lists correctly`` () =
        let input = [5; 3; 4; 1; 2]
        let expected = [1; 2; 3; 4; 5]

        input |> Main.mergeSort |> should equal expected

    [<Test>]
    let ``MeregeSort should sort lists with duplicate elements correctly`` () =
        let input = [2; 3; 2; 2; 1; 1; 0; 1]
        let expected = [0; 1; 1; 1; 2; 2; 2; 3]

        input |> Main.mergeSort |> should equal expected

    [<Test>]
    let ``MergeSort should handle list with one element correctly`` () =
        let input = [-100]
        let expected = [-100]

        input |> Main.mergeSort |> should equal expected

    [<Test>]
    let ``MergeSort should handle empty lists correctly`` () =
        [] |> Main.mergeSort |> should equal []

    [<Test>]
    let ``MergeSort should sort big lists correctly`` () =
        let list = [for i in [1..1000] -> (i * 124232 - 15) % 14324 + 42]
        let resultList = Main.mergeSort list

        resultList |> should be ascending
