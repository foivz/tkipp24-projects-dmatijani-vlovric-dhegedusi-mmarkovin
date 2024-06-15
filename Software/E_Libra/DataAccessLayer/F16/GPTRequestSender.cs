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

            using (var client = new HttpClient())
            {
                var endpoint = new Uri(uri);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(endpoint, requestPayload);
                if (!response.IsSuccessStatusCode)
                {
                    return "Dogodila se greška kod dohvata odgovora.";
                }

                //var responseContent = await response.Content.ReadAsStringAsync();
                return await GetAnswerFromResponseContent(response.Content);
            }
        }

        private StringContent CreatePayloadFromRequest(GPTRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            return new StringContent(requestJson, Encoding.UTF8, "application/json");
        }

        private async Task<string> GetAnswerFromResponseContent(HttpContent content)
        {
            var contentString = await content.ReadAsStringAsync();
            GPTResponse responseObject = JsonConvert.DeserializeObject<GPTResponse>(contentString);
            if (responseObject.choices == null)
            {
                return "Dogodila se greška kod čitanja odgovora.";
            }

            var choices = responseObject.choices;
            if (choices.Count == 0)
            {
                return "Nisu dobiveni nikakvi odgovori.";
            }

            var firstChoice = choices[0];
            var choiceMessage = firstChoice.message;
            if (choiceMessage == null)
            {
                return "Dogodila se greška.";
            }

            return choiceMessage.content;
        }
    }
}
