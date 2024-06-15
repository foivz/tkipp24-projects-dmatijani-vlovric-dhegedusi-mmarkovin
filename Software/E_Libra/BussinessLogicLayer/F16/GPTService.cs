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
            this.requestSender = requestSender;
        }

        public bool SetSystemMessage(string message)
        {
            systemMessage = message;
            return true;
        }

        public void SendSystemMessage(string message)
        {
            var gptRequest = new GPTRequest();
            GPTMessage gptMessage = new GPTMessage
            {
                content = message
            };
            gptRequest.messages = new List<GPTMessage> { gptMessage };
            requestSender.SendRequest(gptRequest);
        }
    }
}
