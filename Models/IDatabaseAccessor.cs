using System;
using System.Data.SQLite;

namespace FinalstreamCommons.Models
{
    public interface IDatabaseAccessor : IDisposable
    {
        string DatabaseName { get; }

        SQLExecuter SqlExecuter { get; }

        SQLiteTransaction BeginTransaction();
    }
}