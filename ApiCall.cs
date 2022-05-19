using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Wordle
{
    class ApiCall
    {
        public static async System.Threading.Tasks.Task<dynamic> ApiCallAsync(string url, Dictionary<string, string> headers, Dictionary<string, string> parameters)
        {
            var options = new RestClientOptions(url)
            {
                Timeout = -1
            };
            var client = new RestClient(options);
            var request = new RestRequest();

            foreach (string header in headers.Keys)
                request.AddHeader(header, headers[header]);

            foreach (string parameter in parameters.Keys)
                request.AddParameter(parameter, parameters[parameter]);

            var response = await client.GetAsync(request);
            return JObject.Parse(response.Content);
        }
    }
}
