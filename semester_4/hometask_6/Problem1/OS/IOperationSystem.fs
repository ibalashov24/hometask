namespace Problem1

/// Describes operation system type
type IOperationSystem =
    abstract member GetName: string
    abstract member GetInfectionProbability: double