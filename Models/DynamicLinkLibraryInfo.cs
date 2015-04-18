using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    public class DynamicLinkLibraryInfo
    {

        public string FilePath { get; private set; }

        public string Categroy { get; private set; }
        
        public string LibraryName { get; private set; }

        public string Version { get { return _version.FileVersion; }}

        public string License { get; private set; }

        public string Copyright { get; private set; }

        public string Url { get; private set; }

        public string BBCodeUrl { get { return string.Format("[url={0}]{0}[/url]", Url); }}

        private FileVersionInfo _version;

        public DynamicLinkLibraryInfo(string filePath, string libraryName, string categroy, string license, string copyright, string url)
        {
            FilePath = filePath;
            Categroy = categroy;
            LibraryName = libraryName;
            License = license;
            Copyright = copyright;
            Url = url;
            _version = FileVersionInfo.GetVersionInfo(FilePath);
        }
    }
}
