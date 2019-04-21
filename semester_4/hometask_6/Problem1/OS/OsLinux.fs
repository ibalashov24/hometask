namespace Problem1

/// Represents Linux OS description
type OsLinux() =
    interface IOperationSystem with
        member this.GetName = "Linux"
        member this.GetInfectionProbability = 0.3
