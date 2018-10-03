namespace CustomThreading
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    /// <summary>
    /// Thread pool with fixed count of threads
    /// </summary>
    public class MyThreadPool
    {
        /// <summary>
        /// Contains all threads in the pool
        /// </summary>
        private readonly Thread[] threads;

        /// <summary>
        /// Queue which contains tasks which are waiting to be executed
        /// </summary>
        private readonly ConcurrentQueue<Action<bool>> pendingTasks =
            new ConcurrentQueue<Action<bool>>();

        /// <summary>
        /// Cancellation token
        /// </summary>
        private readonly CancellationTokenSource cancellation =
            new CancellationTokenSource();

        /// <summary>
        /// Unlocks threads if there are tasks waiting for execution
        /// </summary>
        private readonly AutoResetEvent threadGuard = new AutoResetEvent(false);

        /// <summary>
        /// Count of live threads
        /// </summary>
        private volatile int availableThreadCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyThreadPool"/> class.
        /// </summary>
        /// <param name="threadCount">Fixed thread count in pool</param>
        public MyThreadPool(int threadCount)
        {
            this.MaxThreadCount = threadCount;
            this.availableThreadCount = this.MaxThreadCount;

            this.threads = new Thread[threadCount];
            for (int i = 0; i < this.threads.Length; ++i)
            {
                this.threads[i] = new Thread(() =>
                {
                    while (!this.cancellation.IsCancellationRequested)
                    {
                        if (this.pendingTasks.TryDequeue(out Action<bool> executeTask))
                        {
                            executeTask(false);
                        }
                        else
                        {
                            this.threadGuard.WaitOne();
                        }
                    }

                    --this.availableThreadCount;
                });

                this.threads[i].IsBackground = true;
                this.threads[i].Start();
            }
        }

        /// <summary>
        /// Gets maximal fixed count of threads in the pool
        /// </summary>
        public int MaxThreadCount { get; }

        /// <summary>
        /// Gets count of live treads
        /// </summary>
        public int AvailableThreadCount => this.availableThreadCount;

        /// <summary>
        /// Adds new function to be calculated by the pool threads
        /// </summary>
        /// <typeparam name="TResult">Supplier function result type</typeparam>
        /// <param name="supplier">Function to execute</param>
        /// <returns>Task for calculating function</returns>
        public IMyTask<TResult> AddTask<TResult>(Func<TResult> supplier)
        {
            if (this.cancellation.IsCancellationRequested)
            {
                return null;
            }

            var newTask = new MyTask<TResult>(this, supplier);

            this.pendingTasks.Enqueue(newTask.ExecuteTaskManually);
            this.threadGuard.Set();

            return newTask;
        }

        /// <summary>
        /// Destroy all thread and stop pool work 
        /// (Previous tasks calculation will be finished)
        /// </summary>
        public void Shutdown()
        {
            this.cancellation.Cancel();
            this.threadGuard.Set();

            while (this.pendingTasks.TryDequeue(out Action<bool> taskToDisable))
            {
                taskToDisable(true);
            }
        }
    }
}