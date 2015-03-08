using System.Data.SQLite;
using System.IO;

namespace FinalstreamCommons.Models.SQLiteFunctions
{
    [SQLiteFunction(Name = "GETDIRPATH", FuncType = FunctionType.Scalar, Arguments = 1)]
    public class GetDirectoryPathSQLiteFunction : SQLiteFunction
    {

        public override object Invoke(object[] args)
        {
            return Path.GetDirectoryName(args[0].ToString());
        }

    }
}
