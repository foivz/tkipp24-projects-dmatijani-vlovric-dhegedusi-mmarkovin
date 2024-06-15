using EntitiesLayer.F16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.F16
{
    public class GPTRequestSender : IGPTRequestSender
    {
        public async Task<GPTResponse> SendRequest(GPTRequest request)
        {
            await Task.Delay(100);

            return new GPTResponse();
        }
    }
}
