using BussinessLogicLayer.Exceptions;
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

namespace PresentationLayer.MemberPanels
{
    //Magdalena Markovinocić
    public partial class UcAllNotificationsMember : UserControl
    {
        NotificationService notificationService;
        Member loggedMember;
        List<Notification> readNotifications;
        List<Notification> unreadNotifications;
        List<Notification> allNotificationsFotLibrary;

        public UcAllNotificationsMember()
        {
            InitializeComponent();
            notificationService = new NotificationService();
            LoadAllNotifications();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgvNotifications.ItemsSource = allNotificationsFotLibrary;

        }
        private void LoadAllNotifications()
        {
            int libraryId;
            using (var memberService = new MemberService())
            {
                libraryId = memberService.GetMemberLibraryId(LoggedUser.Username);
                loggedMember = memberService.GetMemberByUsername(LoggedUser.Username);

            }
                readNotifications = notificationService.GetReadNotificationsForMember(loggedMember);
                List<int> readIds = readNotifications.Select(n => n.id).ToList();
                allNotificationsFotLibrary = notificationService.GetAllNotificationByLibrary(libraryId);
                unreadNotifications = allNotificationsFotLibrary.Where(n => !readIds.Contains(n.id)).ToList();
        }

        private void btnNotificationDetails_Click(object sender, RoutedEventArgs e)
        {
            Notification selectedNotification = dgvNotifications.SelectedItem as Notification;
            if (selectedNotification != null) { 
                (Window.GetWindow(this) as MemberPanel).contentPanel.Content = new UcDetailsNotification(selectedNotification);
                notificationService.AddNotificationRead(selectedNotification, loggedMember);
            } else
            {
                MessageBox.Show("Odaberite obavijest!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReadNotif_Click(object sender, RoutedEventArgs e)
        {
            dgvNotifications.ItemsSource = readNotifications;
        }

        private void btnUnreadNotif_Click(object sender, RoutedEventArgs e)
        {
            dgvNotifications.ItemsSource = unreadNotifications;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            notificationService.Dispose();
        }
    }
}
