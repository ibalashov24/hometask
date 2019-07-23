namespace Problem3

module Tests =
    open NUnit.Framework
    open FsUnit

    open Main

    [<Test>]
    let ``Interpreter should handle simple test correctly`` () =
        let test = (Application(LambdaAbstraction('x', Variable('x')), Variable('y')))
        let expected = (Variable('y'))

        test |> performBetaReduction |> should equal expected

    [<Test>]
    let ``Interpreter should handle single variable correctly`` () =
        let test = (Variable('a'))
        let expected = (Variable('a'))

        test |> performBetaReduction |> should equal expected

    [<Test>]
    let ``Interpreter should handle application in normal form in correct way`` () =
        let test = (Application(Application(Variable('x'), Variable('y')), 
                        LambdaAbstraction('n', Variable('n'))))
        let expected = test

        test |> performBetaReduction |> should equal expected


    [<Test>]
    let ``Interpreter should handle conversion with multiple substitutions correctly`` () =
        let test = Application(
                        Application(
                            LambdaAbstraction('x',
                                LambdaAbstraction('y',
                                    LambdaAbstraction('z', 
                                        Application(
                                            Application(Variable('x'), Variable('z')), 
                                            Application(Variable('y'), Variable('z')))))),
                            LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))),
                        LambdaAbstraction('x', LambdaAbstraction('y',Variable('x'))))
        let expected = LambdaAbstraction('z', Variable('z'))

        test |> performBetaReduction |> should equal expected

    [<Test>]
    let ``Interpreter should perform alpha conversion correctly`` () =
        let test = Application(
                        LambdaAbstraction('x', 
                            LambdaAbstraction('y', Variable('x'))),
                        Variable('y'))
        let notExpected = LambdaAbstraction('y', Variable('y'))

        test |> performBetaReduction |> should not' (equal notExpected)
