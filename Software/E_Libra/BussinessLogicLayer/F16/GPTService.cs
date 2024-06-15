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

        public GPTService(IGPTRequestSender requestSender)
        {
            systemMessage = null;
            this.requestSender = requestSender;
        }

        public bool SetSystemMessage(string message)
        {
            systemMessage = message;
            return true;
        }

        public async Task<string> SendSystemMessage(string message)
        {
            return await SendMessage(message, "system");
        }

        public async Task<string> SendUserMessage(string message)
        {
            return await SendMessage(message, "user");
        }

        private async Task<string> SendMessage(string message, string role)
        {
            var gptRequest = new GPTRequest();
            GPTMessage gptMessage = new GPTMessage
            {
                role = role,
                content = message
            };
            gptRequest.messages = new List<GPTMessage> { gptMessage };
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
