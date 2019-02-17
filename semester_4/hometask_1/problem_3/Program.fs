let rec readList est acc = 
    if est <= 0 then
        acc
    else
        readList (est - 1) (acc @ [System.Console.ReadLine()])

let userInputList =
    printf "Enter size: "
    let listSize = System.Console.ReadLine() |> int
    printf "Enter list (one item per line):\n"
    readList listSize []

let reversedList list =
    let rec reverse list buf = 
        match list with
            | [] -> buf
            | head :: tail -> reverse tail (head :: buf)
    reverse list []

printfn "Reversed: %A" (reversedList userInputList) 

