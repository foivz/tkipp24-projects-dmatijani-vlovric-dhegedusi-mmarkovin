using EntitiesLayer;
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

namespace PresentationLayer.AdminPanels {
    // David Matijanić
    public partial class AdminPanel : Window {
        public AdminPanel() {
            InitializeComponent();
            AdminGuiControl.AdminPanel = this;

            KeyDown += MainWindow_KeyDown;
        }

        private void btnAllLibraries_Click(object sender, RoutedEventArgs e) {
            UcAllLibraries ucAllLibraries = new UcAllLibraries();
            AdminGuiControl.LoadNewControl(ucAllLibraries);
        }

        private void btnAllEmployees_Click(object sender, RoutedEventArgs e) {
            UcAllEmployees ucAllEmployees = new UcAllEmployees();
            AdminGuiControl.LoadNewControl(ucAllEmployees);
        }

        private void btnNewLibrary_Click(object sender, RoutedEventArgs e) {
            UcNewLibrary ucNewLibrary = new UcNewLibrary();
            AdminGuiControl.LoadNewControl(ucNewLibrary);
        }

        private void btnNewEmployee_Click(object sender, RoutedEventArgs e) {
            UcNewEmployee ucNewEmployee = new UcNewEmployee();
            AdminGuiControl.LoadNewControl(ucNewEmployee);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) {
            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            Hide();
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.F1) {
                ShowHelp();
            }
        }

        private void ShowHelp() {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF", "User_documentation_admin.pdf");
            Process.Start(path);
        }
    }
}
