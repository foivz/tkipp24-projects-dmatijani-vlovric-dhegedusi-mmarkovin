using EntitiesLayer.F16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.F16
{
    public interface IGPTRequestSender
    {
        Task<string> SendRequest(GPTRequest request);
    }
}
