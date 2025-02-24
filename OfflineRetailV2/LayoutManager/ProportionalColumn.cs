// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.LayoutManager
{
    public sealed class ProportionalColumn : LayoutColumn
    {
        // ----------------------------------------------------------------------
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached(
                "Width",
                typeof(double),
                typeof(ProportionalColumn));

        // ----------------------------------------------------------------------
        private ProportionalColumn()
        {
        } // ProportionalColumn

        // ----------------------------------------------------------------------
        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double width)
        {
            SetWidth(gridViewColumn, width);
            return gridViewColumn;
        }

        // ----------------------------------------------------------------------
        public static double? GetProportionalWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, WidthProperty);
        }

        // ----------------------------------------------------------------------
        public static double GetWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(WidthProperty);
        } // GetWidth

        // ----------------------------------------------------------------------
        public static bool IsProportionalColumn(GridViewColumn column)
        {
            if (column == null)
            {
                return false;
            }
            return HasPropertyValue(column, WidthProperty);
        }

        // ----------------------------------------------------------------------
        public static void SetWidth(DependencyObject obj, double width)
        {
            obj.SetValue(WidthProperty, width);
        } // SetWidth

        // IsProportionalColumn

        // GetProportionalWidth

        // ApplyWidth
    }
}