namespace Problem1

type OsLinux() =
    interface IOperationSystem with
        member this.GetName = "Linux"
        member this.GetInfectionProbability = 0.3
