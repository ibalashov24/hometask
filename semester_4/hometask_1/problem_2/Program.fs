let userInput = System.Console.ReadLine() |> int

let rec fibonacciCalculator x i prev curr =
    if i = x then
        curr
    else
        fibonacciCalculator x (i + 1) curr (prev + curr)
let fibonacci x = if x = 0 then 0 else fibonacciCalculator x 1 0 1

let output = 
    if userInput < 0 then
        printf "Input should be >= 0\n"
    else
        printfn "%d" (fibonacci userInput)