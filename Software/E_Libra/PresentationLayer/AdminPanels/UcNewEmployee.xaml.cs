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
    public partial class UcNewEmployee : UserControl {
        private bool editing { get; set; }

        public UcNewEmployee(Library selectedLibrary = null) {
            InitializeComponent();
            PopulateComboBox(selectedLibrary);
            editing = false;
        }

        public UcNewEmployee(Employee employeeToChange) {
            InitializeComponent();
            PopulateComboBox(employeeToChange.Library);
            LoadEmployeeDataIntoTextBoxes(employeeToChange);
            editing = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            AdminGuiControl.LoadPreviousControl();
        }

        private void PopulateComboBox(Library selectedLibrary = null) {
            var libraryService = new LibraryService();
            var allLibraries = libraryService.GetAllLibraries();
            cboLibrary.ItemsSource = allLibraries;

            if (selectedLibrary != null) {
                cboLibrary.SelectedItem = allLibraries.FirstOrDefault(l => l.id == selectedLibrary.id);
            }
        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {
            SaveEmployee();
        }

        private void SaveEmployee() {
            Library selectedLibrary = cboLibrary.SelectedItem as Library;
            if (selectedLibrary == null) {
                MessageBox.Show("Potrebno je odabrati knjižnicu!");
                return;
            }
            string newEmployeeName = tbEmployeeName.Text;
            string newEmployeeSurname = tbEmployeeSurname.Text;
            if (tbEmployeeUsername.Text.Trim() == "") {
                MessageBox.Show("Korisničko ime ne smije ostati prazno!");
                return;
            }
            string newEmployeeUsername = tbEmployeeUsername.Text;
            if (tbEmployeePassword.Text.Trim() == "") {
                MessageBox.Show("Lozinka ne smije ostati prazna!");
                return;
            }
            string newEmployeePassword = tbEmployeePassword.Text;
            string newEmployeeOIB = tbEmployeeOIB.Text;
            if (newEmployeeOIB.Length != 11) {
                MessageBox.Show("OIB mora imati 11 znakova!");
                return;
            }

            Employee newEmployee = new Employee {
                name = newEmployeeName.Length <= 45 ? newEmployeeName : newEmployeeName.Substring(0, 45),
                surname = newEmployeeSurname.Length <= 50 ? newEmployeeSurname : newEmployeeSurname.Substring(0, 50),
                username = newEmployeeUsername.Length <= 45 ? newEmployeeUsername : newEmployeeUsername.Substring(0, 45),
                password = newEmployeePassword.Length <= 45 ? newEmployeePassword : newEmployeePassword.Substring(0, 45),
                OIB = newEmployeeOIB,
                Library = selectedLibrary
            };

            try {
                EmployeeService service = new EmployeeService();

                if (!editing) {
                    int result = service.AddEmployee(newEmployee);
                    if (result > 0) {
                        AdminGuiControl.LoadNewControl(new UcAllEmployees(selectedLibrary));
                    } else {
                        MessageBox.Show("Zaposlenika nije moguće dodati.");
                    }
                } else {
                    int result = service.UpdateEmployee(newEmployee);
                    if (result > 0) {
                        AdminGuiControl.LoadNewControl(new UcAllEmployees(selectedLibrary));
                    } else {
                        MessageBox.Show("Nije napravljena promjena.");
                    }
                }
                
            } catch (EmployeeException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadEmployeeDataIntoTextBoxes(Employee employee) {
            cboLibrary.IsEnabled = false;
            tbEmployeeName.Text = employee.name;
            tbEmployeeSurname.Text = employee.surname;
            tbEmployeeUsername.Text = employee.username;
            tbEmployeePassword.Text = employee.password;
            tbEmployeeOIB.Text = employee.OIB;
            tbEmployeeOIB.IsEnabled = false;
        }
    }
}
