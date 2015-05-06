using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalstreamCommons.WebServices.Response;
using NLog;
using RestSharp;

namespace FinalstreamCommons.WebServices
{
    public class GoogleCustomSearchService
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        private RestClient _restClient;

        protected string ServiceUrl = "https://www.googleapis.com/customsearch/v1";

        private readonly string _apiKey;

        private readonly string _customSearchEngineId;

        /// <summary>
        /// 新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="customSearchEngineId"></param>
        public GoogleCustomSearchService(string apiKey, string customSearchEngineId)
        {
            _restClient = new RestClient(ServiceUrl);
            _apiKey = apiKey;
            _customSearchEngineId = customSearchEngineId;
        }


        public GoogleCustomSearchResponse Query(string searchKeyword, string lr = "lang_ja")
        {
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", _apiKey);
            request.AddParameter("q", searchKeyword);
            request.AddParameter("cx", _customSearchEngineId);
            request.AddParameter("lr", lr);
            request.AddParameter("fields", "items(link,title)");

            var response = _restClient.Execute<GoogleCustomSearchResponse>(request);

            _log.Info("Google Custom Search Request Url:{0}", response.ResponseUri);
            _log.Debug("Google Custom Search Request Url:{0} Response:{1}", response.ResponseUri, response.Content);
            return response.Data;
        }


    }
}
