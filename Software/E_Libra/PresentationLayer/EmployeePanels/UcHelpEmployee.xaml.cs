using Patagames.Pdf.Net.Wrappers.OptionalContent;
using Patagames.Pdf.Net;
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
using Patagames.Pdf.Net.Controls.Wpf;

namespace PresentationLayer.EmployeePanels {
    // David Matijanić
    public partial class UcHelpEmployee : UserControl {
        private PdfDocument pdfDocument;

        public UcHelpEmployee() {
            InitializeComponent();

            LoadPdfDocument();
        }

        private void LoadPdfDocument() {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF", "User_documentation_employee.pdf");
            pdfDocument = PdfDocument.Load(path);
            PdfViewer pdfViewer = new PdfViewer();
            pdfViewer.Document = pdfDocument;

            pdfView.Content = pdfViewer;
            ChangeHeight();
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

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            ChangeHeight();
        }
    }
}
