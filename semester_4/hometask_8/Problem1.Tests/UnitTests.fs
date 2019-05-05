namespace Problem1

module Tests = 

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let test() =
        let a = CustomLazyFactory.LazyFactory.CreateSingleThreadedLazy(fun () -> 5)
        5 |> should equal 5