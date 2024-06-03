using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class LibraryWithSameIDException : LibraryException {
        public LibraryWithSameIDException(string message) : base(message) { }
    }
}
