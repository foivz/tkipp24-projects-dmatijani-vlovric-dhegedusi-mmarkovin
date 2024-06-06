using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class BookNotFoundException : BookException {
        public BookNotFoundException(string message) : base(message) { }
    }
}
