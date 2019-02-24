namespace Problem1

module Main =
    let list = [5; 17; 8; 1; 2; 3; 8; 8; -100; 17] 
    let sample = 0   

    let findEntry list elem = 
        let rec findEntryRecursive (list : int list) (elem : int) i =
            match list with
                | [] -> -1
                | head :: tail -> 
                    if head = elem then i else findEntryRecursive tail elem (i + 1)

        findEntryRecursive list elem 0

    printfn "First entry (-1 <-> not found): %d" <| findEntry list sample
   