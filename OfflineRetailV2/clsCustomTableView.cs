using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;

namespace OfflineRetailV2
{
    public class MyTableView : TableView
    {
        protected override DataViewBehavior CreateViewBehavior()
        {
            return new MyTableViewBehavior(this);
        }


    }
    class MyTableViewBehavior : GridTableViewBehavior
    {
        public MyTableViewBehavior(TableView view) : base(view) { }
        protected override GridViewInfo CreateViewInfo()
        {
            GridViewInfo viewInfo = base.CreateViewInfo();
            viewInfo.LayoutCalculatorFactory = new MyTableViewLayoutCalculatorFactory();
            return viewInfo;
        }
    }
    public class MyTableViewLayoutCalculatorFactory : GridTableViewLayoutCalculatorFactory
    {

        public override ColumnsLayoutCalculator CreateCalculator(GridViewInfo viewInfo, bool autoWidth)
        {
            return new MyAutoWidthColumnsLayoutCalculator(viewInfo);
        }
    }
    class MyAutoWidthColumnsLayoutCalculator : AutoWidthColumnsLayoutCalculator
    {
        public MyAutoWidthColumnsLayoutCalculator(GridViewInfo viewInfo) : base(viewInfo) { }
        protected override void CreateLayout()
        {
            base.CreateLayout();
            if (ViewInfo.VisibleColumns.Count == 0) return;
            ColumnBase lastColumn = ViewInfo.VisibleColumns[ViewInfo.VisibleColumns.Count - 1];
            LayoutAssigner.Default.SetWidth(lastColumn, lastColumn.ActualHeaderWidth + ViewInfo.VerticalScrollBarWidth);
            ViewInfo.TableView.FixedNoneContentWidth += ViewInfo.VerticalScrollBarWidth;
        }
    }
}
