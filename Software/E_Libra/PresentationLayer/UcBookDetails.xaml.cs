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
using System.Xaml;
using static DataAccessLayer.Repositories.BookRepository;

namespace PresentationLayer
{
    //Viktor Lovrić, metode: HideReserve, UserControl_Loaded, MakeImage, imgBook_ImageFailed, btnSaveReadList_Click, HideReserveDigital, HideAvailable, btnReserve_Click
    // Domagoj Hegedušić, metode: CheckIfDigital, btnAddReview_Click, CreateDigitalButton, DigitalButton_Click
    // David Matijanić: CheckBookBorrowStatus, btnBorrow_Click, BorrowBook
    public partial class UcBookDetails : UserControl
    {
        BookServices bookServices = new BookServices();
        
        private UcBookSearchFilter prevForm;

        public UcBookSearchFilter PrevForm
        {
            set { prevForm = value; }
        }
        Book book;
        BookViewModel bookUI;
        public UcBookDetails(BookViewModel passedBook)
        {
            InitializeComponent();
            book = bookServices.GetBookById(passedBook.Id);
            bookUI = passedBook;
            CheckIfDigital();
            HideReserve();
            CheckBookBorrowStatus();
        }

        private void HideReserve()
        {
            ReservationService reservationService = new ReservationService();
            MemberService memberService = new MemberService();
            int memberId = memberService.GetMemberId(LoggedUser.Username);
            //0 je, ja rezerviram
            //ak opet dodem bit ce sakriveno rezerviraj i pisat tekst
            if (book.current_copies > 0 || reservationService.CheckExistingReservation(book.id, memberId)) //ovo provjerit sa davidom
            {
                btnReserve.Visibility = Visibility.Collapsed;
            }

            if (reservationService.CheckExistingReservation(book.id, memberId))
            {
                tblPosition.Visibility = Visibility.Visible;
                int reservationId = reservationService.GetReservationId(memberId, book.id);
                int position = reservationService.GetReservationPosition(reservationId, book.id);
                tblPosition.Text = tblPosition.Text + " " + position;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = prevForm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MakeImage(book.url_photo);
            tblName.Text = bookUI.Name;
            tblAuthor.Text = bookUI.AuthorName;
            tblDescription.Text = book.description;
            tblGenre.Text = bookUI.GenreName;
            tblDate.Text = !string.IsNullOrEmpty(bookUI.PublishDateDisplay) ? bookUI.PublishDateDisplay : "Nepoznato";

            tblPageNum.Text = book.pages_num.ToString();
            
            if (book.current_copies > 0)
            {
                tblAvailable.Text = "Da, broj raspoloživih primjeraka je: " + book.current_copies;
            }
            else
            {
                tblAvailable.Text = "Ne";
            }
        }

        private void MakeImage(string url)
        {
            if(!string.IsNullOrEmpty(book.url_photo))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                try
                {
                    bitmapImage.UriSource = new Uri(url, UriKind.Absolute);
                    bitmapImage.EndInit();
                    imgBook.Source = bitmapImage;
                }
                catch (Exception)
                {
                    imgBook_ImageFailed(imgBook, null);
                }
                
            }
            else
            {
                imgBook_ImageFailed(imgBook, null);
            }
            
        }

        private void imgBook_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png"));
        }

        private void btnSaveReadList_Click(object sender, RoutedEventArgs e)
        {
            if (bookServices.AddBookToWishlist(book.id))
            {
                MessageBox.Show("Uspješno dodana knjiga na popis Želim pročitati!");
                return;
            }
            else
            {
                MessageBox.Show("Knjiga Vam je već na popisu Želim pročitati!");
                return;
            }
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e) {
            ucReviewsList ucReviewList = new ucReviewsList(book.id);
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucReviewList;

        }

        private void CheckIfDigital() {
            if (book.digital == 1) {
                CreateDigitalButton();
                HideAvailable();
                HideReserveDigital();
            } else {
                return;
            }
        }
        
        private void HideReserveDigital()
        {
            btnReserve.Visibility = Visibility.Collapsed;
            tblPosition.Visibility = Visibility.Collapsed;
        }

        private void HideAvailable()
        {
            tblAvailable.Visibility = Visibility.Collapsed;
            lblAvailable.Visibility = Visibility.Collapsed;
        }

        private void CreateDigitalButton() {

            Button dynamicButton = new Button();
            dynamicButton.Content = "Otvori digitalnu verziju";
            dynamicButton.Width = 150;
            dynamicButton.Height = 40;
            dynamicButton.Margin = new Thickness(0, 0, 10, 0);
            dynamicButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#637E60");
            dynamicButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEFE8");

            dynamicButton.Click += DigitalButton_Click;
            ButtonStackPanel.Children.Add(dynamicButton);

        }

        private void DigitalButton_Click(object sender, RoutedEventArgs e) {
            string online_path = book.url_digital;
            UcDigitalBook ucDigitalBook = new UcDigitalBook(online_path);
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucDigitalBook;
        }
        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            ReservationService reservationService = new ReservationService();
            MemberService memberService = new MemberService();
            int memberId = memberService.GetMemberId(LoggedUser.Username);
            if (reservationService.CountExistingReservations(memberId) == 3)
            {
                MessageBox.Show("Već imate maksimalan broj rezervacija koji je 3!");
                return;
            }

            int position = reservationService.CheckNumberOfReservations(book.id) + 1;
            string text = "Biti ćete " + position + ". na redu čekanja. Potvrdite ili odbijte rezervaciju.";

            WinAcceptDecline winAcceptDecline = new WinAcceptDecline(text);
            winAcceptDecline.ShowDialog();

            if (winAcceptDecline.UserClickedAccept)
            {

                var reservation = new Reservation
                {
                    reservation_date = DateTime.Now,
                    Member_id = memberId,
                    Book_id = book.id,
                };
                bookServices.RemoveOneCopy(book);
                int res = reservationService.AddReservation(reservation);
                bool result = false;
                if (res == 1)
                {
                    result = true;
                }
                MessageBox.Show(result ? "Uspješna rezervacija!" : "Neuspješna rezervacija!");
                HideReserve();
            }
        }
        private void CheckBookBorrowStatus()
        {
            btnBorrow.IsEnabled = true;
            btnBorrow.Visibility = Visibility.Visible;

            BorrowService borrowService = new BorrowService();
            MemberService memberService = new MemberService();
            Member loggedMember = memberService.GetMemberByUsername(LoggedUser.Username);

            List<Borrow> borrows = borrowService.GetBorrowsForMemberAndBook(loggedMember.id, book.id, LoggedUser.LibraryId);
            if (borrows.Count == 0)
            {
                if (book.current_copies == 0)
                {
                    tbBorrowStatus.Text = "Knjiga trenutno nema na zalihi te se ne može posuditi.\nMožete rezervirati knjigu.";
                    btnBorrow.IsEnabled = false;
                    btnBorrow.Visibility = Visibility.Collapsed;
                    return;
                }

                tbBorrowStatus.Text = "Ovu knjigu još niste čitali!\nMožete ju označiti za posudbu.";
                return;
            }

            Borrow borrow = borrows.FirstOrDefault();

            if (borrow.borrow_status != (int)BorrowStatus.Returned)
            {
                btnBorrow.IsEnabled = false;
                btnBorrow.Visibility = Visibility.Collapsed;
            }

            string dateFormat = "dd.MM.yyyy";

            TimeSpan difference;
            switch (borrow.borrow_status)
            {
                case (int)BorrowStatus.Waiting:
                    difference = (TimeSpan)(borrow.return_date - DateTime.Now);
                    int daysLeft = Convert.ToInt16(Math.Ceiling(difference.TotalDays));
                    tbBorrowStatus.Text = $"Knjiga čeka vašu posudbu u knjižnici.\nImate još {daysLeft} dana za posuditi.";
                    break;
                case (int)BorrowStatus.Borrowed:
                    tbBorrowStatus.Text = $"Knjigu ste posudili {borrow.borrow_date.ToString(dateFormat)}.\nTreba ju vratiti do {((DateTime)borrow.return_date).ToString(dateFormat)}.";
                    break;
                case (int)BorrowStatus.Late:
                    difference = (TimeSpan)(DateTime.Now - borrow.return_date);
                    int daysLate = Convert.ToInt16(Math.Ceiling(difference.TotalDays));
                    tbBorrowStatus.Text = $"Knjigu ste posudili {borrow.borrow_date.ToString(dateFormat)}.\nKnjigu ste trebali vratiti do {((DateTime)borrow.return_date).ToString(dateFormat)}.\nKasnite već {daysLate} dana.";
                    break;
                case (int)BorrowStatus.Returned:
                    tbBorrowStatus.Text = "Knjigu ste već čitali!\nSvejedno ju možete označiti za posudbu.";
                    break;
            }
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e)
        {
            BorrowBook();
        }

        private void BorrowBook()
        {
            if (LoggedUser.LibraryId != book.Library_id)
            {
                MessageBox.Show("Knjiga ne pripada ovoj knjižnici!");
                return;
            }

            MemberService memberService = new MemberService();
            Member thisMember = memberService.GetMemberByUsername(LoggedUser.Username);
            if (thisMember == null)
            {
                return;
            }

            int daysToPickUpBook = 5;

            Borrow borrow = new Borrow
            {
                borrow_date = DateTime.Now,
                return_date = DateTime.Now.AddDays(daysToPickUpBook),
                borrow_status = (int)BorrowStatus.Waiting,
                Book = book,
                Member = thisMember
            };

            BorrowService borrowService = new BorrowService();

            try
            {
                int returned = borrowService.AddNewBorrow(borrow);

                if (returned > 0)
                {
                    MessageBox.Show($"Uspješno ste označili knjigu za posudbu. Imate {daysToPickUpBook} dana za doći u knjižnicu i posuditi ju.");
                    CheckBookBorrowStatus();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
