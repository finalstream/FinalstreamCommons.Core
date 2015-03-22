using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    /// <summary>
    /// アセンブリ情報を保持します。
    /// </summary>
    public class AssemblyInfoData
    {
        public static readonly AssemblyInfoData ExecutingAssembly = new AssemblyInfoData(Assembly.GetExecutingAssembly());

        public string FileName { get; private set; }
        public string Version { get; private set; }
        public string FileVersion { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Configuration { get; private set; }
        public string Company { get; private set; }
        public string Product { get; private set; }
        public string Copyright { get; private set; }
        public string Trademark { get; private set; }
        public string Culture { get; private set; }
        public AssemblyName[] RefrencedAssemblyNames { get; private set; }

        public AssemblyInfoData(Assembly assembly)
        {
            var assemblyName = new AssemblyName(assembly.FullName);
            FileName = assemblyName.Name;
            Version = assemblyName.Version.ToString();
            
            FileVersion = GetAttributeName<AssemblyFileVersionAttribute>(assembly, a => a.Version);
            Title = GetAttributeName<AssemblyTitleAttribute>(assembly, a => a.Title);
            Description = GetAttributeName<AssemblyDescriptionAttribute>(assembly, a => a.Description);
            Configuration = GetAttributeName<AssemblyConfigurationAttribute>(assembly, a => a.Configuration);
            Company = GetAttributeName<AssemblyCompanyAttribute>(assembly, a => a.Company);
            Product = GetAttributeName<AssemblyProductAttribute>(assembly, a => a.Product);
            Copyright = GetAttributeName<AssemblyCopyrightAttribute>(assembly, a => a.Copyright);
            Trademark = GetAttributeName<AssemblyTrademarkAttribute>(assembly, a => a.Trademark);
            Culture = GetAttributeName<AssemblyCultureAttribute>(assembly, a => a.Culture);
            RefrencedAssemblyNames = assembly.GetReferencedAssemblies();
        }

        private string GetAttributeName<T>(Assembly assembly, Func<T, string> selector) where T : Attribute
        {
            var attr = assembly.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
            return (attr == null) ? "" : selector(attr);
        }
    }
}
