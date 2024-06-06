using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            pdfReaderWeb.LoadCompleted += PdfReaderWeb_LoadCompleted;
            LoadPDF(online_path);
        }

        public void LoadPDF(string pdfFilePath) {
            try {
                pdfReaderWeb.Navigate(new Uri(pdfFilePath));
            } catch (UriFormatException) {
                MessageBox.Show("The link format is invalid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (Exception ex) {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PdfReaderWeb_LoadCompleted(object sender, NavigationEventArgs e) {
            // Check the content of the WebBrowser control to determine if there was an error
            dynamic document = pdfReaderWeb.Document;
            string documentText = document?.documentElement?.InnerHtml;

            if (!string.IsNullOrEmpty(documentText) && documentText.Contains("Navigation to the webpage was canceled")) {
                MessageBox.Show("Link nije važeći ili server ima problema!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
