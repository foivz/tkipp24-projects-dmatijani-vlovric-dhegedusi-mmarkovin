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
    //Viktor Lovrić
    public partial class WinAcceptDecline : Window
    {
        public bool UserClickedAccept { get; private set; }
        public WinAcceptDecline(string text)
        {
            InitializeComponent();
            tblText.Text = text;
        }

        private void btnDecline_Click(object sender, RoutedEventArgs e)
        {
            UserClickedAccept = false;
            Close();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            UserClickedAccept = true;
            Close();
        }
    }
}
