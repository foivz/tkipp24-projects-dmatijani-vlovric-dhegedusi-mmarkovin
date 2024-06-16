using BussinessLogicLayer.services;
using EntitiesLayer;
using PresentationLayer.AdminPanels;
using PresentationLayer.MemberPanels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace PresentationLayer {
    //Viktor Lovrić, metode: Window_Loaded
    //Magdalena Markovinocić, metode: btnLogout_Click, btnNotifications_Click
    //David Matijanić, metode: btnClickableImage_Click, OpenLibrAIPanel
    public partial class MemberPanel : Window {
        private LibrAI_Panel librAIPanel { get; set; }

        public MemberPanel() {
            InitializeComponent();

            KeyDown += MainWindow_KeyDown;
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcMemberBorrows();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) {
            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            Hide();
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcBookSearchFilter();
        }

        private void btnWishlist_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcWishlist();
        }

        private void btnReservations_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcReservations();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            using (ReservationService reservationService = new ReservationService()) {
                reservationService.CheckReservationDates();
                var res = reservationService.ShowExistingReservations();
                if (!string.IsNullOrEmpty(res)) {
                    MessageBox.Show(res);
                }
            }
            CheckIsMembershipExpiringSoon();
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e) {
            UcAllNotificationsMember memberNotifications = new UcAllNotificationsMember();
            contentPanel.Content = memberNotifications;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.F1) {
                ShowHelp();
            }
        }

        private void ShowHelp() {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF", "User_documentation_member.pdf");
            Process.Start(path);
        }

        private void btnClickableImage_Click(object sender, RoutedEventArgs e) {
            OpenLibrAIPanel();
        }

        private void OpenLibrAIPanel() {
            if (librAIPanel == null) {
                librAIPanel = new LibrAI_Panel(this);
                librAIPanel.Owner = this;
                librAIPanel.Show();
            } else {
                librAIPanel.Activate();
            }
        }

        public void SetLibrAIPanelToNull() {
            librAIPanel = null;
        }

        private void CheckIsMembershipExpiringSoon() {
            using (MemberService memberService = new MemberService()) {
                var daysUntilExpiration = memberService.CalculateDaysUntilExpiration();
                if (daysUntilExpiration > 0) {
                    MessageBox.Show($"Vaše članstvo ističe za {daysUntilExpiration} dana." + Environment.NewLine +
                "Molimo produljite vaše članstvo!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
        }

        private void btnTopBooks_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcTopBooks();

        }
    }
}
