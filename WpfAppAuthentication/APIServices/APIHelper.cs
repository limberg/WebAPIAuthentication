using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using WpfAppAuthentication.Models;

namespace WpfAppAuthentication.APIServices
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;

        public APIHelper()
        {
            apiClient = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            apiClient.BaseAddress = new Uri(apiUrl);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Token> Authentication(string username, string password)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password", password)
            });

            using (var response = await apiClient.PostAsync("/token", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    Token token = await response.Content.ReadAsAsync<Token>();

                    return token;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }

    }
}
