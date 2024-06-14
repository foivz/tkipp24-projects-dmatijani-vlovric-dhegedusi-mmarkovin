using BussinessLogicLayer.services;
using EntitiesLayer;
using EntitiesLayer.Entities;
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

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for UcTopBooks.xaml
    /// </summary>
    public partial class UcTopBooks : UserControl {
        private readonly EmployeeService _employeeService;

        public UcTopBooks() {
            InitializeComponent();
            var _bookService = new BookServices();
            _employeeService = new EmployeeService();


            var libraryId = _employeeService.GetEmployeeLibraryId(LoggedUser.Username);

            Task.Run(() => {
                List<MostPopularBooks> topBorrowedBooks = _bookService.GetTopBorrowedBooks(libraryId);

                Application.Current.Dispatcher.Invoke(() => {
                    dgTopBooks.ItemsSource = topBorrowedBooks;
                });
            });
        }
    }
}
