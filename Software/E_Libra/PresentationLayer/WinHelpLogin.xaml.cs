using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.Wpf;

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for WinHelpLogin.xaml
    /// </summary>
    public partial class WinHelpLogin : Window {
        private PdfDocument pdfDocument;
        public WinHelpLogin() {
            InitializeComponent();

            LoadPdfDocument();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            ChangeHeight();
        }

        private void LoadPdfDocument() {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF", "User_documentation_login.pdf");
            Process.Start(path);
        }

        private void ChangeHeight() {
            if (pdfView.ActualWidth == 0) {
                pdfView.Height = 1000;
                return;
            }

            pdfView.Height = pdfView.ActualWidth * (pdfDocument.Pages[0].Height / pdfDocument.Pages[0].Width) * pdfDocument.Pages.Count;
            Console.WriteLine("WIDTH: " + pdfView.ActualWidth.ToString());
            Console.WriteLine("HEIGHT: " + pdfView.Height.ToString());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
