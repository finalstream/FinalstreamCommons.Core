using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    /// <summary>
    /// アクションを実行するものを表します。
    /// </summary>
    public class ActionExecuter<T> : IDisposable
    {

        EventLoopScheduler _scheduler = new EventLoopScheduler();

        private ISubject<IGeneralAction<T>> _subject;
        private IDisposable _handle;

        public ActionExecuter(T param)
        {
            _subject = new Subject<IGeneralAction<T>>();

            _handle = _subject.ObserveOn(_scheduler)
                .Subscribe(x => x.Invoke(param));

        }


        public void Post(IGeneralAction<T> action)
        {
            _subject.OnNext(action);
        }

        #region Dispose

        // Flag: Has Dispose already been called?
        private bool disposed = false;

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
                _handle.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion

        
    }
}
