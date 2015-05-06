using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalstreamCommons.Frameworks.Actions;

namespace FinalstreamCommons.Frameworks.Actions
{
    public abstract class BackgroundIntervalAction : BackgroundAction
    {
        protected TimeSpan Interval { get; private set; }

        private readonly Stopwatch _stopwatch;

        public BackgroundIntervalAction()
        {
            // デフォルトは10min
            Interval = TimeSpan.FromMinutes(10);
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        protected BackgroundIntervalAction(TimeSpan interval): this()
        {
            Interval = interval;
        }

        protected abstract void InvokeCore();

        protected override void InvokeCoreAsync()
        {
            if (_stopwatch.Elapsed.CompareTo(Interval) > 0)
            {
                InvokeCore();
                _stopwatch.Restart();
            }
        }
    }
}
