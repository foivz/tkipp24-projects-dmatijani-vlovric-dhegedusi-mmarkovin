using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.services;
using DataAccessLayer.Repositories;
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
        }

        private async void btnRemoveLibrary_Click(object sender, RoutedEventArgs e) {
            Library selectedLibrary = GetSelectedLibrary();
            if (selectedLibrary == null) {
                return;
            }

            try {
                int successful = service.DeleteLibrary(selectedLibrary);
                if (successful == 0) {
                    MessageBox.Show("Brisanje nije uspjelo!");
                }

                Loader.Visibility = Visibility.Visible;
                await ShowAllLibraries();
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

        private async Task ShowAllLibraries() {
            txtNoLibraries.Visibility = Visibility.Hidden;
            var taskLibraries = service.GetAllLibrariesAsync();
            var libraries = await taskLibraries;
            dgAllLibraries.ItemsSource = libraries;
            Loader.Visibility = Visibility.Hidden;
            if (libraries.Count == 0) {
                txtNoLibraries.Visibility = Visibility.Visible;
            }
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

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            await ShowAllLibraries();
        }
    }
}
