let userInput = System.Console.ReadLine() |> int

let rec factorialWithAccumulation x acc = 
    if x <= 1 then 
        acc 
    else 
        factorialWithAccumulation (x - 1) (acc * x)
let factorial x = factorialWithAccumulation x 1

let output = 
    if userInput < 0 then 
        printfn "Number should be >= 0" 
    else 
        printfn "%d" (factorial userInput)