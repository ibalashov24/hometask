namespace Problem1

/// Represents Windows OS description
type OsWindows() =
    interface IOperationSystem with
        member this.GetName = "Windows"
        member this.GetInfectionProbability = 0.8
