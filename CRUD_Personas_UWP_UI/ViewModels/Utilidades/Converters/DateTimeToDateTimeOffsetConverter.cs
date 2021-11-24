using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Personas_UWP_UI.ViewModels.Utilidades.Converters
{
    public class DateTimeToDateTimeOffsetConverter
    {
        #region IValueConverter Members

        // Define the Convert method to change a DateTime object to 
        // a month string.
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // The value parameter is the data from the source object.
            DateTime thisdate = (DateTime)value;
            int monthnum = thisdate.Month;
            string month;
            switch (monthnum)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                default:
                    month = "Month not found";
                    break;
            }

            // Return the month value to pass to the target.
            return month;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
