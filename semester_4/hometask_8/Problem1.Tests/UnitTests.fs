module Tests

open System.Threading
open NUnit.Framework
open FsUnit

open CustomLazyFactory
open LazyInterface
open LazyAsync
open LazySync
open LazyLockFree

[<Test>]
let ``Single thread lazy calculator should work correctly on simple test`` () =
    let calculator = LazyFactory.CreateSingleThreadedLazy(fun () -> 5)
    calculator.Get () |> should equal 5

[<Test>]
let ``Multi thread lazy calculator should work correctly on simple test`` () =
    let calculator = LazyFactory.CreateMultiThreadedLazy(fun () -> 42)
    calculator.Get () |> should equal 42

[<Test>]
let ``Lock free multi thread lazy calculator should work correctly on simple test`` () =
    let calculator = LazyFactory.CreateLockFreeMultiThreadedLazy(fun () -> 51)
    calculator.Get () |> should equal 51

[<Test>]
let ``Multi thread lazy calculator should call supplier only one time`` () =
    let mutable callCounter = ref 0L
    let calculator = LazyFactory.CreateMultiThreadedLazy(fun () -> 
        Interlocked.Increment callCounter |> ignore
        (Interlocked.Read callCounter) |> should lessThan 2)

    for i in 1..1000 do
        ThreadPool.QueueUserWorkItem (fun obj -> calculator.Get ()) |> ignore

[<Test>]
let ``Single thread lazy calculator should call supplier only one time`` () =
    let mutable callCounter = 0
    let calculator = LazyFactory.CreateSingleThreadedLazy(fun () -> 
        callCounter <- callCounter + 1
        callCounter |> should lessThan 2)

    for i in 1..1000 do
        calculator.Get ()

[<Test>]
let ``Multi threaded lazy should return the same object on every call`` () =
    let calculator = LazyFactory.CreateMultiThreadedLazy(fun () -> new System.Object())

    let expected = calculator.Get ()
    for i in 1..100 do
        ThreadPool.QueueUserWorkItem (fun obj -> 
            expected |> (calculator.Get ()).Equals |> should be True) |> ignore

[<Test>]
let ``Lock free multi threaded lazy should return the same object on every call`` () =
    let calculator = LazyFactory.CreateLockFreeMultiThreadedLazy(fun () -> new System.Object())

    let expected = calculator.Get ()
    for i in 1..100 do
        ThreadPool.QueueUserWorkItem (fun obj -> 
            expected |> (calculator.Get ()).Equals |> should be True) |> ignore

