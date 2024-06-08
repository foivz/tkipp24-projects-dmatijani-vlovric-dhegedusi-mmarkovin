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

namespace PresentationLayer.MemberPanels {
    // David Matijanić
    public partial class UcMemberBorrows : UserControl {
        private BorrowService borrowService = new BorrowService();
        private MemberService memberService = new MemberService();

        public UcMemberBorrows() {
            InitializeComponent();

            int memberId = memberService.GetMemberId(LoggedUser.Username);
            int libraryId = memberService.GetMemberLibraryId(LoggedUser.Username);

            GetAllBorrowsForMember(memberId, libraryId);
            GetBorrowsForEachStatus(memberId, libraryId);
        }

        private void GetAllBorrowsForMember(int memberId, int libraryId) {
            dgAllBorrows.ItemsSource = borrowService.GetAllBorrowsForMember(memberId, libraryId);
        }

        private void GetBorrowsForEachStatus(int memberId, int libraryId) {
            dgPendingBorrows.ItemsSource = borrowService.GetBorrowsForMemberByStatus(memberId, libraryId, BorrowStatus.Waiting);
            dgCurrentBorrows.ItemsSource = borrowService.GetBorrowsForMemberByStatus(memberId, libraryId, BorrowStatus.Borrowed);
            dgLateBorrows.ItemsSource = borrowService.GetBorrowsForMemberByStatus(memberId, libraryId, BorrowStatus.Late);
            dgDoneBorrows.ItemsSource = borrowService.GetBorrowsForMemberByStatus(memberId, libraryId, BorrowStatus.Returned);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            borrowService.Dispose();
            //TODO: odkomentirati liniju kada MemberService bude realizirao sučelje IDisposable (@mmarkoovin21)
            //memberService.Dispose();
        }
    }
}
