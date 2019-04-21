namespace Problem1

/// Represents dummy os with 1 probability to be infected
type OsSieve() =
    interface IOperationSystem with
        member this.GetName = "Sieve"
        member this.GetInfectionProbability = 1.0
        