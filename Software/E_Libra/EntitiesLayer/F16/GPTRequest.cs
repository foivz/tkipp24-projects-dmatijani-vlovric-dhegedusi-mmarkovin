using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.F16
{
    public class GPTRequest
    {
        public string model { get; set; }
        public List<GPTMessage> messages { get; set; }
        public double temperature { get; set; }
    }
}
