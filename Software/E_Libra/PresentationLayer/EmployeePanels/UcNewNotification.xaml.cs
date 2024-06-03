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

namespace PresentationLayer.EmployeePanels
{
    //Magdalena Markovinocić
    public partial class UcNewNotification : UserControl
    {
        NotificationService notificationService;
        EmployeeService employeService;
        public UcNewNotification()
        {
            InitializeComponent();
            notificationService = new NotificationService();
            employeService = new EmployeeService();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcAllNotifications();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int id = employeService.GetEmployeeLibraryId(LoggedUser.Username);
            Notification newNotification = new Notification()
            {
                title = txtTitle.Text,
                description = txtDescription.Text,
                Library_id = id
            };
            var added = notificationService.AddNewNotification(newNotification);
            if (added)
            {
                MessageBox.Show("Obavjest", "Obavjest dodana", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcAllNotifications();
            }
        }
    }
}
