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
    public partial class UcNewLibrary : UserControl {
        private LibraryService service = new LibraryService();
        private bool editing { get; set; }

        public UcNewLibrary() {
            InitializeComponent();
            editing = false;
        }

        public UcNewLibrary(Library libraryToChange) {
            InitializeComponent();
            LoadLibraryDataIntoTextBoxes(libraryToChange);
            editing = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            AdminGuiControl.LoadPreviousControl();
        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {
            SaveLibrary();
        }

        private void SaveLibrary() {
            int newLibraryID;
            if (int.TryParse(tbLibraryID.Text, out newLibraryID)) {}
            else {
                MessageBox.Show("ID treba biti cijeli broj!");
                return;
            }
            string newLibraryName = tbLibraryName.Text;
            if (tbLibraryOIB.Text.Trim() == "") {
                MessageBox.Show("OIB ne smije biti prazan!");
                return;
            }
            string newLibraryOIB = tbLibraryOIB.Text;
            string newLibraryPhone = tbLibraryPhone.Text;
            string newLibraryEmail = tbLibraryEmail.Text;
            decimal newLibraryPriceDayLate;
            if (decimal.TryParse(tbLibraryPriceDayLate.Text, out newLibraryPriceDayLate)) {}
            else {
                MessageBox.Show("Cijena kašnjenja po danu treba biti decimalan broj!");
                return;
            }
            if (newLibraryPriceDayLate < 0) {
                MessageBox.Show("Cijena kašnjenja po danu ne može biti negativna!");
                return;
            }
            string newLibraryAddress = tbLibraryAddress.Text;
            int newLibraryMembershipDurationDays;
            if (int.TryParse(tbLibraryMembershipDuration.Text, out newLibraryMembershipDurationDays)) {}
            else {
                MessageBox.Show("Broj dana trajanja članarine treba biti cijeli broj!");
                return;
            }
            if (newLibraryMembershipDurationDays < 0) {
                MessageBox.Show("Broj dana trajanja članarine ne može biti negativan!");
                return;
            }

            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime newLibraryMembershipDuration = startDate.AddDays(newLibraryMembershipDurationDays - 1);

            Library newLibrary = new Library {
                id = newLibraryID,
                name = newLibraryName.Length <= 45 ? newLibraryName : newLibraryName.Substring(0, 45),
                OIB = newLibraryOIB.Length <= 20 ? newLibraryOIB : newLibraryOIB.Substring(0, 20),
                phone = newLibraryPhone.Length <= 45 ? newLibraryPhone : newLibraryPhone.Substring(0, 45),
                email = newLibraryEmail.Length <= 45 ? newLibraryEmail : newLibraryEmail.Substring(0, 45),
                price_day_late = newLibraryPriceDayLate,
                address = newLibraryAddress.Length <= 100 ? newLibraryAddress : newLibraryAddress.Substring(0, 100),
                membership_duration = newLibraryMembershipDuration
            };

            try {
                if (!editing) {
                    int result = service.AddLibrary(newLibrary);

                    if (result > 0) {
                        AdminGuiControl.LoadNewControl(new UcAllLibraries());
                    } else {
                        MessageBox.Show("Knjižnicu nije moguće dodati.");
                    }
                } else {
                    int result = service.UpdateLibrary(newLibrary);

                    if (result > 0) {
                        AdminGuiControl.LoadNewControl(new UcAllLibraries());
                    } else {
                        MessageBox.Show("Nije napravljena promjena.");
                    }
                }
            } catch (LibraryException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadLibraryDataIntoTextBoxes(Library library) {
            tbLibraryID.IsEnabled = false;

            tbLibraryID.Text = library.id.ToString();
            tbLibraryName.Text = library.name;
            tbLibraryOIB.Text = library.OIB;
            tbLibraryPhone.Text = library.phone;
            tbLibraryEmail.Text = library.email;
            tbLibraryPriceDayLate.Text = library.price_day_late.ToString();
            tbLibraryAddress.Text = library.address;
            tbLibraryMembershipDuration.Text = service.GetLibraryMembershipDuration(library.id).ToString();
        }
    }
}
