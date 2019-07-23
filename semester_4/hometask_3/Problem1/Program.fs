namespace Problem1

module Main = 
    let input = [5; 4; 2; 2; 8; 1434; 1111; 0; 7; 8]

    /// Calculates even count using List.map and List.fold
    let getEvenUsingMapAndFold (list : int list) = 
        let binaryList = List.map (fun elem -> if elem % 2 = 0 then 1 else 0) list
        let evenCount = List.fold (fun acc elem -> acc + elem) 0 binaryList
        evenCount

    /// Calculates even count using List.filter
    let getEvenUsingFilter (list : int list) = 
        let evenOnly = List.filter (fun elem -> (elem % 2 = 0)) list
        List.length evenOnly

    /// Calculates even count using List.fold
    let getEvenUsingFold (list : int list) = 
        List.fold (fun evenCount elem -> if elem % 2 = 0 then evenCount + 1 else evenCount) 
                    0 
                    list

    input |> getEvenUsingFold |> printfn "Fold only: %d"
    input |> getEvenUsingFilter |> printfn "Filter only: %d"
    input |> getEvenUsingMapAndFold |> printfn "Map&Fold: %d"
