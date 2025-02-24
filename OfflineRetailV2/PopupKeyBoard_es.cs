/*
 purpose :  OnScreen Spanish Keyboard 
 USER CLASS : OnScreen Spanish Keyboard 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using OfflineRetailV2.Data;

namespace pos
{
    public partial class PopupKeyBoard_es : DevExpress.XtraEditors.XtraUserControl
    {
        private XtraForm fcalledform;
        private bool bDirectKey = false;
        private TextEdit txtEdit;
        private bool bAlphaKyBrd;

        public PopupKeyBoard_es()
        {
            InitializeComponent();
        }

        public bool AlphaKyBrd
        {
            get { return bAlphaKyBrd; }
            set { bAlphaKyBrd = value; ChangeKeyBoardLayOut(); }
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

        private void ChangeKeyBoardLayOut()
        {
            if (bAlphaKyBrd)
            {
                btnA.Location = new Point(43, 54);
                btnB.Location = new Point(99, 54);
                btnC.Location = new Point(155, 54);
                btnD.Location = new Point(211, 54);
                btnE.Location = new Point(267, 54);
                btnF.Location = new Point(323, 54);
                btnG.Location = new Point(379, 54);
                btnH.Location = new Point(435, 54);
                btnI.Location = new Point(491, 54);
                btnJ.Location = new Point(547, 54); 

                btnK.Location = new Point(86, 109);
                btnL.Location = new Point(142, 109);
                btnM.Location = new Point(198, 109);
                btnN.Location = new Point(254, 109);
                btnO.Location = new Point(310, 109);
                btnP.Location = new Point(366, 109);
                btnQ.Location = new Point(422, 109);
                btnR.Location = new Point(478, 109);
                btnS.Location = new Point(534, 109);

                btnT.Location = new Point(155, 164);
                btnU.Location = new Point(211, 164);
                btnV.Location = new Point(267, 164);
                btnW.Location = new Point(323, 164);
                btnX.Location = new Point(379, 164);
                btnY.Location = new Point(435, 164);
                btnZ.Location = new Point(491, 164);
            }
            else
            {
                btnQ.Location = new Point(43, 54);
                btnW.Location = new Point(99, 54);
                btnE.Location = new Point(155, 54);
                btnR.Location = new Point(211, 54);
                btnT.Location = new Point(267, 54);
                btnY.Location = new Point(323, 54);
                btnU.Location = new Point(379, 54);
                btnI.Location = new Point(435, 54);
                btnO.Location = new Point(491, 54);
                btnP.Location = new Point(547, 54); 

                btnA.Location = new Point(86, 109);
                btnS.Location = new Point(142, 109);
                btnD.Location = new Point(198, 109);
                btnF.Location = new Point(254, 109);
                btnG.Location = new Point(310, 109);
                btnH.Location = new Point(366, 109);
                btnJ.Location = new Point(422, 109);
                btnK.Location = new Point(478, 109);
                btnL.Location = new Point(534, 109);

                btnZ.Location = new Point(155, 164);
                btnX.Location = new Point(211, 164);
                btnC.Location = new Point(267, 164);
                btnV.Location = new Point(323, 164);
                btnB.Location = new Point(379, 164);
                btnN.Location = new Point(435, 164);
                btnM.Location = new Point(491, 164);
            }
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
                            try
                            {
                                if (((fcalledform.ActiveControl as TextBoxBase) as TextBoxMaskBox).OwnerEdit.EditorTypeName == "MemoEdit")
                                {
                                    ((fcalledform.ActiveControl as TextBoxBase) as TextBoxMaskBox).OwnerEdit.Text += "\r\n";
                                }
                                else
                                {
                                    bool bActivo = fcalledform.SelectNextControl(fcalledform.ActiveControl, true, false, true, true);
                                    txtEdit.Text = "";
                                }
                            }
                            catch
                            {
                                bool bActivo = fcalledform.SelectNextControl(fcalledform.ActiveControl, true, false, true, true);
                                txtEdit.Text = "";
                            }
                            
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
                //Todo: if (fcalledform.Name == "frmPOSN")
                //{
                //    (fcalledform as frmPOSN).DelegateKey(Ch);
                //}
                //if (fcalledform.Name == "frmSCALE")
                //{
                //    (fcalledform as frmSCALE).DelegateKey(Ch);
                //}
                //if (fcalledform.Name == "frmPOSRepairInfoDlg")
                //{
                //    (fcalledform as frmPOSRepairInfoDlg).DelegateKey(Ch);
                //}
                //if (fcalledform.Name == "frmPOSRepairItemInfoDlg")
                //{
                //    (fcalledform as frmPOSRepairItemInfoDlg).DelegateKey(Ch);
                //}
                //if (fcalledform.Name == "frmPOSApptBookDlg")
                //{
                //    (fcalledform as frmPOSApptBookDlg).DelegateKey(Ch);
                //}
                //if (fcalledform.Name == "frmOrderDlg")
                //{
                //    (fcalledform as frmOrderDlg).DelegateKey(Ch);
                //}
                
            }
            
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            WriteCharacter((sender as SimpleButton).Tag.ToString());
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            WriteCharacter((sender as SimpleButton).Text.ToString());

            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
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

            if (chkCapsLock.Checked)
            {
                if ((chkShiftL.Checked) || (chkShiftR.Checked))
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";

                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";

                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";

                    btnNn.Text = "ñ";
                    btnchr7.Text = "ç";
                }
                else
                {
                    btnA.Text = "A";
                    btnB.Text = "B";
                    btnC.Text = "C";
                    btnD.Text = "D";
                    btnE.Text = "E";
                    btnF.Text = "F";
                    btnG.Text = "G";
                    btnH.Text = "H";
                    btnI.Text = "I";
                    btnJ.Text = "J";

                    btnK.Text = "K";
                    btnL.Text = "L";
                    btnM.Text = "M";
                    btnN.Text = "N";
                    btnO.Text = "O";
                    btnP.Text = "P";
                    btnQ.Text = "Q";
                    btnR.Text = "R";
                    btnS.Text = "S";

                    btnT.Text = "T";
                    btnU.Text = "U";
                    btnV.Text = "V";
                    btnW.Text = "W";
                    btnX.Text = "X";
                    btnY.Text = "Y";
                    btnZ.Text = "Z";
                    btnNn.Text = "Ñ";
                    btnchr7.Text = "Ç";
                }
            }
            else
            { 
               
                if ((chkShiftL.Checked) || (chkShiftR.Checked))
                {
                    btnA.Text = "A";
                    btnB.Text = "B";
                    btnC.Text = "C";
                    btnD.Text = "D";
                    btnE.Text = "E";
                    btnF.Text = "F";
                    btnG.Text = "G";
                    btnH.Text = "H";
                    btnI.Text = "I";
                    btnJ.Text = "J";

                    btnK.Text = "K";
                    btnL.Text = "L";
                    btnM.Text = "M";
                    btnN.Text = "N";
                    btnO.Text = "O";
                    btnP.Text = "P";
                    btnQ.Text = "Q";
                    btnR.Text = "R";
                    btnS.Text = "S";

                    btnT.Text = "T";
                    btnU.Text = "U";
                    btnV.Text = "V";
                    btnW.Text = "W";
                    btnX.Text = "X";
                    btnY.Text = "Y";
                    btnZ.Text = "Z";
                    btnNn.Text = "Ñ";
                    btnchr7.Text = "Ç";
                }
                else
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";

                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";

                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";
                    btnNn.Text = "ñ";
                    btnchr7.Text = "ç";
                }
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

            if (chkCapsLock.Checked)
            {
                if ((chkShiftL.Checked) || (chkShiftR.Checked))
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";

                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";

                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";
                    btnNn.Text = "ñ";
                    btnchr7.Text = "ç";
                }
                else
                {
                    btnA.Text = "A";
                    btnB.Text = "B";
                    btnC.Text = "C";
                    btnD.Text = "D";
                    btnE.Text = "E";
                    btnF.Text = "F";
                    btnG.Text = "G";
                    btnH.Text = "H";
                    btnI.Text = "I";
                    btnJ.Text = "J";

                    btnK.Text = "K";
                    btnL.Text = "L";
                    btnM.Text = "M";
                    btnN.Text = "N";
                    btnO.Text = "O";
                    btnP.Text = "P";
                    btnQ.Text = "Q";
                    btnR.Text = "R";
                    btnS.Text = "S";

                    btnT.Text = "T";
                    btnU.Text = "U";
                    btnV.Text = "V";
                    btnW.Text = "W";
                    btnX.Text = "X";
                    btnY.Text = "Y";
                    btnZ.Text = "Z";
                    btnNn.Text = "Ñ";
                    btnchr7.Text = "Ç";
                }
            }
            else
            {

                if ((chkShiftL.Checked) || (chkShiftR.Checked))
                {
                    btnA.Text = "A";
                    btnB.Text = "B";
                    btnC.Text = "C";
                    btnD.Text = "D";
                    btnE.Text = "E";
                    btnF.Text = "F";
                    btnG.Text = "G";
                    btnH.Text = "H";
                    btnI.Text = "I";
                    btnJ.Text = "J";

                    btnK.Text = "K";
                    btnL.Text = "L";
                    btnM.Text = "M";
                    btnN.Text = "N";
                    btnO.Text = "O";
                    btnP.Text = "P";
                    btnQ.Text = "Q";
                    btnR.Text = "R";
                    btnS.Text = "S";

                    btnT.Text = "T";
                    btnU.Text = "U";
                    btnV.Text = "V";
                    btnW.Text = "W";
                    btnX.Text = "X";
                    btnY.Text = "Y";
                    btnZ.Text = "Z";
                    btnNn.Text = "Ñ";
                    btnchr7.Text = "Ç";
                }
                else
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";

                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";

                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";
                    btnNn.Text = "ñ";
                    btnchr7.Text = "ç";
                }
            }
        }

        private void chkCapsLock_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCapsLock.Checked)
            {
                chkCapsLock.Appearance.BackColor = Color.DimGray;
                if ((chkShiftL.Checked) || (chkShiftR.Checked))
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";

                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";

                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";
                    btnNn.Text = "ñ";
                    btnchr7.Text = "ç";
                }
                else
                {
                    btnA.Text = "A";
                    btnB.Text = "B";
                    btnC.Text = "C";
                    btnD.Text = "D";
                    btnE.Text = "E";
                    btnF.Text = "F";
                    btnG.Text = "G";
                    btnH.Text = "H";
                    btnI.Text = "I";
                    btnJ.Text = "J";

                    btnK.Text = "K";
                    btnL.Text = "L";
                    btnM.Text = "M";
                    btnN.Text = "N";
                    btnO.Text = "O";
                    btnP.Text = "P";
                    btnQ.Text = "Q";
                    btnR.Text = "R";
                    btnS.Text = "S";

                    btnT.Text = "T";
                    btnU.Text = "U";
                    btnV.Text = "V";
                    btnW.Text = "W";
                    btnX.Text = "X";
                    btnY.Text = "Y";
                    btnZ.Text = "Z";
                    btnNn.Text = "Ñ";
                    btnchr7.Text = "Ç";
                }
            }
            else
            {
                chkCapsLock.Appearance.BackColor = Color.Black;
                if ((chkShiftL.Checked) || (chkShiftR.Checked))
                {
                    btnA.Text = "A";
                    btnB.Text = "B";
                    btnC.Text = "C";
                    btnD.Text = "D";
                    btnE.Text = "E";
                    btnF.Text = "F";
                    btnG.Text = "G";
                    btnH.Text = "H";
                    btnI.Text = "I";
                    btnJ.Text = "J";

                    btnK.Text = "K";
                    btnL.Text = "L";
                    btnM.Text = "M";
                    btnN.Text = "N";
                    btnO.Text = "O";
                    btnP.Text = "P";
                    btnQ.Text = "Q";
                    btnR.Text = "R";
                    btnS.Text = "S";

                    btnT.Text = "T";
                    btnU.Text = "U";
                    btnV.Text = "V";
                    btnW.Text = "W";
                    btnX.Text = "X";
                    btnY.Text = "Y";
                    btnZ.Text = "Z";
                    btnNn.Text = "Ñ";
                    btnchr7.Text = "Ç";
                }
                else
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";

                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";

                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";
                    btnNn.Text = "ñ";
                    btnchr7.Text = "ç";
                }
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
                WriteCharacter("\"");
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
                WriteCharacter("·");
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
                WriteCharacter(SystemVariables.CurrencySymbol);
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
                WriteCharacter("{%}");
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
                WriteCharacter("&");
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
                WriteCharacter("/");
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
                WriteCharacter("{(}");
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
                WriteCharacter("{)}");
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
                WriteCharacter("=");
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
                WriteCharacter("?");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("'");
            }
        }

        private void btnchr3_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("¿");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("¡");
            }
        }

        private void btnchr1_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("ª");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("º");
            }
        }

        private void btnchr4_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("{^}");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("`");
            }
        }

        private void btnchr5_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("*");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("{+}");
            }
        }

        private void btnchr6_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("¨");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("´");
            }
        }

        private void btnchr7_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter("ç");
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
                WriteCharacter(";");
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
                WriteCharacter(":");
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
                WriteCharacter("_");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("-");
            }
        }

        private void btnchr11_Click(object sender, EventArgs e)
        {
            if ((chkShiftL.Checked) || (chkShiftR.Checked))
            {
                WriteCharacter(">");
                chkShiftL.Checked = false;
                chkShiftR.Checked = false;
            }
            else
            {
                WriteCharacter("<");
            }
        }


    }
}
