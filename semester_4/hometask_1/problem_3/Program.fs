let inputList = [ 5; -1; 10; 16; 3 ]

let reversedList list =
    let rec reverse list buf = 
        match list with
            | [] -> buf
            | head :: tail -> reverse tail (head :: buf)
    reverse list []

printfn "Reversed: %A" (reversedList inputList) 

