using System.Data.SQLite;
using System.IO;

namespace FinalstreamCommons.Database.SQLiteFunctions
{
    [SQLiteFunction(Name = "GETDIRPATH", FuncType = FunctionType.Scalar, Arguments = 1)]
    // ReSharper disable once InconsistentNaming
    public class GetDirectoryPathSQLiteFunction : SQLiteFunction
    {

        public override object Invoke(object[] args)
        {
            return Path.GetDirectoryName(args[0].ToString());
        }

    }
}
