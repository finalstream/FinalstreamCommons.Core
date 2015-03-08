using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    public abstract class DatabaseAccessor : IDatabaseAccessor
    {
        public string DatabaseName { get; protected set; }

        public SQLExecuter SqlExecuter { get; protected set; }

        public SQLiteTransaction BeginTransaction()
        {
            return SqlExecuter.BeginTransaction();
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
                if (SqlExecuter != null) SqlExecuter.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}
