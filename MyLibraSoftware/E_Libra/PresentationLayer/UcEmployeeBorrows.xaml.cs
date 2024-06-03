using BussinessLogicLayer.services;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    // David Matijanić
    public partial class UcEmployeeBorrows : UserControl {
        private BorrowService borrowService = new BorrowService();
        private EmployeePanel mainWindow { get; set; }

        public UcEmployeeBorrows(EmployeePanel _mainWindow) {
            InitializeComponent();

            this.mainWindow = _mainWindow;

            LoadAllBorrows();
        }

        public async void LoadAllBorrows() {
            await GetAllBorrowsForLibrary(LoggedUser.LibraryId);
            await GetBorrowsForEachStatus(LoggedUser.LibraryId);
        }

        private async Task GetAllBorrowsForLibrary(int libraryId) {
            dgAllBorrows.ItemsSource = await borrowService.GetAllBorrowsForLibraryAsync(libraryId);
        }

        private async Task GetBorrowsForEachStatus(int libraryId) {
            dgPendingBorrows.ItemsSource = await borrowService.GetBorrowsForLibraryByStatusAsync(libraryId, BorrowStatus.Waiting);
            dgCurrentBorrows.ItemsSource = await borrowService.GetBorrowsForLibraryByStatusAsync(libraryId, BorrowStatus.Borrowed);
            dgLateBorrows.ItemsSource = await borrowService.GetBorrowsForLibraryByStatusAsync(libraryId, BorrowStatus.Late);
            dgDoneBorrows.ItemsSource = await  borrowService.GetBorrowsForLibraryByStatusAsync(libraryId, BorrowStatus.Returned);
        }

        private void btnReturnBook_Click(object sender, RoutedEventArgs e) {
            bool notSelected = true;
            string memberBarcode = "";
            string bookBarcode = "";

            if (tbcTabs.SelectedIndex == 2) {
                if (dgCurrentBorrows.SelectedItems.Count == 1) {
                    Borrow borrow = dgCurrentBorrows.SelectedItem as Borrow;
                    if (borrow != null) {
                        MemberService memberService = new MemberService();
                        BookServices bookService = new BookServices();
                        if (borrow.Member_id != null) {
                            memberBarcode = memberService.GetMemberBarcode((int)borrow.Member_id);
                            bookBarcode = bookService.GetBookBarcode(borrow.Book_id);
                            notSelected = false;
                        }
                    }
                }
            } else if (tbcTabs.SelectedIndex == 3) {
                if (dgLateBorrows.SelectedItems.Count == 1) {
                    Borrow borrow = dgLateBorrows.SelectedItem as Borrow;
                    if (borrow != null) {
                        MemberService memberService = new MemberService();
                        BookServices bookService = new BookServices();
                        if (borrow.Member_id != null) {
                            memberBarcode = memberService.GetMemberBarcode((int)borrow.Member_id);
                            bookBarcode = bookService.GetBookBarcode(borrow.Book_id);
                            notSelected = false;
                        }
                    }
                }
            }

            if (notSelected) {
                mainWindow.contentPanel.Content = new UcReturnBook(mainWindow, this);
            } else {
                mainWindow.contentPanel.Content = new UcReturnBook(mainWindow, this, memberBarcode, bookBarcode);
            }
            
        }

        private void btnBorrowBook_Click(object sender, RoutedEventArgs e) {
            bool notSelected = true;
            string memberBarcode = "";
            string bookBarcode = "";

            if (tbcTabs.SelectedIndex == 1) {
                if (dgPendingBorrows.SelectedItems.Count == 1) {
                    Borrow borrow = dgPendingBorrows.SelectedItem as Borrow;
                    if (borrow != null) {
                        MemberService memberService = new MemberService();
                        BookServices bookService = new BookServices();
                        if (borrow.Member_id != null) {
                            memberBarcode = memberService.GetMemberBarcode((int)borrow.Member_id);
                            bookBarcode = bookService.GetBookBarcode(borrow.Book_id);
                            notSelected = false;
                        }
                    }
                }
            }

            if (notSelected) {
                mainWindow.contentPanel.Content = new UcBorrowNewBook(mainWindow, this);
            } else {
                mainWindow.contentPanel.Content = new UcBorrowNewBook(mainWindow, this, memberBarcode, bookBarcode);
            }
        }
    }
}
