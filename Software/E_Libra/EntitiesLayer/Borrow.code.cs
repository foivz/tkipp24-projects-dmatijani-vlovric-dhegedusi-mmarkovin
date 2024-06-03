using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
    public enum BorrowStatus : int {
        Waiting,
        Borrowed,
        Late,
        Returned
    }

    public partial class Borrow {
    }
}
