/*
 purpose : Custom Lookup Edit class with advanced sort and search facility 
 
 */ 

using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.ComponentModel;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace pos
{
    [UserRepositoryItem("RegisterSortAndSearchLookUpEdit")]
    public class RepositoryItemSortAndSearchLookUpEdit : RepositoryItemLookUpEdit
    {
        static RepositoryItemSortAndSearchLookUpEdit() { RegisterSortAndSearchLookUpEdit(); }

        public RepositoryItemSortAndSearchLookUpEdit()
            : base()
        {
            HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.Sorting;
        }

        public const string SortAndSearchLookUpEditName = "SortAndSearchLookUpEdit";

        public override string EditorTypeName { get { return SortAndSearchLookUpEditName; } }

        public static void RegisterSortAndSearchLookUpEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(SortAndSearchLookUpEditName,
              typeof(SortAndSearchLookUpEdit), typeof(RepositoryItemSortAndSearchLookUpEdit),
              typeof(LookUpEditViewInfo), new ButtonEditPainter(), true, null));
        }
    }

    public class SortAndSearchLookUpEdit : LookUpEdit
    {
        static SortAndSearchLookUpEdit() { RepositoryItemSortAndSearchLookUpEdit.RegisterSortAndSearchLookUpEdit(); }

        public override string EditorTypeName { get { return RepositoryItemSortAndSearchLookUpEdit.SortAndSearchLookUpEditName; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemSortAndSearchLookUpEdit Properties { get { return base.Properties as RepositoryItemSortAndSearchLookUpEdit; } }

        protected override PopupBaseForm CreatePopupForm() { return new SortAndSearchPopupLookUpEditForm(this); }
    }

    public class SortAndSearchPopupLookUpEditForm : PopupLookUpEditForm
    {
        public SortAndSearchPopupLookUpEditForm(SortAndSearchLookUpEdit ownerEdit) : base(ownerEdit) { }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            LookUpPopupHitTest _pressInfo = ViewInfo.GetHitTest(new Point(e.X, e.Y));
            if (_pressInfo.HitType == LookUpPopupHitType.Header)
            {
                if (SelectedIndex > 0)
                    SelectedIndex--;
                else
                    if (Filter.RowCount > 1)
                        SelectedIndex++;
                ViewInfo.AutoSearchHeaderIndex = _pressInfo.Index;
                LayoutChanged();
            }
            base.OnMouseDown(e);
        }
    }
}

