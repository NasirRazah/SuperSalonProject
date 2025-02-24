// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.LayoutManager
{
    public abstract class LayoutColumn
    {
        // ----------------------------------------------------------------------
        protected static double? GetColumnWidth(GridViewColumn column, DependencyProperty dp)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            object value = column.ReadLocalValue(dp);
            if (value != null && value.GetType() == dp.PropertyType)
            {
                return (double)value;
            }

            return null;
        }

        // ----------------------------------------------------------------------
        protected static bool HasPropertyValue(GridViewColumn column, DependencyProperty dp)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            object value = column.ReadLocalValue(dp);
            if (value != null && value.GetType() == dp.PropertyType)
            {
                return true;
            }

            return false;
        } // HasPropertyValue

        // GetColumnWidth
    }
}