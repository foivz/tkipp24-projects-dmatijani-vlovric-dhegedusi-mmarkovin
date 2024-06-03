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
    public partial class UcWishlist : UserControl
    {
        public UcWishlist()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dgvWishlist.SelectedItem == null)
            {
                MessageBox.Show("Morate odabrati knjigu!");
                return;
            }

            var book = dgvWishlist.SelectedItem as BookViewModel;
            BookServices bookServices = new BookServices();
            if (bookServices.RemoveBookFromWishlist(book.Id)){
                MessageBox.Show("Knjiga je uspješno maknuta!");
                LoadDgv();
                return;
            }
            else
            {
                MessageBox.Show("Neuspješno!");
                return;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDgv();
        }

        private void LoadDgv()
        {
            BookServices bookServices = new BookServices();
            dgvWishlist.ItemsSource = bookServices.GetWishlistedBooks();
            var columnName = dgvWishlist.Columns.FirstOrDefault(c => c.Header.ToString() == "Name");
            columnName.Header = "Naziv";
            columnName = dgvWishlist.Columns.FirstOrDefault(c => c.Header.ToString() == "PublishDate");
            columnName.Header = "Datum izdavanja";
            columnName = dgvWishlist.Columns.FirstOrDefault(c => c.Header.ToString() == "AuthorName");
            columnName.Header = "Ime autora";
            columnName = dgvWishlist.Columns.FirstOrDefault(c => c.Header.ToString() == "GenreName");
            columnName.Header = "Žanr";
            foreach (var column in dgvWishlist.Columns)
            {
                if (column.Header.ToString() == "Id")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
