using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class EmployeeWithSameOIBException : EmployeeException {
        public EmployeeWithSameOIBException(string message) : base(message) { }
    }
}
