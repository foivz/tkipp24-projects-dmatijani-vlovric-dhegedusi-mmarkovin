using BussinessLogicLayer.services;
using DataAccessLayer.Repositories;
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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer {
    // David Matijanić
    public partial class UcReturnBook : UserControl {
        private EmployeePanel mainWindow { get; set; }
        private UcEmployeeBorrows parentUserControl { get; set; }
        private Borrow borrow = null;

        private BorrowService borrowService = new BorrowService();

        public UcReturnBook(EmployeePanel _mainWindow, UcEmployeeBorrows _parentUserControl) {
            InitializeComponent();

            this.mainWindow = _mainWindow;
            this.parentUserControl = _parentUserControl;

            btnReturnBorrow.IsEnabled = false;
            btnReturnBorrow.Visibility = Visibility.Collapsed;
        }

        public UcReturnBook(EmployeePanel _mainWindow, UcEmployeeBorrows _parentUserControl, string memberBarcode, string bookBarcode) {
            InitializeComponent();

            this.mainWindow = _mainWindow;
            this.parentUserControl = _parentUserControl;

            btnReturnBorrow.IsEnabled = false;
            btnReturnBorrow.Visibility = Visibility.Collapsed;

            tbMemberBarcode.Text = memberBarcode;
            tbBookBarcode.Text = bookBarcode;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            ReturnParentUserControl();
        }

        private void ReturnParentUserControl() {
            mainWindow.contentPanel.Content = parentUserControl;
        }

        private void btnCheckBorrow_Click(object sender, RoutedEventArgs e) {
            CheckForBorrows();
        }

        private void CheckForBorrows() {
            string borrowInformation = "";
            tbBorrowInfo.Text = "";
            borrow = null;

            if (!CheckInputFields()) {
                return;
            }

            Book enteredBook = GetEnteredBook();
            if (enteredBook == null) {
                return;
            }

            Member enteredMember = GetEnteredMember();
            if (enteredMember == null) {
                return;
            }

            Borrow existingBorrow = GetBorrow(enteredBook, enteredMember);
            if (existingBorrow == null) {
                borrowInformation += "Ne postoji aktualna posudba!";
                tbBorrowInfo.Text = borrowInformation;
                return;
            }

            borrow = existingBorrow;

            string dateFormat = "dd.MM.yyyy";
            bool late = existingBorrow.borrow_status == (int)BorrowStatus.Late;

            borrowInformation += "Postoji posudba!" + Environment.NewLine;
            borrowInformation += "Član: " + existingBorrow.Member.ToString() + Environment.NewLine;
            borrowInformation += "Knjiga: " + existingBorrow.Book.ToString() + Environment.NewLine;
            borrowInformation += "Kasni? " + (late ? "DA" : "NE") + Environment.NewLine;
            borrowInformation += "Datum posudbe: " + existingBorrow.borrow_date.ToString(dateFormat) + Environment.NewLine;
            if (existingBorrow.return_date != null) {
                borrowInformation += "Rok za vraćanje: " + ((DateTime)existingBorrow.return_date).ToString(dateFormat);
                if (late) {
                    TimeSpan difference = DateTime.Now - (DateTime)existingBorrow.return_date;
                    int daysLate = Convert.ToInt16(Math.Ceiling(difference.TotalDays));
                    borrowInformation += $" - kasni {daysLate} dana.";
                }
                borrowInformation += Environment.NewLine;
            }

            tbBorrowInfo.Text = borrowInformation;

            btnReturnBorrow.IsEnabled = true;
            btnReturnBorrow.Visibility = Visibility.Visible;
        }

        private async void btnReturnBorrow_Click(object sender, RoutedEventArgs e) {
            if (borrow == null) {
                MessageBox.Show("Ne postoji posudba!");
                return;
            }

            if (borrow.borrow_status == (int)BorrowStatus.Late) {
                using (var libraryService = new LibraryService()) {
                    decimal priceDayLate = libraryService.GetLibraryPriceDayLate(LoggedUser.LibraryId);
                    TimeSpan difference = DateTime.Now - (DateTime)borrow.return_date;
                    int daysLate = Convert.ToInt16(Math.Ceiling(difference.TotalDays));

                    decimal priceToPay = priceDayLate * daysLate;

                    MessageBoxResult result = MessageBox.Show($"Potreban iznos za platiti kašnjenje: {priceToPay}." + Environment.NewLine + "Je li član platio iznos?", "Kašnjenje", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.No) {
                        MessageBox.Show("Knjiga neće biti vraćena.");
                        return;
                    }
                }
            }

            //TODO: ovdje staviti using za employeeService kad realizira sučelje IDisposable (@mmarkoovin21)
            EmployeeService employeeService = new EmployeeService();
            Employee thisEmployee = employeeService.GetEmployeeByUsername(LoggedUser.Username);
            if (thisEmployee == null) {
                return;
            }

            Borrow updatedBorrow = new Borrow {
                idBorrow = borrow.idBorrow,
                borrow_date = borrow.borrow_date,
                return_date = DateTime.Now,
                borrow_status = (int)BorrowStatus.Returned,
                Book = borrow.Book,
                Member = borrow.Member,
                Employee = borrow.Employee,
                Employee1 = thisEmployee
            };

            int updateResult = borrowService.UpdateBorrow(updatedBorrow);
            if (updateResult == 0) {
                MessageBox.Show("Knjiga nije vraćena.");
            }
            await UpdateParentBorrows();
            ReturnParentUserControl();
        }

        private bool CheckInputFields() {
            btnReturnBorrow.IsEnabled = false;
            btnReturnBorrow.Visibility = Visibility.Collapsed;

            if (tbBookBarcode.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti unesen barkod knjige!");
                return false;
            }

            if (tbMemberBarcode.Text.Trim().Length == 0) {
                MessageBox.Show("Mora biti unesen barkod sa članske iskaznice člana!");
                return false;
            }

            return true;
        }

        private Member GetEnteredMember() {
            //TODO: ovdje staviti using za memberService kad realizira sučelje IDisposable (@mmarkoovin21)
            MemberService memberService = new MemberService();
            try {
                Member enteredMember = memberService.GetMemberByBarcodeId(LoggedUser.LibraryId, tbMemberBarcode.Text);
                return enteredMember;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private Book GetEnteredBook() {
            //TODO: ovdje staviti using za bookService kad realizira sučelje IDisposable (@vlovric21)
            BookServices bookService = new BookServices();
            try {
                Book enteredBook = bookService.GetBookByBarcodeId(LoggedUser.LibraryId, tbBookBarcode.Text);
                return enteredBook;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private Borrow GetBorrow(Book book, Member member) {
            try {
                return borrowService.GetBorrowsForMemberAndBook(member.id, book.id, LoggedUser.LibraryId).Find(b => b.borrow_status != (int)BorrowStatus.Returned
                    && b.borrow_status != (int)BorrowStatus.Waiting);
            } catch (Exception) {
                return null;
            }
        }

        private async Task UpdateParentBorrows() {
            await parentUserControl.LoadAllBorrows();
            parentUserControl.tbcTabs.SelectedIndex = 3;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            borrowService.Dispose();
        }
    }
}
