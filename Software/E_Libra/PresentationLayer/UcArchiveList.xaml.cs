using BussinessLogicLayer.services;
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

namespace PresentationLayer
{
    //Viktor Lovrić
    public partial class UcArchiveList : UserControl
    {
        public UcArchiveList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (ArchiveServices archiveServices = new ArchiveServices())
            {
                dgvArchive.ItemsSource = archiveServices.GetArchive();
            }
            RenameColumns();
        }

        private void RenameColumns()
        {
            var columnName = dgvArchive.Columns.FirstOrDefault(c => c.Header.ToString() == "BookName");
            columnName.Header = "Naziv knjige";
            columnName = dgvArchive.Columns.FirstOrDefault(c => c.Header.ToString() == "EmployeeName");
            columnName.Header = "Ime zaposlenika";
            columnName = dgvArchive.Columns.FirstOrDefault(c => c.Header.ToString() == "ArchiveDate");
            columnName.Header = "Datum arhiviranja";

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcCatalogueOptions();
        }
    }
}
