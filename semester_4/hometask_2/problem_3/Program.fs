namespace Problem3

module Main = 
    let mergeSort list =
        let merge first second = 
            let rec mergeRecursive left right continuation =
                match left, right with
                    | [], right -> continuation right
                    | left, [] -> continuation left
                    | headLeft::tailLeft, headRight::tailRight -> 
                        if headLeft < headRight then
                            mergeRecursive tailLeft right (fun acc -> headLeft::acc |> continuation)
                        else
                            mergeRecursive left tailRight (fun acc -> headRight::acc |> continuation)
            mergeRecursive first second id

        let rec mergeSortRecursive list continuation =
            if List.length list <= 1 then
                continuation list
            else
                let left, right = List.splitAt (list.Length / 2) list
                mergeSortRecursive left (fun accLeft -> 
                        mergeSortRecursive right (fun accRight -> merge accLeft accRight |> continuation))

        mergeSortRecursive list id

    let list = [5; 3; 4; 1; 2]
    mergeSort list |> printfn "%A"