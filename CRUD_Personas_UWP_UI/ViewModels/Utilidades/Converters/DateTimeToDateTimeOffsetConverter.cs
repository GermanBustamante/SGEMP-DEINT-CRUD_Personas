using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace CRUD_Personas_UWP_UI.ViewModels.Utilidades.Converters
{
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        #region IValueConverter Members

        // Define the Convert method to change a DateTime object to 
        // a DateTimeOffSet
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // The value parameter is the data from the source object.
            DateTime utcTime1 = (DateTime)value;
            utcTime1 = DateTime.SpecifyKind(utcTime1, DateTimeKind.Utc);
            DateTimeOffset utcTime2 = utcTime1;
            // Return the month value to pass to the target.
            return utcTime2;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            // The value parameter is the data from the source object.
            DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
            DateTime thisdate = dateTimeOffset.DateTime;
            // Return the month value to pass to the target.
            return thisdate;
        }

        #endregion
    }
}
