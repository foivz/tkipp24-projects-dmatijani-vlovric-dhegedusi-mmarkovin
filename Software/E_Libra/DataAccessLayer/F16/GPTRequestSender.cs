using EntitiesLayer.F16;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.F16
{
    public class GPTRequestSender : IGPTRequestSender
    {
        private const string uri = "https://api.openai.com/v1/chat/completions";
        private string apiKey { get; set; }

        public GPTRequestSender(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<string> SendRequest(GPTRequest request)
        {
            var requestPayload = CreatePayloadFromRequest(request);

            using (var client = CreateConfiguredHttpClient())
            {
                var response = await client.PostAsync(new Uri(uri), requestPayload);
                if (!response.IsSuccessStatusCode)
                {
                    return "Dogodila se greška kod dohvata odgovora.";
                }

                return await ExtractContentFromResponse(response.Content);
            }
        }

        private HttpClient CreateConfiguredHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private StringContent CreatePayloadFromRequest(GPTRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            return new StringContent(requestJson, Encoding.UTF8, "application/json");
        }

        private async Task<string> ExtractContentFromResponse(HttpContent content)
        {
            var contentString = await content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<GPTResponse>(contentString);

            if (responseObject?.choices == null || responseObject.choices.Count == 0)
            {
                return "Nisu dobiveni nikakvi odgovori.";
            }

            var firstChoiceMessage = responseObject.choices.FirstOrDefault()?.message;
            return firstChoiceMessage?.content ?? "Dogodila se greška.";
        }
    }
}
