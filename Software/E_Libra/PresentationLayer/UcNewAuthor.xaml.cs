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

namespace PresentationLayer
{
    //Viktor Lovrić
    public partial class UcNewAuthor : UserControl
    {
        private UcAddNewBook prevForm;
        public UcAddNewBook PrevForm
        {
            set { prevForm = value; }
        }
        public event EventHandler CancelButtonClicked;
        public UcNewAuthor()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = prevForm;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Morate unijeti ime knjige!");
                return;
            }
            if (txtSurname.Text == "")
            {
                MessageBox.Show("Morate unijeti prezime!");
                return;
            }
            try
            {
                ConvertIntoDateTime(txtBirthDate);
            }catch (Exception)
            {
                MessageBox.Show("Neispravan format datuma! Primjer formata je 05-09-2002");
                return;
            }
            var author = new Author
            {
                name = txtName.Text,
                surname = txtSurname.Text,
                birth_date = ConvertIntoDateTime(txtBirthDate),
            };
            AuthorService authorService = new AuthorService();
            var res = authorService.AddAuthor(author);
            MessageBox.Show(res ? "Uspješno" : "Neuspješno");

            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = prevForm;
            CancelButtonClicked?.Invoke(this, EventArgs.Empty);


        }
        private DateTime? ConvertIntoDateTime(TextBox txtDate)
        {
            if (txtDate.Text == "")
            {
                return null;
            }
            DateTime date = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }
    }

}
