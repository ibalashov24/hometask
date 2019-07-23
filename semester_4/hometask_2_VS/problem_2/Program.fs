namespace Problem2

module Main = 
    let input = "aaabbbaaa"

    let isPalyndrome (input : string) = 
        let rec reverse list buf = 
            match list with
                | [] -> buf
                | head :: tail -> reverse tail (head :: buf)

        let rec isPalyndromeRecursive str (reversed : char list) =
            match str with
                | [] -> true
                | head :: tail -> if head = reversed.Head then isPalyndromeRecursive tail reversed.Tail else false

        let inputList = input.ToCharArray() |> List.ofArray
        let inputReversedList = reverse inputList []

        isPalyndromeRecursive inputList inputReversedList

    isPalyndrome input |> printfn "%b"
    