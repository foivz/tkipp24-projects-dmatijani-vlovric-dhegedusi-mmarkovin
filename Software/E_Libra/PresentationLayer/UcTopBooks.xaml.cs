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
        private readonly MemberService _memberService;
        private readonly BookServices _bookService;

        public UcTopBooks() {
            InitializeComponent();
            _bookService = new BookServices();
            _memberService = new MemberService();

            ShowData();
        }

        private void ShowData() {
            var libraryId = _memberService.GetMemberLibraryId(LoggedUser.Username);

            List<MostPopularBooks> topBorrowedBooks = _bookService.GetTopBorrowedBooks(libraryId);
            dgTopBooks.ItemsSource = topBorrowedBooks;
        }
    }
}
