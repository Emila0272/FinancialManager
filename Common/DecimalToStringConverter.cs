using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FinancialManager.Common
{
    public class DecimalToStringConverter : IValueConverter
    {
               public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is decimal decimalValue)
            {
                return decimalValue.ToString("0.00");  
            }
            return string.Empty; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value as string, out decimal result))
            {
                return result;  
            }
            return 0m;  
        }
    }
}
