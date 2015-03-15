using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    /// <summary>
    ///     アクションを実行するものを表します。
    /// </summary>
    public class ActionExecuter<T> : IDisposable
    {
        private readonly IDisposable _handle;
        private readonly EventLoopScheduler _scheduler = new EventLoopScheduler();

        private readonly ISubject<IGeneralAction<T>> _subject;

        public ActionExecuter(T param)
        {
            _subject = new Subject<IGeneralAction<T>>();

            _handle = _subject.ObserveOn(_scheduler)
                .Subscribe(x =>
                {
                    {
                        try
                        {
                            x.Invoke(param);
                        }
                        catch (TaskCanceledException)
                        {
                            // とりああえずいまはすてとく。
                            // TODO:なんか処理する。
                        }
                    }
                });
        }


        public void Post(IGeneralAction<T> action)
        {
            _subject.OnNext(action);
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
                _scheduler.Dispose();
                _handle.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}