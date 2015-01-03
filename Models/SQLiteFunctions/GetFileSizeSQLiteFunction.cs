using System.Data.SQLite;
using System.IO;

namespace FinalstreamCommons.Models.SQLiteFunctions
{
    [SQLiteFunction(Name = "GETFILESIZE", FuncType = FunctionType.Scalar, Arguments = 1)]
    public class GetFileSizeSQLiteFunction : SQLiteFunction
    {

        public override object Invoke(object[] args)
        {
            long result = 0;
            if (File.Exists(args[0].ToString()))
            {
                FileInfo fileInfo = new FileInfo(args[0].ToString());
                result = fileInfo.Length;
            }
            return result;
        }

    }
}
