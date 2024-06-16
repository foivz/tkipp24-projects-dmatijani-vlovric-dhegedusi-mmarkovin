using BussinessLogicLayer.services;
using EntitiesLayer;
using PresentationLayer.MemberPanels;
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

namespace PresentationLayer.EmployeePanels
{   //Magdalena Markovinocić
    public partial class UcAllNotifications : UserControl
    {
        NotificationService notificationService;
        EmployeeService employeeService;
        public UcAllNotifications()
        {
            InitializeComponent();
            notificationService = new NotificationService();
            employeeService = new EmployeeService();
        }

        private void btnNewNotification_Click(object sender, RoutedEventArgs e)
        {
            UcNewNotification newNotification = new UcNewNotification();
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = newNotification;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            ShowNotifications();
        }
        private void ShowNotifications()
        {
            var id = employeeService.GetEmployeeLibraryId(LoggedUser.Username);
            dgvAllNotifications.ItemsSource = notificationService.GetAllNotificationByLibrary(id);
        }

        private void btnNotificationUpdate_Click(object sender, RoutedEventArgs e)
        {
            Notification selectedNotification = dgvAllNotifications.SelectedItem as Notification;
            if (selectedNotification != null)
            {
                UcEditNotification ucEditNotification = new UcEditNotification(selectedNotification);
                (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucEditNotification;
            } else
            {
                MessageBox.Show("Odaberite obavijest!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            employeeService.Dispose();
            notificationService.Dispose();
        }
    }
}
