using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace B2wServerTools.Sync
{
    internal class HttpUtils
    {
        private static HttpClient _httpClient;
        private static string WebUrl = "";

        private static void InitHttp()
        {
            if (_httpClient != null) return;
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(CfgUtils.WebUrl) };
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static T Post<T>(string postUrl, object postData)
        {
            InitHttp();
            if (_httpClient != null)
            {
                var res = _httpClient.PostAsJsonAsync(postUrl, postData).Result;
                if(res.StatusCode!= HttpStatusCode.OK)
                {
                    throw new Exception(res.ReasonPhrase+res.Content.ReadAsStringAsync().Result);
                }
                var content = res.Content.ReadAsAsync<T>();
                var result = content.Result;
                return result;
            }
            return default(T);
        }
    }
}
