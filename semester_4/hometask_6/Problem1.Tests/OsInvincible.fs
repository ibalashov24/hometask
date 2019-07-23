namespace Problem1

/// Represents dummy os with 0 probability to be infected
type OsInvincible() =
    interface IOperationSystem with
        member this.GetName = "Invincible"
        member this.GetInfectionProbability = 0.0
