using DataAccessLayer.F16;
using EntitiesLayer.F16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.F16
{
    public class GPTService
    {
        public string systemMessage { get; set; }
        private IGPTRequestSender requestSender { get; set; }
        private string GPTModel { get; set; }

        public GPTService(IGPTRequestSender requestSender, string gptModel = "gpt-3.5-turbo")
        {
            systemMessage = null;
            this.requestSender = requestSender;
            this.GPTModel = gptModel;
        }

        public bool SetSystemMessage(string message)
        {
            systemMessage = message;
            return true;
        }

        public async Task<string> SendSystemMessage(string message, double temperature = 1)
        {
            return await SendMessage(message, "system", temperature);
        }

        public async Task<string> SendUserMessage(string message, double temperature = 1)
        {
            return await SendMessage(message, "user", temperature);
        }

        private async Task<string> SendMessage(string message, string role, double temperature = 1)
        {
            
            GPTMessage gptMessage = new GPTMessage
            {
                role = role,
                content = message
            };
            List<GPTMessage> messagesToSend = new List<GPTMessage> { gptMessage };
            var gptRequest = new GPTRequest
            {
                messages = messagesToSend,
                model = GPTModel,
                temperature = temperature
            };
            AppendSystemMessage(gptRequest);

            return await requestSender.SendRequest(gptRequest);
        }

        private void AppendSystemMessage(GPTRequest gptRequest)
        {
            if (systemMessage != null)
            {
                gptRequest.messages.Insert(0, new GPTMessage
                {
                    role = "system",
                    content = systemMessage
                });
            }
        }
    }
}
