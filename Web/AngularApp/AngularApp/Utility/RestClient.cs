using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AngularApp.Utility
{
    public static class RestClient
    {
        public static string Get(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                var response = client.GetAsync(endpoint).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
            }

            throw new Exception(string.Format("Error calling enpoint {0}.", endpoint));
        }

        public static string Post(string endpoint, object parameters)
        {
            var formattedParameters = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                var response = client.PostAsync(endpoint, formattedParameters).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
            }

            throw new Exception(string.Format("Error calling enpoint {0}.", endpoint));
        }
    }
}
