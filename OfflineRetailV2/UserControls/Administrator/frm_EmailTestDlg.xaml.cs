using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_EmailTestDlg.xaml
    /// </summary>
    public partial class frm_EmailTestDlg : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frm_EmailTestDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private string strSMTPHost;
        private string strSMTPPort;
        private string strSMTPUser;
        private string strSMTPPswd;
        private string strSMTPSSL;
        private string strFromAddr;
        private string strToAddr;

        public string SMTP_Host
        {
            get { return strSMTPHost; }
            set { strSMTPHost = value; }
        }

        public string SMTP_Port
        {
            get { return strSMTPPort; }
            set { strSMTPPort = value; }
        }

        public string SMTP_User
        {
            get { return strSMTPUser; }
            set { strSMTPUser = value; }
        }

        public string SMTP_Pswd
        {
            get { return strSMTPPswd; }
            set { strSMTPPswd = value; }
        }

        public string SMTP_SSL
        {
            get { return strSMTPSSL; }
            set { strSMTPSSL = value; }
        }

        public string From_Addr
        {
            get { return strFromAddr; }
            set { strFromAddr = value; }
        }

        public string To_Addr
        {
            get { return strToAddr; }
            set { strToAddr = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.Email_Setup_Test;
            txtREFromEmail.Text = strFromAddr;
            txtReportEmail.Text = strToAddr;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtReportEmail.Text.Trim() == "")
            {
                DocMessage.MsgInformation(Properties.Resources.Enter_To_Address);
                GeneralFunctions.SetFocus(txtReportEmail);
                return;
            }

            Cursor = Cursors.Wait;
            bool bl = false;
            try
            {
            //    MailMessage message = new MailMessage();
            //    SmtpClient smtp = new SmtpClient();
            //    message.From = new MailAddress(strFromAddr);
            //    message.To.Add(new MailAddress(txtReportEmail.Text.Trim()));
            //    message.Subject = Properties.Resources.Test_Email_From_ + strFromAddr;
            //    message.IsBodyHtml = true; //to make message body as html  
            //    message.Body = Properties.Resources.This_email_is_generated_by_XEPOS; ;
            //    smtp.Port = 587;
            //    smtp.Host = strSMTPHost;
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new System.Net.NetworkCredential(strSMTPUser, strSMTPPswd);
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtp.Send(message);

               
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                
                smtp.Host = strSMTPHost;
                smtp.Port = GeneralFunctions.fnInt32(strSMTPPort);
                smtp.EnableSsl = (strSMTPSSL == "Y");
                //smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(strSMTPUser, strSMTPPswd);
                //       smtp.UseDefaultCredentials = false;
       //         smtp.Credentials   = new System.Net.NetworkCredential(strSMTPUser, strSMTPPswd);
                smtp.ServicePoint.MaxIdleTime = 1;

                MailMessage msg = new MailMessage();
                msg.To.Add(txtReportEmail.Text.Trim());
                msg.From = new System.Net.Mail.MailAddress(strFromAddr);

                msg.Subject = Properties.Resources.Test_Email_From_ + strFromAddr;
                msg.Body = Properties.Resources.This_email_is_generated_by_XEPOS;

                smtp.Send(msg);
 
                bl = true;
            }
            catch (Exception er)
            {
            }
            finally
            {
               Cursor = Cursors.Arrow;
            }
            if (bl)
            {
                DocMessage.MsgInformation(Properties.Resources.Email_sent_successfully);
                CloseKeyboards();
                Close();
            }
            else
            {
                DocMessage.MsgInformation(Properties.Resources.Error_while_sending_Email);
                CloseKeyboards();
                Close();
            }
        }

        //static async Task sendEmailUsingSendGrid(string apiKey)
        //{
        //    var client = new SendGridClient(apiKey);
        //    ////send an email message using the SendGrid Web API with a console application.  
        //    var msgs = new SendGridMessage()
        //    {
        //        From = new EmailAddress("pankaj.sapkal@xxx.com", "Pankaj Sapkal"),
        //        Subject = "Subject line",
        //        TemplateId = "fb09a5fb-8bc3-4183-b648-dc6d48axxxxx",
        //        ////If you have html ready and dont want to use Template's   
        //        //PlainTextContent = "Hello, Email!",  
        //        //HtmlContent = "<strong>Hello, Email!</strong>",  
        //    };
        //    //if you have multiple reciepents to send mail  
        //    msgs.AddTo(new EmailAddress("pankaj.sapkal@xxx.com", "Pankaj Sapkal"));
        //    //If you have attachment  
        //    var attach = new Attachment();
        //    //attach.Content = Convert.ToBase64String("rawValues");  
        //    attach.Type = "image/png";
        //    attach.Filename = "hulk.png";
        //    attach.Disposition = "inline";
        //    attach.ContentId = "hulk2";
        //    //msgs.AddAttachment(attach.Filename, attach.Content, attach.Type, attach.Disposition, attach.ContentId);  
        //    //Set footer as per your requirement  
        //    msgs.SetFooterSetting(true, "<strong>Regards,</strong><b> Pankaj Sapkal", "Pankaj");
        //    //Tracking (Appends an invisible image to HTML emails to track emails that have been opened)  
        //    //msgs.SetClickTracking(true, true);  
        //    var responses = await client.SendEmailAsync(msgs);
        //}

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }
    }
}
