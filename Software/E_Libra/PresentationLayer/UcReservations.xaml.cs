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
    public partial class UcReservations : UserControl
    {
        MemberService memberService = new MemberService();
        int memberId;
        public UcReservations()
        {
            InitializeComponent();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dgvReservations.SelectedItem == null)
            {
                MessageBox.Show("Morate odabrati rezervaciju!");
                return;
            }
            var selectedReservation = dgvReservations.SelectedItem as ReservationViewModel;
            ReservationService reservationService = new ReservationService();
            if (reservationService.RemoveReservationFromList(selectedReservation.ReservationId))
            {
                MessageBox.Show("Uspješno!");
                LoadDgv();
            }
            else
            {
                MessageBox.Show("Neuspješno!");
            }
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            memberId = memberService.GetMemberId(LoggedUser.Username);
            LoadDgv();
        }

        private void LoadDgv()
        {
            ReservationService reservationService = new ReservationService();
            dgvReservations.ItemsSource = reservationService.GetReservationForMember(memberId);
            RenameHideColumns();
        }

        private void RenameHideColumns()
        {
            var columnName = dgvReservations.Columns.FirstOrDefault(c => c.Header.ToString() == "BookName");
            columnName.Header = "Knjiga";
            columnName = dgvReservations.Columns.FirstOrDefault(c => c.Header.ToString() == "Date");
            columnName.Header = "Datum isteka rezervacije";
            foreach (var column in dgvReservations.Columns)
            {
                if (column.Header.ToString() == "ReservationId")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
