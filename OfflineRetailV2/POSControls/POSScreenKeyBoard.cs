using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace POSControls
{
    public partial class POSScreenKeyBoard : UserControl
    {
        public POSScreenKeyBoard()
        {
            InitializeComponent();
        }

        private void WriteCharacter(string Ch)
        {
            if (this.FindForm().ActiveControl == null) return;
            if (!(this.FindForm().ActiveControl is TextBoxBase)) return;
            if ((FindForm().ActiveControl as TextBoxBase).ReadOnly) return;
            if ((FindForm().ActiveControl as TextBoxBase).MaxLength > 0)
                if ((FindForm().ActiveControl as TextBoxBase).Text.Length >= (FindForm().ActiveControl as TextBoxBase).MaxLength)
                    return;


            if ((FindForm().ActiveControl as TextBoxBase).SelectionLength > 0)
            {
                int intStart = (FindForm().ActiveControl as TextBoxBase).SelectionStart;
                (FindForm().ActiveControl as TextBoxBase).Text = (FindForm().ActiveControl as TextBoxBase).Text.Remove((FindForm().ActiveControl as TextBoxBase).SelectionStart, (FindForm().ActiveControl as TextBoxBase).SelectionLength);
                (FindForm().ActiveControl as TextBoxBase).Text = (FindForm().ActiveControl as TextBoxBase).Text.Insert(intStart, Ch);
                (FindForm().ActiveControl as TextBoxBase).SelectionStart = intStart + 1;
            }
            else
            {
                (FindForm().ActiveControl as TextBoxBase).SelectionLength = 0;
                int intStart = (FindForm().ActiveControl as TextBoxBase).SelectionStart;
                (FindForm().ActiveControl as TextBoxBase).Text = (FindForm().ActiveControl as TextBoxBase).Text.Insert((FindForm().ActiveControl as TextBoxBase).SelectionStart, Ch);
                (FindForm().ActiveControl as TextBoxBase).SelectionStart = intStart + 1;
            }

            /*if (this.FindForm().ActiveControl is DocManager.Controls.DocFloatEdit)
                (this.FindForm().ActiveControl as DocManager.Controls.DocFloatEdit).Value = Convert.ToDouble((FindForm().ActiveControl as TextBoxBase).Text);*/
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (this.FindForm().ActiveControl == null) return;
            if (this.FindForm().ActiveControl != null)
            {
                bool bActivo = this.FindForm().SelectNextControl(this.FindForm().ActiveControl, true, false, true, true);
                this.FindForm().ActiveControl.Focus();
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            WriteCharacter((sender as SimpleButton).Tag.ToString());
        }
    }
}
