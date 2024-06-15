using BussinessLogicLayer.F16;
using BussinessLogicLayer.services;
using DataAccessLayer.F16;
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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for LibrAI_Panel.xaml
    /// </summary>
    public partial class LibrAI_Panel : Window
    {
        private bool EnteredApiKey = false;
        private IGPTRequestSender gptRequestSender { get; set; }
        private GPTService gptService { get; set; }
        private bool WaitingForResponse = false;

        private MemberPanel memberPanel { get; set; }

        public LibrAI_Panel(MemberPanel memberPanel)
        {
            InitializeComponent();
            this.memberPanel = memberPanel;
        }

        private void btnSaveApiKey_Click(object sender, RoutedEventArgs e)
        {
            if (EnteredApiKey)
            {
                return;
            }

            string apiKey = GetEnteredApiKey();
            if (apiKey == "")
            {
                ChangeLaptopImage("angry");
                MessageBox.Show("Nije valjan API ključ!");
                ChangeLaptopImage("introduction");
                return;
            }

            UnhideRequestGUI();

            CreateGPTService(apiKey);
        }

        private async void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (WaitingForResponse)
            {
                return;
            }

            var textToSend = txtRequest.Text;
            if (String.IsNullOrWhiteSpace(textToSend))
            {
                tbResponse.Text = "Unesite valjano pitanje!";
                return;
            }

            WaitingForResponse = true;
            ChangeLaptopImage("thinking");
            await SendAPIRequest(txtRequest.Text);
        }

        private async Task SendAPIRequest(string request)
        {
            var response = await gptService.SendUserMessage(request);
            tbResponse.Text = response;
            WaitingForResponse = false;
            ChangeLaptopImage("responding");
        }

        private string GetEnteredApiKey()
        {
            if (EnteredApiKey)
            {
                return "";
            }

            if (String.IsNullOrWhiteSpace(pbApiKey.Password))
            {
                return "";
            }
            return pbApiKey.Password.Trim();
        }

        private void ChangeLaptopImage(string image)
        {
            imgLaptop.Source = new BitmapImage(new Uri($@"/Images/LibrAI_Images/{image}.png", UriKind.Relative));
        }

        private void CreateGPTService(string apiKey)
        {
            gptRequestSender = new GPTRequestSender(apiKey);
            gptService = new GPTService(gptRequestSender);

            string prompt = "Ti si AI asistent knjižnice, zoveš se LibrAI, a aplikacija u kojoj se nalaziš se zove MyLibra.";
            prompt += " Služiš za pomaganje prijavljenom korisniku, koji je član knjižnice, tako što mu pomažeš oko nekih pitanja vezanih uz knjižnicu.";

            var libraryService = new LibraryService();
            decimal membershipDuration = libraryService.GetLibraryMembershipDuration(LoggedUser.LibraryId);
            prompt += " Ako te korisnik pita, reci mu da je članstvo u knjižnici važeće " + membershipDuration + " dana.";

            decimal pricePerDayLate = libraryService.GetLibraryPriceDayLate(LoggedUser.LibraryId);
            prompt += " Ako te pita koliko košta jedan dan kašnjenja posudbi, reci mu da je to " + pricePerDayLate + " eura po danu.";

            var library = libraryService.GetAllLibraries().First(l => l.id == LoggedUser.LibraryId);
            prompt += " Knjižnica za koju odgovaraš zove se " + library.name + ".";

            gptService.SetSystemMessage(prompt);
        }

        private void txtRequest_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!WaitingForResponse && EnteredApiKey)
            {
                ChangeLaptopImage("listening");
            }
        }

        private void txtRequest_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!WaitingForResponse && EnteredApiKey)
            {
                ChangeLaptopImage("responding");
            }
        }

        private void UnhideRequestGUI()
        {
            tbResponse.Text = "";
            EnteredApiKey = true;
            ChangeLaptopImage("responding");
            btnSendRequest.IsEnabled = true;
            tbEnterApiKey.Visibility = Visibility.Collapsed;
            spApiKeyEnter.Visibility = Visibility.Collapsed;
            spQuestionAnswer.Visibility = Visibility.Visible;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            memberPanel.SetLibrAIPanelToNull();
        }
    }
}
