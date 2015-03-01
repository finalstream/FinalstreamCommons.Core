using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Builders
{
    public class CommandLineBuilder : Microsoft.Build.Utilities.CommandLineBuilder
    {

        /// <summary>
        /// クリアします。
        /// </summary>
        public void Clear()
        {
            CommandLine.Clear();
        }
    }
}
