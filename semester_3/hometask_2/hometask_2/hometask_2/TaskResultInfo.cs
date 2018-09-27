namespace CustomThreading
{
    using System;

    /// <summary>
    /// Contains information about the thread execution results
    /// </summary>
    public class TaskResultInfo : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskResultInfo"/> class.
        /// </summary>
        /// <param name="result">Thread execution result</param>
        public TaskResultInfo(object result)
        {
            this.Result = result;
            this.ThrowedException = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskResultInfo"/> class.
        /// </summary>
        /// <param name="throwedException">Exception raised by the thread</param>
        public TaskResultInfo(Exception throwedException)
        {
            this.Result = null;
            this.ThrowedException = throwedException;
        }

        /// <summary>
        /// Gets thread execution result
        /// </summary>
        public object Result { get; private set; }

        /// <summary>
        /// Gets exception raised by the thread
        /// </summary>
        public Exception ThrowedException { get; private set; }
    }
}