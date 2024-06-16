using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities {
    public class MostPopularBooksViewModel {
        public int Order_Number { get; set; }
        public string Book_Name { get; set; }
        public string Author_Name { get; set; }
        public int Times_Borrowed { get; set; }
        public string Url_Photo { get; set; }
    }
}
