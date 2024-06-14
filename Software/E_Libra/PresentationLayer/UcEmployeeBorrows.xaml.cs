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
        private EmployeePanel mainWindow { get; set; }

        public UcEmployeeBorrows(EmployeePanel _mainWindow) {
            InitializeComponent();

            this.mainWindow = _mainWindow;
        }

        public async Task LoadAllBorrows() {
            await GetAllBorrowsForLibrary(LoggedUser.LibraryId);
            await GetBorrowsForEachStatus(LoggedUser.LibraryId);
        }

        private async Task GetAllBorrowsForLibrary(int libraryId) {
            using (var borrowService = new BorrowService()) {
                imgLoaderAllBorrows.Visibility = Visibility.Visible;
                txtNoAllBorrows.Visibility = Visibility.Hidden;
                var borrows = await borrowService.GetAllBorrowsForLibraryAsync(libraryId);
                dgAllBorrows.ItemsSource = borrows;
                if (borrows.Count == 0) {
                    txtNoAllBorrows.Visibility = Visibility.Visible;
                }
                imgLoaderAllBorrows.Visibility = Visibility.Hidden;
            }
            
        }

        private async Task GetBorrowsForEachStatus(int libraryId) {
            await GetBorrowsForOneStatus(libraryId, BorrowStatus.Waiting, txtNoPendingBorrows, imgLoaderPendingBorrows, dgPendingBorrows);
            await GetBorrowsForOneStatus(libraryId, BorrowStatus.Borrowed, txtNoCurrentBorrows, imgLoaderCurrentBorrows, dgCurrentBorrows);
            await GetBorrowsForOneStatus(libraryId, BorrowStatus.Late, txtNoLateBorrows, imgLoaderLateBorrows, dgLateBorrows);
            await GetBorrowsForOneStatus(libraryId, BorrowStatus.Returned, txtNoDoneBorrows, imgLoaderDoneBorrows, dgDoneBorrows);
        }

        private async Task GetBorrowsForOneStatus(int libraryId, BorrowStatus status, Border msgNone, Image loader, DataGrid grid) {
            using (var borrowService = new BorrowService()) {
                loader.Visibility = Visibility.Visible;
                msgNone.Visibility = Visibility.Hidden;
                var borrows = await borrowService.GetBorrowsForLibraryByStatusAsync(libraryId, status);
                grid.ItemsSource = borrows;
                if (borrows.Count == 0) {
                    msgNone.Visibility = Visibility.Visible;
                }
                loader.Visibility = Visibility.Hidden;
            }
        }

        private void btnReturnBook_Click(object sender, RoutedEventArgs e) {
            bool notSelected = true;
            string memberBarcode = "";
            string bookBarcode = "";

            if (tbcTabs.SelectedIndex == 2 && dgCurrentBorrows.SelectedItems.Count == 1) {
                Borrow borrow = dgCurrentBorrows.SelectedItem as Borrow;
                if (borrow != null) {
                    using (MemberService memberService = new MemberService())
                    {
                        using (BookServices bookService = new BookServices())
                        {
                            if (borrow.Member_id != null)
                            {
                                memberBarcode = memberService.GetMemberBarcode((int)borrow.Member_id);
                                bookBarcode = bookService.GetBookBarcode(borrow.Book_id);
                                notSelected = false;
                            }
                        }    
                    }
                }
            } else if (tbcTabs.SelectedIndex == 3 && dgLateBorrows.SelectedItems.Count == 1) {
                Borrow borrow = dgLateBorrows.SelectedItem as Borrow;
                if (borrow != null) {
                    using (MemberService memberService = new MemberService())
                    {
                        using (BookServices bookService = new BookServices())
                        {
                            if (borrow.Member_id != null)
                            {
                                memberBarcode = memberService.GetMemberBarcode((int)borrow.Member_id);
                                bookBarcode = bookService.GetBookBarcode(borrow.Book_id);
                                notSelected = false;
                            }
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

            if (tbcTabs.SelectedIndex == 1 && dgPendingBorrows.SelectedItems.Count == 1) {
                Borrow borrow = dgPendingBorrows.SelectedItem as Borrow;
                if (borrow != null) {
                    using (MemberService memberService = new MemberService())
                    {
                        using (BookServices bookService = new BookServices())
                        {
                            if (borrow.Member_id != null)
                            {
                                memberBarcode = memberService.GetMemberBarcode((int)borrow.Member_id);
                                bookBarcode = bookService.GetBookBarcode(borrow.Book_id);
                                notSelected = false;
                            }
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

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            SetDefaultLoaderAndMessageVisibility();
            await LoadAllBorrows();
        }

        private void SetDefaultLoaderAndMessageVisibility() {
            txtNoAllBorrows.Visibility = Visibility.Hidden;
            txtNoPendingBorrows.Visibility = Visibility.Hidden;
            txtNoCurrentBorrows.Visibility = Visibility.Hidden;
            txtNoDoneBorrows.Visibility = Visibility.Hidden;
            imgLoaderAllBorrows.Visibility = Visibility.Hidden;
            imgLoaderPendingBorrows.Visibility = Visibility.Hidden;
            imgLoaderCurrentBorrows.Visibility = Visibility.Hidden;
            txtNoDoneBorrows.Visibility = Visibility.Hidden;
        }
    }
}
