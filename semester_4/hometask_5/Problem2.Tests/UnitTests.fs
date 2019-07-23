namespace Problem2

module Tests = 
    open NUnit.Framework
    open FsCheck

    [<Test>]
    let ``Original and point-free functions should return same results`` () =
        Check.Quick(fun x l -> (Main.func x l) = (Main.func'4 x l))

    [<Test>]
    let ``Original and V3 functions should return same results`` () =
        Check.Quick(fun x l -> (Main.func x l) = (Main.func'3 x l))

    [<Test>]
    let ``Original and V2 functions should return same results`` () =
        Check.Quick(fun x l -> (Main.func x l) = (Main.func'2 x l))

    [<Test>]
    let ``Original and V1 functions should return same results`` () =
        Check.Quick(fun x l -> (Main.func x l) = (Main.func'1 x l))
    
     
        