using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.AdminPanels {
    // David Matijanić
    public static class AdminGuiControl {
        public static AdminPanel AdminPanel { get; set; }
        private static UserControl previousUserControl { get; set; }

        static AdminGuiControl() {
            previousUserControl = null;
        }

        public static void LoadNewControl(UserControl newUserControl) {
            previousUserControl = AdminPanel.contentPanel.Content as UserControl;
            AdminPanel.contentPanel.Content = newUserControl;
        }

        public static void LoadPreviousControl() {
            AdminPanel.contentPanel.Content = previousUserControl;
        }
    }
}
