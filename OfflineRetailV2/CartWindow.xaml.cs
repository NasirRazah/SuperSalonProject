using System;
using System.Data;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    public partial class CartWindow : ThemedWindow
    {
        public CartWindow(DataTable cartTable)
        {
            InitializeComponent();
            if (cartTable == null)
            {
                throw new ArgumentNullException(nameof(cartTable), "cartTable cannot be null");
            }
            CartGrid.ItemsSource = cartTable.DefaultView;
        }
    }
}