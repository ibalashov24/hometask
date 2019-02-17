let userInput = System.Console.ReadLine() |> int
let rec factorial x = if x <= 1 then 1 else x * factorial(x - 1)
let output = 
    if userInput < 0 then 
        printfn "Number should be >= 0" 
    else 
        printfn "%d" (factorial userInput)    