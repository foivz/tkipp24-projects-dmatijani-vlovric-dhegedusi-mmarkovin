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
using ZXing;

namespace PresentationLayer.EmployeePanels
{
    //Magdalena Markovinocić
    public partial class UcRegisterMember : UserControl
    {
        MemberService memberService;
        EmployeeService employeeService;
        public UcRegisterMember()
        {
            InitializeComponent();
            memberService = new MemberService();
            employeeService = new EmployeeService();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CheckOIBIsNumber();
            if (txtOIB.Text != "" && txtPassword.Password != "" && txtUsername.Text != "" && txtBarcode.Text != "")
            {
                Member newMember = MakeNewMember();
                bool checkedConstraints = CheckUniqueConstraints(newMember);
                if (checkedConstraints)
                {
                    memberService.AddNewMember(newMember);
                    (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcMemberManagment();
                }

            } else
            {
                MessageBox.Show("Obavezni podaci čnana nisu uneseni ili su krivo unešeni!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private Member MakeNewMember() 
        {
            int LibraryId = employeeService.GetEmployeeLibraryId(LoggedUser.Username);
            Member newMember = new Member()
            {
                name = (txtName.Text).Length <= 45 ? txtName.Text : (txtName.Text).Substring(0, 45),
                surname = (txtSurname.Text).Length <= 45 ? txtSurname.Text : (txtSurname.Text).Substring(0, 45),
                OIB = (txtOIB.Text).Length <= 11 ? txtOIB.Text : (txtOIB.Text).Substring(0, 11),
                username = (txtUsername.Text).Length <= 45 ? txtUsername.Text : (txtUsername.Text).Substring(0, 45),
                password = (txtPassword.Password).Length <= 45 ? txtPassword.Password : (txtPassword.Password).Substring(0, 45),
                membership_date = txtDate.SelectedDate,
                barcode_id = (txtBarcode.Text).Length <= 45 ? txtBarcode.Text : (txtBarcode.Text).Substring(0, 45),
                Library_id = LibraryId
            };
            return newMember;
        }
        private void CheckOIBIsNumber()
        {
            if (double.TryParse(txtOIB.Text, out _))
            {
                txtOIB.Text = txtOIB.Text;
            } else txtOIB.Text = "";
        }
        private bool CheckUniqueConstraints(Member member)
        {
            if (!IsUnique(() => memberService.CheckExistingUsername(member), "Korisničko ime već postoji!", clearTextBox: true))
            {
                return false;
            }

            if (!IsUnique(() => memberService.CheckBarcodeUnoque(member), "Barkod već postoji!"))
            {
                return false;
            }

            if (!IsUnique(() => memberService.CheckOibUnoque(member), "Oib već postoji!"))
            {
                return false;
            }

            return true;
        }

        private bool IsUnique(Func<bool> checkFunc, string errorMessage, bool clearTextBox = false)
        {
            if (checkFunc())
            {
                if (clearTextBox)
                {
                    txtUsername.Text = "";
                }

                MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void btnGenerateBarcode_Click(object sender, RoutedEventArgs e)
        {
            txtBarcode.Text = memberService.RandomCodeGenerator().ToString();

            BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128};
            System.Drawing.Bitmap barcodeBitmap = writer.Write(txtBarcode.Text);
            imgBarcode.Source = ConvertBitmapToImageSource(barcodeBitmap);
        }

        private ImageSource ConvertBitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
