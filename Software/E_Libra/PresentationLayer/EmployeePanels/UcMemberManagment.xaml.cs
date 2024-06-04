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

namespace PresentationLayer.EmployeePanels
{
    //Magdalena Markovinocić
    public partial class UcMemberManagment : UserControl
    {
        MemberService memberService;
        public UcMemberManagment()
        {
            InitializeComponent();
            memberService = new MemberService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgvMembers.ItemsSource = memberService.GetAllMembersByLybrary();
        }

        private void btnMemberRegistration_Click(object sender, RoutedEventArgs e)
        {
            UcRegisterMember ucRegisterMember = new UcRegisterMember();
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucRegisterMember;
        }

        private void btnEditMember_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = dgvMembers.SelectedItem as Member;
            if (selectedMember != null)
            {
            UcEditMember ucEditMember = new UcEditMember(selectedMember);
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucEditMember;
            } else
            {
                MessageBox.Show("Odaberite člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = dgvMembers.SelectedItem as Member;
            if (selectedMember != null)
            {
                bool deleted = false;
                MessageBoxResult reuslt =  MessageBox.Show("Jeste li sigurni da želite izbrisati", "Upozorenje", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(reuslt == MessageBoxResult.OK)
                {
                    deleted = memberService.DeleteMember(selectedMember);
                }
                if (deleted)
                {
                    dgvMembers.ItemsSource = memberService.GetAllMembersByLybrary();
                }
            } else
            {
                MessageBox.Show("Odaberite člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string name = "", surname = "";
            string input = txtFilter.Text;
            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 1)
            {
                name = words[0];
                surname = words[0];
            } else
            {
                name = words[0];
                surname = words[1];
            }
            
            List<Member> filteredMembers = memberService.GetAllMembersByFilter(name,surname);
            dgvMembers.ItemsSource = filteredMembers;
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
            dgvMembers.ItemsSource = memberService.GetAllMembersByLybrary();
        }

        private void btnMembership_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = dgvMembers.SelectedItem as Member;
            if (selectedMember != null)
            {
                bool restored = memberService.RestoreMembership(selectedMember);
                if (restored)
                {
                    MessageBox.Show("Članstvo produljeno!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgvMembers.ItemsSource = memberService.GetAllMembersByLybrary();
                } else
                {
                    MessageBox.Show("Članstvo još nije isteklo!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else
            {
                MessageBox.Show("Odaberite člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
