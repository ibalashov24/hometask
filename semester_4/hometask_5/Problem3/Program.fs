namespace Problem3

module Main = 
    open System
    open System.IO
    open System.Runtime.Serialization.Formatters.Binary

    /// File to keep serialized data
    let serializationFileName = "serialized.data"

    /// Reads and inserts new record to the phonebook
    let addRecord records =
        printf "Enter new record (name phone): "
        let input = Console.ReadLine().Split([|' '|])

        if (Array.length input) <> 2 then
            printfn("Invalid record!")
            records
        else
            printfn("Success!")
            (input.[0], input.[1]) :: records

    /// Finds phone by name in the phonebook
    let findPhoneByName records = 
        let rec finder sampleName records =
            match records with
                | (name, phone) :: tail when name = sampleName ->
                    Some phone
                | (_, _) :: tail -> tail |> finder sampleName
                | _ -> None

        printf "Enter name: "
        let name = Console.ReadLine()

        match (finder name records) with
            | None -> printfn "Phone not found"
            | Some phone -> phone |> printfn "Phone: %s" 

    /// Finds name by phone in the phonebook
    let findNameByPhone records = 
        let rec finder samplePhone records =
            match records with
                | (name, phone) :: tail when phone = samplePhone ->
                    Some name
                | (_, _) :: tail -> tail |> finder samplePhone
                | _ -> None

        printf "Enter phone: "
        let phone = Console.ReadLine()

        match (finder phone records) with
            | None -> printfn "Name not found"
            | Some name -> name |> printfn "Name: %s" 

    /// Prints all the records in the phonebook
    let printAllRecords records = 
        let rec printer records = 
            if not (List.isEmpty records) then
                let (name, phone) = List.head records
                printfn "%s %s" name phone
                List.tail records |> printer

        if List.isEmpty records then
            printfn "No records"
        else
            printfn "Printing all records: "
            records |> printer 

    /// Imports phonebook from the file
    let importFromFile records =
        let inStream = new FileStream(serializationFileName, FileMode.Open)
        let formatter = new BinaryFormatter()
        let result = unbox<(string * string) list>(formatter.Deserialize(inStream))
        inStream.Close()

        printfn "Successfully imported from the file"

        result @ records

    /// Exports phonebook to the file
    let exportToFile records =
        let outStream = new FileStream(serializationFileName, FileMode.Create)
        let formatter = new BinaryFormatter()
        formatter.Serialize(outStream, records)
        outStream.Close()

        printfn "Successfully exported to the file"

    /// Prints help
    let printHelp () =
        printfn "1 - exit"
        printfn "2 - add new record"
        printfn "3 - find phone by name"
        printfn "4 - find name by phone"
        printfn "5 - print all records"
        printfn "6 - export phonebook to file"
        printfn "7 - import phonebook from file"

    /// Main loop of the phonebook keeper
    let rec mainLoop records = 
        printf "Enter command: "
        let cmd = Console.ReadLine()
        match cmd with 
            | "1" -> true
            | "2" -> addRecord records |> mainLoop
            | "3" -> 
                findPhoneByName records 
                records |> mainLoop
            | "4" -> 
                findNameByPhone records
                records |> mainLoop
            | "5" -> 
                printAllRecords records
                records |> mainLoop
            | "6" -> 
                exportToFile records
                records |> mainLoop
            | "7" -> importFromFile records |> mainLoop
            | _ -> 
                printHelp ()
                records |> mainLoop

    printHelp ()
    if mainLoop List.empty then
        printfn "Good bye!"