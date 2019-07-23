namespace Problem3

module Tests =
    open NUnit.Framework
    open FsUnit

    open Main

    [<Test>]
    let ``Phonebook should find phone by name correctly`` () =
        let phonebook = List.empty |> addRecord "Vasya" "12345" |> 
                                                addRecord "Petya" "88005553535"
        let expected = Some("12345")

        phonebook |> findPhoneByName "Vasya" |> should equal expected

    [<Test>]
    let ``Phonebook should find name by phone correctly`` () =
        let phonebook = List.empty |> addRecord "Vasya" "12345" |> 
                                                addRecord "Petya" "88005553535" |> 
                                                addRecord "Vova" "84956970349"
        let expected = Some("Vova")

        phonebook |> findNameByPhone "84956970349" |> should equal expected

    [<Test>]
    let ``Phonebook should serialize itself correctly`` () =
        let phonebook = List.empty |> addRecord "Vasya" "12345" |> 
                                                addRecord "Petya" "88005553535" |> 
                                                addRecord "Vova" "84956970349"
                                                
        phonebook |> exportToFile
        let deserializedPhonebook = importFromFile List.empty

        phonebook |> should equal deserializedPhonebook