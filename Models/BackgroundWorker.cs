using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    /// <summary>
    ///     バッググラウンドで動作するスレッドを表します。
    /// </summary>
    public class BackgroundWorker : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;

        public ICollection<BackgroundAction> BackgroundActions { get; private set; }

        public BackgroundWorker(ICollection<BackgroundAction> backgroundActions)
        {
            BackgroundActions = backgroundActions;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            // タスクを開始する。
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    foreach (var backgroundAction in BackgroundActions)
                    {
                        backgroundAction.InvokeAsync();
                    }

                    Task.Delay(1000).Wait();
                }
            },
                _cancellationTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        #region Dispose

        // Flag: Has Dispose already been called?
        private bool disposed;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                _cancellationTokenSource.Cancel();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}