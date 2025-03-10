﻿// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.LayoutManager
{
    public sealed class RangeColumn : LayoutColumn
    {
        // ----------------------------------------------------------------------
        public static readonly DependencyProperty IsFillColumnProperty =
            DependencyProperty.RegisterAttached(
                "IsFillColumn",
                typeof(bool),
                typeof(RangeColumn));

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.RegisterAttached(
                "MaxWidth",
                typeof(double),
                typeof(RangeColumn));

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty MinWidthProperty =
            DependencyProperty.RegisterAttached(
                "MinWidth",
                typeof(double),
                typeof(RangeColumn));

        // ----------------------------------------------------------------------
        private RangeColumn()
        {
        } // RangeColumn

        // ----------------------------------------------------------------------
        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double minWidth,
            double width, double maxWidth)
        {
            return ApplyWidth(gridViewColumn, minWidth, width, maxWidth, false);
        }

        // ----------------------------------------------------------------------
        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double minWidth,
            double width, double maxWidth, bool isFillColumn)
        {
            SetMinWidth(gridViewColumn, minWidth);
            gridViewColumn.Width = width;
            SetMaxWidth(gridViewColumn, maxWidth);
            SetIsFillColumn(gridViewColumn, isFillColumn);
            return gridViewColumn;
        }

        // ----------------------------------------------------------------------
        public static bool GetIsFillColumn(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFillColumnProperty);
        }

        // ----------------------------------------------------------------------
        public static double GetMaxWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MaxWidthProperty);
        }

        // ----------------------------------------------------------------------
        public static double GetMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MinWidthProperty);
        } // GetMinWidth

        // ----------------------------------------------------------------------
        public static bool? GetRangeIsFillColumn(GridViewColumn column)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            object value = column.ReadLocalValue(IsFillColumnProperty);
            if (value != null && value.GetType() == IsFillColumnProperty.PropertyType)
            {
                return (bool)value;
            }

            return null;
        }

        // ----------------------------------------------------------------------
        public static double? GetRangeMaxWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, MaxWidthProperty);
        }

        // ----------------------------------------------------------------------
        public static double? GetRangeMinWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, MinWidthProperty);
        }

        // ----------------------------------------------------------------------
        public static bool IsRangeColumn(GridViewColumn column)
        {
            if (column == null)
            {
                return false;
            }
            return
                HasPropertyValue(column, MinWidthProperty) ||
                HasPropertyValue(column, MaxWidthProperty) ||
                HasPropertyValue(column, IsFillColumnProperty);
        }

        // ----------------------------------------------------------------------
        public static void SetIsFillColumn(DependencyObject obj, bool isFillColumn)
        {
            obj.SetValue(IsFillColumnProperty, isFillColumn);
        }

        // ----------------------------------------------------------------------
        public static void SetMaxWidth(DependencyObject obj, double maxWidth)
        {
            obj.SetValue(MaxWidthProperty, maxWidth);
        }

        // ----------------------------------------------------------------------
        public static void SetMinWidth(DependencyObject obj, double minWidth)
        {
            obj.SetValue(MinWidthProperty, minWidth);
        } // SetMinWidth

        // GetMaxWidth

        // SetMaxWidth

        // GetIsFillColumn

        // SetIsFillColumn

        // IsRangeColumn

        // GetRangeMinWidth

        // GetRangeMaxWidth

        // GetRangeIsFillColumn

        // ApplyWidth

        // ApplyWidth
    }
}