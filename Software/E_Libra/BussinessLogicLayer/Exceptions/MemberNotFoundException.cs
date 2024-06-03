using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class MemberNotFoundException : Exception {
        public MemberNotFoundException(string message) : base(message) {

        }
    }
}
