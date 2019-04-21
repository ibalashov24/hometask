namespace Problem1

/// Describes interface for loggers used in Simulator()
type ILogger = 
    abstract member LogState : bool array -> unit