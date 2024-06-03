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
using static DataAccessLayer.Repositories.BookRepository;

namespace PresentationLayer
{
    //Viktor Lovrić
    public partial class UcBookSearchFilter : UserControl
    {
        BookServices bookServices;
        public UcBookSearchFilter()
        {
            InitializeComponent();
            bookServices = new BookServices();
            cbCheck.IsChecked = true;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ch = (bool)cbCheck.IsChecked;
            ApplyFilter(ch);

        }

        private void ApplyFilter(bool ch)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dgvBookSearch.ItemsSource = null;
                return;
            }
            //0-sve, 1-žanr, 2-pisac, 3-godina
            switch (cmbFilter.SelectedIndex)
            {
                case 0:
                    dgvBookSearch.ItemsSource = bookServices.SearchBooks(txtSearch.Text, ch);
                    HideRenameColumns();
                    break;
                case 1:
                    dgvBookSearch.ItemsSource = bookServices.GetBooksByGenre(txtSearch.Text, ch);
                    HideRenameColumns();
                    break;
                case 2:
                    dgvBookSearch.ItemsSource = bookServices.GetBooksByAuthor(txtSearch.Text, ch);
                    HideRenameColumns();
                    break;
                case 3:
                    if (int.TryParse(txtSearch.Text, out int year))
                    {
                        dgvBookSearch.ItemsSource = bookServices.GetBooksByYear(year, ch);
                        HideRenameColumns();
                    }
                    else
                    {
                        txtSearch.Text = null;
                    }
                    break;
                default:
                    break;
            }
        }

        private void HideRenameColumns()
        {
            var columnName = dgvBookSearch.Columns.FirstOrDefault(c => c.Header.ToString() == "Name");
            columnName.Header = "Naziv";
            columnName = dgvBookSearch.Columns.FirstOrDefault(c => c.Header.ToString() == "PublishDateDisplay");
            columnName.Header = "Datum izdavanja";
            columnName = dgvBookSearch.Columns.FirstOrDefault(c => c.Header.ToString() == "AuthorName");
            columnName.Header = "Ime autora";
            columnName = dgvBookSearch.Columns.FirstOrDefault(c => c.Header.ToString() == "GenreName");
            columnName.Header = "Žanr";
            columnName = dgvBookSearch.Columns.FirstOrDefault(c => c.Header.ToString() == "Digital");
            columnName.Header = "Digitalna";
            foreach (var column in dgvBookSearch.Columns)
            {
                if (column.Header.ToString() == "Id" || column.Header.ToString() == "PublishDate")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dgvBookSearch.ItemsSource = null;
            txtSearch.Text = string.Empty;
            cmbFilter.SelectedIndex = 0;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dgvBookSearch.SelectedItem == null)
            {
                MessageBox.Show("Morate odabrati knjigu!");
                return;
            }
            BookViewModel bookUI = dgvBookSearch.SelectedItem as BookViewModel;
            UcBookDetails ucBookDetails = new UcBookDetails(bookUI)
            {
                PrevForm = this
            };
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucBookDetails;
        }

        private void cbCheck_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilter(true);
        }

        private void cbCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilter(false);
        }
    }
}
