using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace SnippetManager
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double windowHeight)
            {
                // Subtract a fixed value (e.g., 150) to account for other UI elements, margins, etc.
                double adjustedHeight = windowHeight - 150;
                return Math.Max(adjustedHeight, 0); // Ensures height is not negative
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
