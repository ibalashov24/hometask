using System;
using System.Windows.Data;

namespace SimpleFTPClientGUI
{
    /// <summary>
    /// Converter which substracts 2 (two)
    /// </summary>
    public class MinusTwo : IValueConverter
    {
        /// <summary>
        /// Substracts 2
        /// </summary>
        /// <param name="value">Value to substract from</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Some object</param>
        /// <param name="culture">Culture</param>
        /// <returns>Value - 2</returns>
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture)
        {
            return (int)value - 2;
        }

        /// <summary>
        /// Substracts 2
        /// </summary>
        /// <param name="value">Value to substract from</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Some object</param>
        /// <param name="culture">Culture</param>
        /// <returns>Value - 2</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            return (int)value - 2;
        }
    }
}
