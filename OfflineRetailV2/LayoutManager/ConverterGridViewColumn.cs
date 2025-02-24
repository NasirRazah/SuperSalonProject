// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace OfflineRetailV2.LayoutManager
{
    public abstract class ConverterGridViewColumn : GridViewColumn, IValueConverter
    {
        // ---------------------------------------------------------------------- members
        private readonly Type bindingType;

        // ----------------------------------------------------------------------
        protected ConverterGridViewColumn(Type bindingType)
        {
            if (bindingType == null)
            {
                throw new ArgumentNullException("bindingType");
            }

            this.bindingType = bindingType;

            // binding
            Binding binding = new Binding();
            binding.Mode = BindingMode.OneWay;
            binding.Converter = this;
            DisplayMemberBinding = binding;
        } // ConverterGridViewColumn

        // ----------------------------------------------------------------------
        public Type BindingType
        {
            get { return bindingType; }
        } // BindingType

        // ----------------------------------------------------------------------
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!bindingType.IsInstanceOfType(value))
            {
                throw new InvalidOperationException();
            }
            return ConvertValue(value);
        }

        // ----------------------------------------------------------------------
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        // ----------------------------------------------------------------------
        protected abstract object ConvertValue(object value);

        // IValueConverter.Convert

        // IValueConverter.ConvertBack
    }
}