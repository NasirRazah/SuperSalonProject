using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace POSControls
{
    public partial class POSNumKeyBoardEnter : UserControl
    {
        public POSNumKeyBoardEnter()
        {
            InitializeComponent();
        }

        public void RearrangeForCalculatorStyle(bool bCalculatorStyle)
        {
            if (bCalculatorStyle)
            {
                this.SuspendLayout();
                btn3.Location = new Point(135, 135);
                btn2.Location = new Point(69, 135);
                btn1.Location = new Point(3, 135);

                
                btn9.Location = new Point(135, 3);
                btn8.Location = new Point(69, 3);
                btn7.Location = new Point(3, 3);
                this.ResumeLayout();
            }
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

        private void btn9_Click(object sender, EventArgs e)
        {
            WriteCharacter((sender as SimpleButton).Tag.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.FindForm().ActiveControl == null) return;
            if (!(this.FindForm().ActiveControl is TextBoxBase)) return;
            if ((FindForm().ActiveControl as TextBoxBase).ReadOnly) return;
            (FindForm().ActiveControl as TextBoxBase).Text = "";
            (FindForm().ActiveControl as TextBoxBase).Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
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
    }
}
