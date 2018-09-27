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
        /// Queue which contains threads ready to execute new tasks
        /// </summary>
        private readonly ConcurrentQueue<BasePoolThread> freeThreads;

        /// <summary>
        /// Queue which contains tasks which are waiting to be executed
        /// </summary>
        private readonly ConcurrentQueue<Action<BasePoolThread>> pendingTasks;

        /// <summary>
        /// Cancellation token
        /// </summary>
        private readonly CancellationTokenSource cancellation =
            new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyThreadPool"/> class.
        /// </summary>
        /// <param name="threadCount">Fixed thread count in pool</param>
        public MyThreadPool(int threadCount)
        {
            this.freeThreads = new ConcurrentQueue<BasePoolThread>();
            this.pendingTasks = new ConcurrentQueue<Action<BasePoolThread>>();
            this.MaxThreadCount = threadCount;

            for (int i = 0; i < this.MaxThreadCount; ++i)
            {
                var newThread = new BasePoolThread(this.cancellation.Token);

                newThread.CalculationFinished += this.FreeReleasedThread;
                this.freeThreads.Enqueue(newThread);
            }
        }

        /// <summary>
        /// Gets maximal fixed count of threads in the pool
        /// </summary>
        public int MaxThreadCount { get; }

        /// <summary>
        /// Gets count of free threads in the pool
        /// </summary>
        public int AvailableThreadCount => this.freeThreads.Count;

        /// <summary>
        /// Adds new function to be calculated by the pool threads
        /// </summary>
        /// <typeparam name="TResult">Supplier function result type</typeparam>
        /// <param name="supplier">Function to execute</param>
        /// <returns>Task for calculating function</returns>
        public IMyTask<TResult> AddTask<TResult>(Func<TResult> supplier)
        {
            var newTask = new MyTask<TResult>(supplier, this.EnqueueTask);
            return newTask;
        }

        /// <summary>
        /// Destroy all thread and stop pool work 
        /// (Previous tasks calculation will be finished)
        /// </summary>
        public void Shutdown()
        {
            this.cancellation.Cancel();

            foreach (var freeThread in this.freeThreads)
            {
                freeThread.ForceCancellationTokenCheck();
            }
        }

        /// <summary>
        /// Enqueue released thread back into the thread queue
        /// </summary>
        /// <param name="sender">Released thread</param>
        /// <param name="info">Execution results</param>
        private void FreeReleasedThread(object sender, TaskResultInfo info)
        {
            this.freeThreads.Enqueue((BasePoolThread)sender);
            this.HandlePendingTasks();
        }

        /// <summary>
        /// Callback which inserts given task into the task queue
        /// </summary>
        /// <param name="threadInstaller">
        /// Callback which assign free thread to the task
        /// </param>
        private void EnqueueTask(Action<BasePoolThread> threadInstaller)
        {
            this.pendingTasks.Enqueue(threadInstaller);
            this.HandlePendingTasks();
        }

        /// <summary>
        /// Assigns free threads to pending tasks
        /// </summary>
        private void HandlePendingTasks()
        {
            lock (this.freeThreads)
            {
                while (!this.freeThreads.IsEmpty &&
                    this.pendingTasks.TryDequeue(out Action<BasePoolThread> nextTask))
                {
                    this.freeThreads.TryDequeue(out BasePoolThread freeThread);

                    nextTask(freeThread);
                }
            }
        }
    }
}