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
    public partial class UcEditMember : UserControl
    {
        Member editMember;
        MemberService memberService;
        public UcEditMember(Member member)
        {
            InitializeComponent();
            editMember = member;
            memberService = new MemberService();

            txtName.Text = member.name;
            txtSurname.Text = member.surname;
            txtOIB.Text = member.OIB;
            txtUsername.Text = member.username;
            txtPassword.Password = member.password;
            txtBarcode.Text = member.barcode_id;
            txtDate.Text = (member.membership_date).ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            editMember.name = (txtName.Text).Length <= 45 ? txtName.Text : (txtName.Text).Substring(0, 45);
            editMember.surname = (txtSurname.Text).Length <= 45 ? txtSurname.Text : (txtSurname.Text).Substring(0, 45);
            editMember.password = (txtPassword.Password).Length <= 45 ? txtPassword.Password : (txtPassword.Password).Substring(0, 45);

            bool edited = memberService.UpdateMember(editMember);
            if (edited)
            {
                (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
            }
        }
    }
}
