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
using BussinessLogicLayer.services;
using PresentationLayer.AdminPanels;
using EntitiesLayer.Entities;
using DataAccessLayer.Repositories;

namespace PresentationLayer {
    // Domagoj Hegedušić
    public partial class ucReviewsList : UserControl {
        ReviewService services = new ReviewService();
        MemberService memberService = new MemberService();
        BorrowService borrowService = new BorrowService();
        private int bookId;
        private int memberId;
        public ucReviewsList(int book_id) {
            InitializeComponent();
            bookId = book_id;
            memberId = memberService.GetMemberId(LoggedUser.Username);
            LoadReviews();

        }

        private void LoadReviews() {
            if (bookId <= 0) {
                dgReviews.ItemsSource = new List<ReviewInfo>();
                return;
            }

            Task.Run(() => {
                List<ReviewInfo> reviews = services.GetReviewsForBook(bookId);

                Application.Current.Dispatcher.Invoke(() => {
                    dgReviews.ItemsSource = reviews;
                });
            });
        }

        private void btnRemoveReview_Click(object sender, RoutedEventArgs e) {

            if (services.HasUserReviewedBook(memberId, bookId)) {
                services.DeleteReview(memberId, bookId);
                LoadReviews();
                MessageBox.Show("Vaša recenzija je uspješno obrisana.");
            } else {
                MessageBox.Show("Niste napisali recenziju za ovu knjigu!");
            }
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e) {

            bool hasUserBorrowedBook = borrowService.HasUserBorrowedBook(memberId, bookId);

            if (hasUserBorrowedBook) {
                if (services.HasUserReviewedBook(memberId, bookId)) {
                    MessageBox.Show("Već si napisao recenziju za ovu knjigu!");
                } else {
                    UcNewReview ucNewReview = new UcNewReview(bookId);
                    (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucNewReview;
                }
            } else {
                MessageBox.Show("Moraš posuditi knjigu prije pisanja recenzije!");
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            services.Dispose();
            memberService.Dispose();
            borrowService.Dispose();
        }
    }

}