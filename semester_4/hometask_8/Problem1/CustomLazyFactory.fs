module CustomLazyFactory

/// Represents object factory which produces 3 types of lazy calculator
type LazyFactory() =
    /// Creates new single thread lazy calculator
    static member CreateSingleThreadedLazy<'a> (supplier : unit -> 'a) =
        new LazySync.LazySingleThread<'a>(supplier) :> LazyInterface.ILazy<'a>

    /// Creates new multi thread lazy calculator
    static member CreateMultiThreadedLazy supplier =
        new LazyAsync.LazyMultiThread<'a>(supplier) :> LazyInterface.ILazy<'a>

    /// Creates new lock-free multi thread lazy calculator
    static member CreateLockFreeMultiThreadedLazy supplier =
        new LazyLockFree.LazyLockFree<'a>(supplier) :> LazyInterface.ILazy<'a>
