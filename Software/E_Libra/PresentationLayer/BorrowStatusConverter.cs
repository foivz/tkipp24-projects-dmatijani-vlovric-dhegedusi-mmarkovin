using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PresentationLayer {
    // David Matijanić
    public class BorrowStatusConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int number) {
                switch (number) {
                    case 0:
                        return "Na čekanju";
                    case 1:
                        return "Posuđena";
                    case 2:
                        return "Posuđena, no kasni";
                    case 3:
                        return "Vraćena";
                    default:
                        return value.ToString();
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
