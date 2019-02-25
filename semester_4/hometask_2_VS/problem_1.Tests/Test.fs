namespace Problem1

module Tests =
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``Smoke test`` () =
        let list = [1; 2; 3; -1; 23423; -4234; 0; 3]
        let sample = -1

        sample |> Main.findEntry list |> should equal 3

    [<Test>]
    let ``Duplicate item should be found normally`` () =
        let list = [1; 2; 3; 23423; -4234; -1; -1; 0; 3; 1; -1]
        let sample = -1

        sample |> Main.findEntry list |> should equal 5

    [<Test>]
    let ``Non-existing element should not be found`` () =
        let list = [1; 2; 3; 23423; -4234; -1; -1]
        let sample = 42

        sample |> Main.findEntry list |> should equal -1

    [<Test>]
    let ``Function should work correctly on empty list`` () =
        let list = []
        let sample = 42

        sample |> Main.findEntry list |> should equal -1
