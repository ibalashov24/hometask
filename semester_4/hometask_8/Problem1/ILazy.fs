namespace Problem1

module LazyInterface =
    /// Lazy calculator interface
    type ILazy<'a> = 
    /// Launches the calculation and returns the result
        abstract member Get: unit -> 'a 