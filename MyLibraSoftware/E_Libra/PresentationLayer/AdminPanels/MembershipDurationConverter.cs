using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PresentationLayer.AdminPanels {
    // David Matijanić
    public class MembershipDurationConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateTime dateTime) {
                int membershipDuration = ((dateTime - new DateTime(2024, 1, 1)).Days + 1);
                string suffix = "dana";
                if (membershipDuration % 10 == 1) {
                    if (membershipDuration % 100 != 11) {
                        suffix = "dan";
                    }
                }
                return membershipDuration.ToString() + " " + suffix;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
