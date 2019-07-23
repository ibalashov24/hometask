namespace Problem4

module Tests = 
    open NUnit.Framework
    open FsUnit

    let firstPrimes = 
        [
            2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 
            67; 71; 73; 79; 83; 89; 97; 101; 103; 107; 109; 113; 127; 131; 137;
            139; 149; 151; 157; 163; 167; 173; 179; 181; 191; 193; 197; 199
        ]

    [<Test>]
    [<TestCaseSource("firstPrimes")>]
    let ``First prime numbers should be in the sequence`` test = 
        let sequence = Main.generateInfinitePrimeSequence()
        sequence |> Seq.contains test |> should be True

    [<Test>]
    let ``Prime checker should not recognize zero and one as primes`` =
        1 |> Main.isPrime |> should be False
        0 |> Main.isPrime |> should be False

    [<Test>]
    let ``Prime checker should not recognize prime^2 as prime`` = 
        11 * 11 |> Main.isPrime |> should be False
        739 * 739 |> Main.isPrime |> should be False

    [<Test>]
    let ``Prime checker should not recognize negative numbers as primes`` =
        -1 |> Main.isPrime |> should be False
        -3425234 |> Main.isPrime |> should be False
    