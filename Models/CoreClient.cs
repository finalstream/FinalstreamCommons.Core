using NLog;

namespace FinalstreamCommons.Models
{
    public abstract class CoreClient
    {

        private Logger _logger = LogManager.GetCurrentClassLogger();

        protected CoreClient()
        {
        }

        /// <summary>
        /// 初期化を行います。
        /// </summary>
        public void Initialize()
        {
            InitializeCore();



        }

        protected abstract void InitializeCore();


    }
}
