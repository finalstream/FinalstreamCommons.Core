using System;
using System.IO;
using System.Reflection;
using FinalstreamCommons.Extentions;
using FinalstreamCommons.Utils;
using Newtonsoft.Json;
using NLog;

namespace FinalstreamCommons.Models
{
    public abstract class CoreClient
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        protected Assembly ExecutingAssembly;

        protected CoreClient(Assembly executingAssembly)
        {
            ExecutingAssembly = executingAssembly;
        }

        /// <summary>
        /// 初期化を行います。
        /// </summary>
        public void Initialize()
        {
            var execAssembly = new AssemblyInfoData(ExecutingAssembly);
            _log.Info("Start Application: {0} {1} {2}", 
                execAssembly.Product, 
                execAssembly.Version,
                ApplicationUtils.IsAssemblyDebugBuild(ExecutingAssembly) ? "Debug" : "Release");
            execAssembly.RefrencedAssemblyNames.DebugWriteJson("RefrenceAssemblys", ObjectExtensions.LogFormat.Indented);
            InitializeCore();
        }

        /// <summary>
        /// 終了処理を行います。
        /// </summary>
        public void Finish()
        {
            FinalizeCore();
        }

        /// <summary>
        /// 設定をファイルからロードします。
        /// </summary>
        protected T LoadConfig<T>(string configFilePath) where T : IAppConfig, new()
        {
            if (!File.Exists(configFilePath)) return new T();
            return JsonConvert.DeserializeObject<T>(
                File.ReadAllText(configFilePath));
        }

        /// <summary>
        /// 設定をファイルに保存します。
        /// </summary>
        protected void SaveConfig<T>(string configFilePath, T config) where T : IAppConfig, new()
        {
            if (configFilePath == null) return;
            Directory.CreateDirectory(Path.GetDirectoryName(configFilePath));
            File.WriteAllText(configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        protected abstract void InitializeCore();
        protected abstract void FinalizeCore();
    }
}