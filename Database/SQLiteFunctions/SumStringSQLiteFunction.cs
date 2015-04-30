using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FinalstreamCommons.Database.SQLiteFunctions
{
    [SQLiteFunction(Name = "SUMSTR", FuncType = FunctionType.Aggregate, Arguments = 1)]
    // ReSharper disable once InconsistentNaming
    public class SumStringSQLiteFunction : SQLiteFunction
    {

        class Context
        {
            public List<string> Value = new List<string>();
        }

        public override void Step(object[] args, int stepNumber, ref object contextData)
        {
            var data = (contextData as Context) ?? (Context)(contextData = new Context());
            if (!data.Value.Contains(args[0].ToString()))
            {
                data.Value.Add(args[0].ToString());
            }
        }

        public override object Final(object contextData)
        {
            var data = contextData as Context;
            return (data == null) ? null : (object)String.Join(",", (data.Value.ToArray()));
        }

    }
}
