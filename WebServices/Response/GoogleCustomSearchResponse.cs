using System.Collections.Generic;

namespace FinalstreamCommons.WebServices.Response
{
    public class GoogleCustomSearchResponse
    {

        public List<GoogleCustomSearchItem> items { get; set; } 
    }

    public class GoogleCustomSearchItem
    {
        public string title { get; set; }
        public string link { get; set; }
    }
}