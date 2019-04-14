using System;
using System.Windows.Data;
using System.Globalization;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Converter
{
    // 下記URLを参考
    // http://thinkami.hatenablog.com/entry/2014/10/27/063202
    public class MultiBindingConverter : IMultiValueConverter
    {
        public MultiBindingConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // MultiBindingを使うと、第1引数で配列として設定した順に渡される
            var cnt = values[0] == null ? 0 : (int)values[0];
            var displayName = values[1] == null ? "" : (string)values[1];

            return new object[] { cnt, displayName };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
