module Test

open NUnit.Framework
open FsUnit

open Main

[<Test>]
let ``Downloader should download page correctly`` () =
    let pages = "http://edition.cnn.com/EVENTS/1996/year.in.review/" |> downloadPages

    // 7.05.2019
    pages |> List.length |> should equal 3

    for subpage in pages do
        if subpage |> Option.isNone then
            Assert.Fail("Every supbpage should be accessible!")

[<Test>]
let ``Downloader should handle invalid urls correctly`` () =
    // Reserved domain
    let page = "http://example.su/" |> downloadPages

    page |> List.length |> should equal 1
    page.[0] |> should equal None