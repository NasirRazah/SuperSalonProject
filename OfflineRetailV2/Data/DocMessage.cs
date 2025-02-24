using System;
using System.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace OfflineRetailV2.Data
{
    public class DocMessage
    {
        public class MessageLocalizer : Localizer
        {
            public override string GetLocalizedString(StringId id)
            {

                switch (id)
                {

                    case StringId.XtraMessageBoxOkButtonText: return Properties.Resources.OK;
                    case StringId.XtraMessageBoxCancelButtonText: return Properties.Resources.Cancel;
                    case StringId.XtraMessageBoxRetryButtonText: return Properties.Resources.Retry;
                    case StringId.XtraMessageBoxYesButtonText: return Properties.Resources.Yes;
                    case StringId.XtraMessageBoxNoButtonText: return Properties.Resources.No;
                }
                return base.GetLocalizedString(id);
            }
        }

        public DocMessage()
        {

        }

        public static void MsgInformationForVersionUpdate(String strMsg, string strCaption)
        {
            Localizer.Active = new MessageLocalizer();
            ResMan.MessageBox.Show(strMsg, strCaption, MessageBoxButton.OK, MessageBoxImage.None);
        }

        public static void MsgPermission()
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess_Line1 + "\n" + Properties.Resources.strRestrictedAccess_Line2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
            else
                ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess_Line1 + "\n" + Properties.Resources.strRestrictedAccess_Line2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        public static void MsgPermission1()
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess1_Line1 + "\n" + Properties.Resources.strRestrictedAccess1_Line2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
            else
                ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess1_Line1 + "\n" + Properties.Resources.strRestrictedAccess1_Line2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        public static void MsgPermission2()
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
            else
                ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        public static void MsgError(String strMsg)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                ResMan.MessageBox.Show(strMsg, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            else
                ResMan.MessageBox.Show(strMsg, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public static void MsgInformation(String strMsg)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                ResMan.MessageBox.Show(strMsg, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                ResMan.MessageBox.Show(strMsg, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult MsgConfirmation(String strMsg)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(strMsg, Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question);
            else
                return ResMan.MessageBox.Show(strMsg, Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question);

        }

        public static MessageBoxResult MsgOkCancel(String strMsg)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(strMsg, Properties.Resources.Confirm, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            else
                return ResMan.MessageBox.Show(strMsg, Properties.Resources.Confirm, MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }

        public static void MsgWarning(String strMsg)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                ResMan.MessageBox.Show(strMsg, Properties.Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                ResMan.MessageBox.Show(strMsg, Properties.Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static bool MsgDelete()
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
            {
                if (ResMan.MessageBox.Show(Properties.Resources.strDelete, Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    return true;
                else
                    return false;
            }
            else
            {
                if (ResMan.MessageBox.Show(Properties.Resources.strDelete, Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool MsgDelete(String strMsg)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
            {
                if (ResMan.MessageBox.Show(Properties.Resources.strCustomDelete + " " + strMsg + "?", Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    return true;
                else
                    return false;
            }
            else
            {
                if (ResMan.MessageBox.Show(Properties.Resources.strCustomDelete + " " + strMsg + "?", Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool MsgDelete(String strMsg, String strCode)
        {
            Localizer.Active = new MessageLocalizer();

            if (Settings.IsPOSSelected)
            {
                if (ResMan.MessageBox.Show(Properties.Resources.strCustomDelete + " " + strMsg + " " + strCode + "?", Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    return true;
                else
                    return false;
            }
            else
            {
                if (ResMan.MessageBox.Show(Properties.Resources.strCustomDelete + " " + strMsg + " " + strCode + "?", Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static MessageBoxResult MsgSaveChangesScheduler()
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(Properties.Resources.strSaveChangesTask, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            else
                return ResMan.MessageBox.Show(Properties.Resources.strSaveChangesTask, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }

        public static MessageBoxResult MsgSaveChanges()
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            else
                return ResMan.MessageBox.Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }

        public static MessageBoxResult MsgInvalid(String strInValidCode)
        {
            Localizer.Active = new MessageLocalizer();

            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(Properties.Resources.strCustomInValid + " " + strInValidCode, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                return ResMan.MessageBox.Show(Properties.Resources.strCustomInValid + " " + strInValidCode, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult MsgEnter(String strEnter)
        {

            Localizer.Active = new MessageLocalizer();

            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(Properties.Resources.strCustomEnter + " " + strEnter, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                return ResMan.MessageBox.Show(Properties.Resources.strCustomEnter + " " + strEnter, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public static void MsgNoAccess()
        {
            MsgInformation("You do not have permission to perform this operation." + "\n" + "Please contact your System Administrator.");
        }

        public static MessageBoxResult POSRestrictAccess()
        {
            Localizer.Active = new MessageLocalizer();
            return ResMan.MessageBox.Show(Properties.Resources.strRestrictedAccess_Line1 + "\n" + Properties.Resources.strRestrictedAccess_Line2, Properties.Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult MsgRetryCancel(string ErrorMessage)
        {
            Localizer.Active = new MessageLocalizer();
            if (Settings.IsPOSSelected)
                return ResMan.MessageBox.Show(ErrorMessage, Properties.Resources.Information, MessageBoxButton.OKCancel, MessageBoxImage.Information);
            else
                return ResMan.MessageBox.Show(ErrorMessage, Properties.Resources.Information, MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }

        public static void ShowException(string strOperation, string strErrorMessage)
        {
            /*	frm_ErrorDlg ErrDlg = new frm_ErrorDlg(strOperation, strErrorMessage);
                ErrDlg.ShowDialog();
                ErrDlg.Dispose(); 
             */
        }

        public static MessageBoxResult MsgNameChange()
        {
            Localizer.Active = new MessageLocalizer();
            return ResMan.MessageBox.Show(Properties.Resources.strMsgNameChange_Line1 + "\n" + Properties.Resources.strMsgNameChange_Line2 + "\n" + Properties.Resources.strMsgNameChange_Line3, Properties.Resources.NameChangeConfirmation, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }

        public static MessageBoxResult MsgAddressChange()
        {
            Localizer.Active = new MessageLocalizer();
            return ResMan.MessageBox.Show(Properties.Resources.strMsgAddressChange_Line1 + "\n" + Properties.Resources.strMsgAddressChange_Line2 + "\n" + Properties.Resources.strMsgAddressChange_Line3, Properties.Resources.AddressChangeConfirmation, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }

        public static bool MsgGridStatus()
        {
            Localizer.Active = new MessageLocalizer();
            if (ResMan.MessageBox.Show(Properties.Resources.strGridStatus_Line1 + "\n" + Properties.Resources.strGridStatus_Line2, Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
