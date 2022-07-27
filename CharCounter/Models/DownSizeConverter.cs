namespace CharCounter.Models
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// 入力された値を僅かに小さくして返すコンバーター
    /// ListViewItem の幅を ListView.ActualWidth とバインディングした際、
    /// ListViewItem の幅が ListView と同じになるため、数ピクセルだけ ListView の内部からはみ出る。
    /// これにより、横に数ピクセル動かせるだけの無意味なスクロールバーが表示される
    /// これを防ぐために ListViewItem.Width にバインドする際にこのコンバーターを使用する。
    /// </summary>
    public class DownSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - 4.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
