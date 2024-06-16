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

namespace PresentationLayer {
    /// Domagoj Hegedušić
    public partial class UcNewReview : UserControl {
        private int bookId;

        public UcNewReview(int book_id) {
            InitializeComponent();
            bookId = book_id;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            ucReviewsList ucReviews = new ucReviewsList(bookId);
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucReviews;
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e) {
            using (MemberService memberService = new MemberService())
            {
                using (ReviewService reviewService = new ReviewService()) {
                
                    int newRating = cboRating.SelectedIndex;
                    string newComment = txtComment.Text;
                    int rwMember_id = memberService.GetMemberId(LoggedUser.Username);
                    int rwBook_id = bookId;


                    if (cboRating.SelectedItem != null) {

                        Review newReview = new Review {
                            Member_id = rwMember_id,
                            Book_id = rwBook_id,
                            comment = newComment,
                            rating = newRating,
                            date = DateTime.Today
                        };

                        reviewService.AddReview(newReview);

                        ucReviewsList ucReviews = new ucReviewsList(bookId);
                        (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucReviews;
                    } else {
                        MessageBox.Show("Niste odabrali ocjenu!");
                    }
                }
            }
        }
    }
}
