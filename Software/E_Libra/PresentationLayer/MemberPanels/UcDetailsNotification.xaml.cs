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
    public partial class UcDetailsNotification : UserControl
    {
        Notification selectedNotification;
        public UcDetailsNotification(Notification notification)
        {
            InitializeComponent();
            selectedNotification = notification;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lblTitle.Text = selectedNotification.title;
            lblDescription.Text = selectedNotification.description;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = new UcAllNotificationsMember();
        }
    }
}
