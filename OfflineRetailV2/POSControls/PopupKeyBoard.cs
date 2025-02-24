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
    public partial class PopupKeyBoard : DevExpress.XtraEditors.XtraUserControl
    {
        private XtraForm fcalledform;
        private bool bDirectKey = false;
        private TextEdit txtEdit;
        public PopupKeyBoard()
        {
            InitializeComponent();
        }

        public bool DirectKey
        {
            get { return bDirectKey; }
            set { bDirectKey = value; }
        }

        public TextEdit EditControl
        {
            get { return txtEdit; }
            set { txtEdit = value; }
        }

        public XtraForm CalledForm
        {
            get { return fcalledform; }
            set { fcalledform = value; }
        }

        private void WriteCharacter(string Ch)
        {

            if (!bDirectKey)
            {
                if (fcalledform.ActiveControl == null) return;
                if (!(fcalledform.ActiveControl is TextBoxBase)) return;
                if ((fcalledform.ActiveControl as TextBoxBase).ReadOnly) return;
                txtEdit.Text = (fcalledform.ActiveControl as TextBoxBase).Text;

                txtEdit.Focus();
                if (txtEdit.Text.Length > 0)
                {
                    txtEdit.SelectionLength = 0;
                    txtEdit.SelectionStart = txtEdit.Text.Length;
                }
                if (fcalledform.Name == "frmPOSN")
                {
                    if (Ch == "{ENTER}")
                    {
                        return;
                    }
                }
                else
                {
                    if (Ch == "{ENTER}")
                    {
                        if (fcalledform == null) return;

                        if (fcalledform.ActiveControl != null)
                        {
                            bool bActivo = fcalledform.SelectNextControl(fcalledform.ActiveControl, true, false, true, true);
                            txtEdit.Text = "";
                        }
                    }
                }

                if (fcalledform.ActiveControl == null) return;
                if (!(fcalledform.ActiveControl is TextBoxBase)) return;
                if ((fcalledform.ActiveControl as TextBoxBase).ReadOnly) return;

                if (((fcalledform.ActiveControl as TextBoxBase).MaxLength == (fcalledform.ActiveControl as TextBoxBase).Text.Length) && ((fcalledform.ActiveControl as TextBoxBase).MaxLength > 0))
                {
                    if (Ch != "{BACKSPACE}") return;
                }

                if (fcalledform.ActiveControl is TextBoxBase)
                {
                    if (fcalledform.ActiveControl.AccessibleName != null)
                    {
                        if (fcalledform.ActiveControl.AccessibleName.ToString() == "floatctrl")
                        {
                            if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                (Ch == "0") || (Ch == ".") || (Ch == "{BACKSPACE}"))) return;
                            if (Ch == ".")
                            {
                                if (fcalledform.ActiveControl.Text.Length > 0)
                                {
                                    int n = fcalledform.ActiveControl.Text.IndexOf(".");
                                    if (n > 0)
                                    {
                                        return;
                                    }

                                }
                            }
                        }

                        if (fcalledform.ActiveControl.AccessibleName.ToString() == "intctrl")
                        {
                            if (!((Ch == "1") || (Ch == "2") || (Ch == "3") ||
                                (Ch == "4") || (Ch == "5") || (Ch == "6") ||
                                (Ch == "7") || (Ch == "8") || (Ch == "9") ||
                                (Ch == "0") || (Ch == "{BACKSPACE}"))) return;
                        }

                    }
                }

                SendKeys.Send(Ch);
            }
            else
            {
                SendKeys.Send(Ch);
            }
            
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            WriteCharacter((sender as SimpleButton).Tag.ToString());
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            if ((chkCapsLock.Checked) || (chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter((sender as SimpleButton).Text.ToString());
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter((sender as SimpleButton).Text.ToString().ToLower());
            }
        }

        private void chkShiftL_CheckedChanged(object sender, EventArgs e)
        {
            chkShiftR.Checked = chkShiftL.Checked;
            if (chkShiftL.Checked)
            {
                chkShiftL.Appearance.BackColor = Color.DimGray;
                chkShiftR.Appearance.BackColor = Color.DimGray;
            }
            else
            {
                chkShiftL.Appearance.BackColor = Color.Black;
                chkShiftR.Appearance.BackColor = Color.Black;
            }
        }

        private void chkShiftR_CheckedChanged(object sender, EventArgs e)
        {
            chkShiftL.Checked = chkShiftR.Checked;
            if (chkShiftL.Checked)
            {
                chkShiftL.Appearance.BackColor = Color.DimGray;
                chkShiftR.Appearance.BackColor = Color.DimGray;
            }
            else
            {
                chkShiftL.Appearance.BackColor = Color.Black;
                chkShiftR.Appearance.BackColor = Color.Black;
            }
        }

        private void chkCapsLock_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCapsLock.Checked)
            {
                chkCapsLock.Appearance.BackColor = Color.DimGray;
            }
            else
            {
                chkCapsLock.Appearance.BackColor = Color.Black;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            WriteCharacter("{ENTER}");
        }

        private void btnBkSp_Click(object sender, EventArgs e)
        {
            WriteCharacter("{BACKSPACE}");
        }

        private void btnSp_Click(object sender, EventArgs e)
        {
            WriteCharacter(" ");
        }

        private void btn1_Click_1(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("!");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("1");
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("@");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("2");
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("#");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("3");
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("$");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("4");
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("%");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("5");
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("^");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("6");
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("&");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("7");
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("*");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("8");
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("(");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("9");
            }
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter(")");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("0");
            }
        }

        private void btnchr2_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("_");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("-");
            }
        }

        private void btnchr3_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("+");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("=");
            }
        }

        private void btnchr1_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("~");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("`");
            }
        }

        private void btnchr4_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("{");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("[");
            }
        }

        private void btnchr5_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("}");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("]");
            }
        }

        private void btnchr6_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter(":");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter(";");
            }
        }

        private void btnchr7_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("\"");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("'");
            }
        }

        private void btnchr8_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("<");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter(",");
            }
        }

        private void btnchr9_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter(">");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter(".");
            }
        }

        private void btnchr10_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("?");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("/");
            }
        }
    }
}
