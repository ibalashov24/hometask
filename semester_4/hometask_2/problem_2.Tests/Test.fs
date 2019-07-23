namespace Problem2

module Tests = 
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``Function should handle non-palindromes correctly`` () = 
        let input = "abcd"
        input |> Main.isPalyndrome |> should be False

    [<Test>]
    let ``Function should handle palindromes with even number of characters correctly`` () =
        let input = "aaabbaaa"
        input |> Main.isPalyndrome |> should be True

    [<Test>]
    let ``Function should handle palindromes with odd number of characters correctly`` () =
        let input = "aaabbbaaa"
        input |> Main.isPalyndrome |> should be True

    [<Test>]
    let ``Function should handle palindromes with spaces and numbers correctly`` () =
        let input = " a  1221  a "
        input |> Main.isPalyndrome |> should be True

    [<Test>]
    let ``Function should recognize empty string as palindrome`` () =
        let input = ""
        input |> Main.isPalyndrome |> should be True
