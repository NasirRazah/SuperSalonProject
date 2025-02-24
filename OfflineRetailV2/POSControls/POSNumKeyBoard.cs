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
    public partial class POSNumKeyBoard : DevExpress.XtraEditors.XtraUserControl
    {
        public POSNumKeyBoard()
        {
            InitializeComponent();
        }

        public void RearrangeForCalculatorStyle(bool bCalculatorStyle)
        {
            if (bCalculatorStyle)
            {
                this.SuspendLayout();

                btn1.Height = btn1.Width = btn2.Height = btn2.Width = btn3.Height = btn3.Width = btn4.Height = btn4.Width
                    = btn5.Height = btn5.Width = btn6.Height = btn6.Width = btn7.Height = btn7.Width = btn8.Height = btn8.Width
                    = btn9.Height = btn9.Width = btnMinus.Height = btnMinus.Width = btnBack.Height = btnBack.Width = btnPoint.Height = btnPoint.Width = btnClear.Height = btnClear.Width = 64;
                btn0.Height = 64; btn0.Width = 196;


                int defaultLeft = 0;
                int defaultTop = 0;

                btn7.Left = defaultLeft + 3;
                btn7.Top = defaultTop + 3;

                btn8.Left = defaultLeft + 3 + 66;
                btn8.Top = defaultTop + 3;

                btn9.Left = defaultLeft + 3 + 132;
                btn9.Top = defaultTop + 3;

                btnMinus.Left = defaultLeft + 3 + 198;
                btnMinus.Top = defaultTop + 3;

                btn4.Left = defaultLeft + 3;
                btn4.Top = defaultTop + 3 + 66;

                btn5.Left = defaultLeft + 3 + 66;
                btn5.Top = defaultTop + 3 + 66;

                btn6.Left = defaultLeft + 3 + 132;
                btn6.Top = defaultTop + 3 + 66;

                btnClear.Left = defaultLeft + 3 + 198;
                btnClear.Top = defaultTop + 3 + 66;

                btn1.Left = defaultLeft + 3;
                btn1.Top = defaultTop + 3 + 132;

                btn2.Left = defaultLeft + 3 + 66;
                btn2.Top = defaultTop + 3 + 132;

                btn3.Left = defaultLeft + 3 + 132;
                btn3.Top = defaultTop + 3 + 132;

                btnBack.Left = defaultLeft + 3 + 198;
                btnBack.Top = defaultTop + 3 + 132;


                btn0.Left = defaultLeft + 3;
                btn0.Top = defaultTop + 3 + 198;

                btnPoint.Left = defaultLeft + 3 + 198;
                btnPoint.Top = defaultTop + 3 + 198;

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

            if (FindForm().ActiveControl.AccessibleName == "floatctrl")
            {
                DocManager.Controls.DocFloatEdit ctrl = new DocManager.Controls.DocFloatEdit();
                //Todo: ctrl = (((FindForm().ActiveControl as TextBoxBase) as DevExpress.XtraEditors.TextBoxMaskBox).OwnerEdit as DocManager.Controls.DocFloatEdit);

                TextBoxBase tempC = FindForm().ActiveControl as TextBoxBase;

                string PrevValue = tempC.Text;

                

                if (tempC.SelectionLength > 0)
                {
                    int intStart = tempC.SelectionStart;
                    tempC.Text = tempC.Text.Remove(tempC.SelectionStart, tempC.SelectionLength);
                    tempC.Text = tempC.Text.Insert(intStart, Ch);
                    tempC.SelectionStart = intStart + 1;
                }
                else
                {
                    tempC.SelectionLength = 0;
                    int intStart = tempC.SelectionStart;
                    tempC.Text = tempC.Text.Insert(tempC.SelectionStart, Ch);
                    tempC.SelectionStart = intStart + 1;
                }

                if (Ch == "-")
                {
                    if (PrevValue == "-")
                    {
                        tempC.Text = PrevValue;
                        return;
                    }
                    else
                    {
                        int countminus = GetHowManyTimeOccurenceCharInString(tempC.Text, '-');
                        if (countminus > 1)
                        {
                            tempC.Text = PrevValue;
                            return;
                        }
                        else
                        {
                            if (tempC.Text.StartsWith(".") || tempC.Text.StartsWith("0") || tempC.Text.StartsWith("1") || tempC.Text.StartsWith("2")
                                || tempC.Text.StartsWith("3") || tempC.Text.StartsWith("4") || tempC.Text.StartsWith("5") || tempC.Text.StartsWith("6")
                                || tempC.Text.StartsWith("7") || tempC.Text.StartsWith("8") || tempC.Text.StartsWith("9"))
                            {
                                tempC.Text = PrevValue;
                                return;
                            }
                        }
                    }
                }

                if (ctrl.Decimals > 0)
                {
                    int countdecimal = GetHowManyTimeOccurenceCharInString(tempC.Text, '.');
                    if (countdecimal > 1)
                    {
                        tempC.Text = PrevValue;
                        return;
                    }
                    if (tempC.Text != ".")
                    {
                        try
                        {
                            decimal argument = Convert.ToDecimal(tempC.Text);
                            int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                            if (count > ctrl.Decimals)
                            {
                                tempC.Text = PrevValue;
                                return;
                            }
                        }
                        catch
                        {

                        }
                    }
                }

            }

            else
            {
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
            }

            /*if (this.FindForm().ActiveControl is DocManager.Controls.DocFloatEdit)
                (this.FindForm().ActiveControl as DocManager.Controls.DocFloatEdit).Value = Convert.ToDouble((FindForm().ActiveControl as TextBoxBase).Text);*/
        }

        private int GetHowManyTimeOccurenceCharInString(string text, char c)
        {
            int count = 0;
            foreach (char ch in text)
            {
                if (ch.Equals(c))
                {
                    count++;
                }

            }
            return count;
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

        private void btnLeft_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{Left}");

            /*if (this.FindForm().ActiveControl == null) return;
            if (this.FindForm().ActiveControl != null)
            {
                bool bActivo = this.FindForm().SelectNextControl(this.FindForm().ActiveControl, true, false, true, true);
                this.FindForm().ActiveControl.Focus();
            }*/
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{Right}");
        }
    }
}
