using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DataAccessLayer.Repositories.BookRepository;

namespace PresentationLayer {
    // Domagoj Hegedušić
    public partial class UcDigitalBook : UserControl {
        public UcDigitalBook(string online_path) {
            InitializeComponent();
            LoadPDF(online_path);
        }


        public void LoadPDF(string pdfFilePath) {
            pdfReaderWeb.Navigate(new Uri(pdfFilePath));
        }
    }
}
