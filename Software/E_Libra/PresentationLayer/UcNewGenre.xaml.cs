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
    public partial class UcNewGenre : UserControl
    {
        private UcAddNewBook prevForm;
        public UcAddNewBook PrevForm
        {
            set { prevForm = value; }
        }
        public event EventHandler ButtonClicked;
        public UcNewGenre()
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
                MessageBox.Show("Morate unijeti ime žanra!");
                return;
            }
            var genre = new Genre
            {
                name = txtName.Text,
            };
            bool res;
            using (GenreServices genreServices = new GenreServices())
            {
                res = genreServices.Add(genre);
            }  
            MessageBox.Show(res ? "Uspješno!" : "Neuspješno!");

            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = prevForm;
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
