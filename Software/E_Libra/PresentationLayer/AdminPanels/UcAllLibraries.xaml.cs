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

namespace PresentationLayer.AdminPanels {
    // David Matijanić
    public partial class UcAllLibraries : UserControl {
        private LibraryService service = new LibraryService();

        public UcAllLibraries() {
            InitializeComponent();
            ShowAllLibraries();
        }

        private void btnRemoveLibrary_Click(object sender, RoutedEventArgs e) {
            Library selectedLibrary = GetSelectedLibrary();
            if (selectedLibrary == null) {
                return;
            }

            try {
                int successful = service.DeleteLibrary(selectedLibrary);
                if (successful == 0) {
                    MessageBox.Show("Brisanje nije uspjelo!");
                }

                ShowAllLibraries();
            } catch (LibraryException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditLibrary_Click(object sender, RoutedEventArgs e) {
            Library selectedLibrary = GetSelectedLibrary();
            if (selectedLibrary == null) {
                return;
            }

            UcNewLibrary ucNewLibrary = new UcNewLibrary(selectedLibrary);
            AdminGuiControl.LoadNewControl(ucNewLibrary);
        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {
            UcNewLibrary ucNewLibrary = new UcNewLibrary();
            AdminGuiControl.LoadNewControl(ucNewLibrary);
        }

        private void btnLibraryEmployees_Click(object sender, RoutedEventArgs e) {
            Library selectedLibrary = GetSelectedLibrary();
            if (selectedLibrary == null) {
                return;
            }

            UcAllEmployees ucAllEmployees = new UcAllEmployees(selectedLibrary);
            AdminGuiControl.LoadNewControl(ucAllEmployees);
        }

        private void ShowAllLibraries() {
            Task.Run(() => {
                var libraries = service.GetAllLibraries();

                Application.Current.Dispatcher.Invoke(() => {
                    dgAllLibraries.ItemsSource = libraries;
                });
            });
        }

        private Library GetSelectedLibrary() {
            if (dgAllLibraries.SelectedItems.Count != 1) {
                MessageBox.Show("Odaberite jednu knjižnicu!");
                return null;
            }

            Library selectedLibrary = dgAllLibraries.SelectedItem as Library;
            if (selectedLibrary == null) {
                MessageBox.Show("Odaberite jednu knjižnicu!");
                return null;
            }

            return selectedLibrary;
        }
    }
}
