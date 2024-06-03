using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class EmployeeWithSameUsernameException : EmployeeException {
        public EmployeeWithSameUsernameException(string message) : base(message) { }
    }
}
