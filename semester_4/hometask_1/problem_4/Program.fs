let n = 3
let m = 10

let twoPowerNtoM n m =
    let reversedList list =
        let rec reverse list buf = 
            match list with
                | [] -> buf
                | head :: tail -> reverse tail (head :: buf)
        reverse list []
             
    let rec generateList n m i accPower accList = 
        if (1 <= i) && (i < n) then
            generateList n m (i + 1) (2 * accPower) accList
        elif n <= i && i <= m then
            generateList n m (i + 1) (2 * accPower) (2 * accPower :: accList)
        else
            accList

    generateList n m 1 1 [] |> reversedList

printfn "Powers from 2^%d to 2^%d: %A" n m (twoPowerNtoM n m)