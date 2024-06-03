using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class LibraryHasEmployeesException : LibraryException {
        public LibraryHasEmployeesException(string message) : base(message) { }
    }
}
