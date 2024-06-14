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

namespace PresentationLayer
{
    //Viktor Lovrić
    public partial class UcAddNewBook : UserControl
    {
        string checkboxValue;
        public UcAddNewBook()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcCatalogueOptions();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGenres();
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            using (AuthorService authorService = new AuthorService())
            {
                cmbAuthor.ItemsSource = authorService.GetAllAuthors();
            } 
        }

        private void LoadGenres()
        {
            using (GenreServices genreServices = new GenreServices())
            {
                var genres = genreServices.GetGenres();
                cmbGenre.ItemsSource = genres;
            }  
        }

        private string ValidateInputs()
        {
            if (txtName.Text == "")
            {
                return "Morate unijeti ime knjige!";
            }
            if (txtNumberCopies.Text == "")
            {
                return "Morate unijeti broj primjeraka knjige! Ako je knjiga digitalna unesite 0";
            }
            if (cmbGenre.Text == "")
            {
                return "Morate odabrati žanr!";
            }
            if (GetCheckBoxValue() == 3)
            {
                return "Morate odabrati je li knjiga digitalna!";
            }
            if (cmbAuthor.Text == "")
            {
                return "Morate odabrati autora!";
            }
            return null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string validationError = ValidateInputs();
            if (validationError != null)
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                ConvertIntoDateTime(txtDate);
                TryParseInt(txtNumberPages.Text);
                TryParseInt(txtNumberCopies.Text);
            }
            catch (BookException ex)
            {
                MessageBox.Show(ex.Poruka);
                return;
            }

            Book book = MakeNewBook();

            var author = cmbAuthor.SelectedItem as Author;
            bool rez;
            using (var bookService = new BookServices())
            {
                rez = bookService.AddBook(book, author);
            }
            MessageBox.Show(rez ? "Uspješno" : "Neuspješno");
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcAddNewBook();
        }

        private Book MakeNewBook()
        {
            using (EmployeeService service = new EmployeeService())
            {
                var book = new Book
                {
                    name = txtName.Text,
                    description = txtDescription.Text,
                    publish_date = ConvertIntoDateTime(txtDate),
                    pages_num = TryParseInt(txtNumberPages.Text),
                    digital = GetCheckBoxValue(),
                    url_digital = txtLinkDigital.Text,
                    url_photo = txtLinkPicture.Text,
                    total_copies = (int)TryParseInt(txtNumberCopies.Text),
                    Genre = cmbGenre.SelectedItem as Genre,
                    Library_id = service.GetEmployeeLibraryId(LoggedUser.Username)
                };

                return book;
            }
        }

        private DateTime? ConvertIntoDateTime(TextBox txtDate)
        {
            if (txtDate.Text == "")
            {
                return null;
            }
            try
            {
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return date;
            }
            catch (FormatException)
            {
                throw new BookException("Neispravan format datuma! Primjer formata je 05-09-2002");
            }
        }


        private int? TryParseInt(string input)
        {
            if (input == "")
            {
                return null;
            }
            if (int.TryParse(input, out int result))
            {
                if(result < 0)
                {
                    throw new BookException("Broj stranica ili primjeraka mora biti pozitivan!");
                }
                return result;
            }
            else
            {
                throw new BookException("Polja u koja se upisuje broj moraju sadržavati samo brojeve!");
            }
        }

        private int GetCheckBoxValue()
        {
            if (checkboxValue == "Da") return 1;
            else if (checkboxValue == "Ne") return 0;
            else return 3;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            
            if(radioButton.IsChecked == true)
            {
                checkboxValue = radioButton.Content.ToString();
                
            }
        }

        private void btnNewAuthor_Click(object sender, RoutedEventArgs e)
        {
            UcNewAuthor ucNewAuthor = new UcNewAuthor();
            ucNewAuthor.PrevForm = this;
            ucNewAuthor.CancelButtonClicked += UcNewAuthor_CancelButtonClicked;
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucNewAuthor;
        }

        private void UcNewAuthor_CancelButtonClicked(object sender, EventArgs e)
        {
            LoadAuthors();
        }

        private void btnNewGenre_Click(object sender, RoutedEventArgs e)
        {
            UcNewGenre ucNewGenre = new UcNewGenre();
            ucNewGenre.PrevForm = this;
            ucNewGenre.ButtonClicked += UcNewGenre_ButtonClicked;
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucNewGenre;
        }

        private void UcNewGenre_ButtonClicked(object sender, EventArgs e)
        {
            LoadGenres();
        }

        private void txtSearchGenre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearchAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
