using DataAccessLayer.F16;
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

        public GPTService(IGPTRequestSender requestSender)
        {

        }

        public bool SetSystemMessage(string message)
        {
            systemMessage = message;
            return true;
        }

        public void SendSystemMessage(string message)
        {

        }
    }
}
