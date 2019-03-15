namespace Problem4

module Main = 
    /// Checks if given number is prime
    let isPrime number =
        let rec isPrimeRec number i = 
            if i <> number && number % i = 0 || number < 2 then
                false
            else if i > number / 2 then 
                true
            else 
                isPrimeRec number (i + 1)

        isPrimeRec number 2

    /// Generates infinite sequence of prime number
    let generateInfinitePrimeSequence () =
        id |> Seq.initInfinite |> Seq.filter isPrime

    generateInfinitePrimeSequence () |> printfn "%A"