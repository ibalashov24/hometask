namespace CustomThreading
{
    using System;
    using System.Threading;

    /// <summary>
    /// A wrapper for <see cref="Thread "/> intended for <see cref="MyThreadPool"/>
    /// </summary>
    public class BasePoolThread
    {
        /// <summary>
        /// Thread which executes supplier functions
        /// </summary>
        private readonly Thread mainThread;

        /// <summary>
        /// Locks main thread until new supplier function enter
        /// </summary>
        private readonly AutoResetEvent threadGuard = new AutoResetEvent(false);

        /// <summary>
        /// Cancels thread work if activated
        /// </summary>
        private readonly CancellationToken cancellationToken;

        /// <summary>
        /// Function which is currently being executed by <see cref="mainThread"/>
        /// </summary>
        private Func<TaskResultInfo> currentSupplier;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePoolThread"/> class.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public BasePoolThread(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;

            this.mainThread = new Thread(() =>
            {
                this.threadGuard.WaitOne(); // Waiting for the first request

                while (this.currentSupplier != null ||
                    !cancellationToken.IsCancellationRequested)
                {
                    var executionInfo = this.currentSupplier();

                    this.currentSupplier = null;
                    this.IsAvailable = true;

                    // If there is at least 1 handler
                    this.CalculationFinished?.Invoke(this, executionInfo);

                    if (this.cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    this.threadGuard.WaitOne(); // Waiting for the next request
                }
            })
            {
                IsBackground = true
            };

            this.mainThread.Start();
        }

        /// <summary>
        /// Is activated when the thread has completed its work
        /// and contains the calculation results
        /// </summary>
        public event EventHandler<TaskResultInfo> CalculationFinished;

        /// <summary>
        /// Gets a value indicating whether thread is free and ready
        /// to accept new supplier function
        /// </summary>
        public bool IsAvailable { get; private set; } = true;

        /// <summary>
        /// Assigns new supplier function for the thread.
        /// If thread is dead sends
        /// <see cref="ThreadStateException"/> in result info
        /// </summary>
        /// <param name="newTask">Supplier function to execute</param>
        public void AssignTask(Func<TaskResultInfo> newTask)
        {
            if (!this.IsAvailable || this.cancellationToken.IsCancellationRequested)
            {
                this.ForceCancellationTokenCheck();
                this.CalculationFinished?.Invoke(
                    this,
                    new TaskResultInfo(new ThreadStateException("Thread is not available!")));

                return;
            }

            this.currentSupplier = newTask;
            this.IsAvailable = false;

            this.threadGuard.Set();
        }

        /// <summary>
        /// Unlocks guard (and thread) in order to allow
        /// execution flow reach cancellation token check
        /// </summary>
        public void ForceCancellationTokenCheck()
        {
            if (this.cancellationToken.IsCancellationRequested)
            {
                this.threadGuard.Set();
            }
        }
    }
}