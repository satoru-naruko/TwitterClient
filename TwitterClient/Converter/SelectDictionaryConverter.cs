using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Model;

namespace TwitterClient.Converter
{
    public class SelectDictionaryConverter : System.Windows.Data.IMultiValueConverter
    {
        public SelectDictionaryConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var collection = (Dictionary<String, ObservableCollection<Tweet>>)values[0];

            return collection[key: values[1].ToString()];

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
