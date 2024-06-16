using BussinessLogicLayer.services;
using EntitiesLayer;
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
    public partial class UcArchiveBook : UserControl
    {
        public UcArchiveBook()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcCatalogueOptions();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(dgvBookNamesArchive.SelectedItem == null)
            {
                MessageBox.Show("Morate odabrati knjigu!");
                return;
            }

            Book book = dgvBookNamesArchive.SelectedItem as Book;

            if(book.current_copies != book.total_copies)
            {
                MessageBox.Show("Ne možete arhivirati ovu knjigu jer nisu vraćeni svi primjerci u knjižnicu!");
                return;
            }
            using (EmployeeService employeeServices = new EmployeeService())
            {
                var archive = new Archive
                {
                    Book_id = book.id,
                    Employee_id = employeeServices.GetEmployeeId(LoggedUser.Username),
                    arhive_date = DateTime.Now,
                };
                using (BookServices bookServices = new BookServices())
                {
                    if (bookServices.ArchiveBook(book, archive))
                    {
                        MessageBox.Show("Uspješno!");
                        LoadDgv();
                    }
                    else
                    {
                        MessageBox.Show("Neuspješno!");
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDgv();
        }

        private void LoadDgv()
        {
            using (BookServices services = new BookServices())
            {
                dgvBookNamesArchive.ItemsSource = services.GetNonArchivedBooks(false);
            }
            HideColumns();
        }

        private void HideColumns()
        {
            var columnName = dgvBookNamesArchive.Columns.FirstOrDefault(c => c.Header.ToString() == "name");
            columnName.Header = "Naziv";
            foreach (var column in dgvBookNamesArchive.Columns)
            {
                if (column.Header.ToString() != "Naziv")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void txtBookName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtBookName.Text;
            if (string.IsNullOrEmpty(text))
            {
                dgvBookNamesArchive.ItemsSource = null;
                return;
            }
            using (BookServices bookServices = new BookServices())
            {
                dgvBookNamesArchive.ItemsSource = bookServices.GetNonArchivedBooksByName(text);
            } 
            HideColumns();
        }
    }
}
