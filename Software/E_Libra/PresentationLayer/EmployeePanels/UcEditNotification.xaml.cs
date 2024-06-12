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
    public partial class UcEditNotification : UserControl
    {
        NotificationService notificationService;
        EmployeeService employeService;
        Notification editNotification;
        public UcEditNotification(Notification notification)
        {
            InitializeComponent();
            editNotification = notification;
            txtTitle.Text = editNotification.title;
            txtDescription.Text = editNotification.description;
            notificationService = new NotificationService();
            employeService = new EmployeeService();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            editNotification.title = txtTitle.Text;
            editNotification.description = txtDescription.Text;
            var edited = notificationService.EditNotification(editNotification);
            if (edited)
            {
                MessageBox.Show("Obavjest", "Obavjest izmjenjena!", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcAllNotifications();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcAllNotifications();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            notificationService.Dispose();
        }
    }
}
