namespace Problem2

module Tests = 
    open System
    open NUnit.Framework
    open FsUnit

    open Main

    [<Test>]
    let ``binaryTreeMap() should handle simple test correctly`` () =
        let mapping = fun value -> value + 1
        let tree = 
            Tree
                (5, Tree(-1, Tip, Tip), Tree(56, Tree(-3, Tip, Tree(6, Tip, Tip)),
                    Tree(0, Tree(5, Tip, Tip), Tip)))
        let expected =
            Tree
                (6, Tree(0, Tip, Tip), Tree(57, Tree(-2, Tip, Tree(7, Tip, Tip)),
                    Tree(1, Tree(6, Tip, Tip), Tip)))

        tree |> binaryTreeMap mapping |> should equal expected


    [<Test>]
    let ``binaryTreeMap() should handle empty tree correctly`` () =
        Tip |> binaryTreeMap id |> should equal Tip

    [<Test>]
    let ``binaryTreeMap() should handle chain correctly`` () =
        let mapping = fun value -> "a" + value
        let tree =
            Tree
                ("1", Tree("0", Tree("5", Tree("6", Tree("7", Tip, Tip), Tip), Tip), Tip), Tip)
        let expected = 
            Tree
                ("a1", Tree("a0", Tree("a5", Tree("a6", Tree("a7", Tip, Tip), Tip), Tip), Tip), Tip)

        tree |> binaryTreeMap mapping |> should equal expected

    [<Test>]
    let ``binaryTreeMap() should handle symmetric tree correctly`` () =
        let mapping = fun value -> value - 1
        let tree =
            Tree
                (2, Tree(2, Tree(2, Tip, Tip), Tree(2, Tip, Tip)), 
                    Tree(2, Tree(2, Tip, Tip), Tree(2, Tip, Tip)))
        let expected =
            Tree
                (1, Tree(1, Tree(1, Tip, Tip), Tree(1, Tip, Tip)), 
                    Tree(1, Tree(1, Tip, Tip), Tree(1, Tip, Tip)))

        tree |> binaryTreeMap mapping |> should equal expected
