namespace Problem1

/// Describes interface for loggers used in Simulator()
/// Returns current step
type ILogger = 
    abstract member LogState : bool array -> int