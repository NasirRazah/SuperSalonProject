// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Editors;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace OfflineRetailV2
{
    public static class ResMan
    {

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);

        public static void ShowFullKeyboard()
        {
            Process[] oskProcessArray = Process.GetProcessesByName("TabTip");
            foreach (Process onscreenProcess in oskProcessArray)
            {
                onscreenProcess.Kill();
            }

            string touchKeyboardPath =
           @"C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe";
            Process.Start(touchKeyboardPath);
        }

        public static string DelimitCurrency(decimal currency)
        {
            if (currency == 0)
            {
                return "0";
            }

            return currency.ToString("#,#");
        }

        public static void SetDecimal(TextEdit textEdit, byte decimalCount)
        {
            textEdit.MaskType = DevExpress.Xpf.Editors.MaskType.Numeric;
            if (decimalCount == 0)
                textEdit.Mask = "################;";
            if (decimalCount == 1)
                textEdit.Mask = "################.#;";
            if (decimalCount == 2)
                textEdit.Mask = "################.##;";
            if (decimalCount == 3)
                textEdit.Mask = "################.###;";
            if (decimalCount == 4)
                textEdit.Mask = "################.####;";
            textEdit.MaskCulture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
        }
        public static void DelimitCurrency(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return;

            if (decimal.TryParse(textBox.Text, out decimal _result))
                textBox.Text = DelimitCurrency(_result);
        }

        public static MessageBoxWindow MessageBox => new MessageBoxWindow();

        public static FontFamily OSBold
        {
            get
            {
                try
                {
                    return (FontFamily)FindResource("OSBold");
                }
                catch
                {
                    return null;
                }
            }
        }

        public static FontFamily OSLight
        {
            get
            {
                try
                {
                    return (FontFamily)FindResource("OSLight");
                }
                catch
                {
                    return null;
                }
            }
        }

        public static FontFamily OSRegular
        {
            get
            {
                try
                {
                    return (FontFamily)FindResource("OSRegular");
                }
                catch
                {
                    return null;
                }
            }
        }

        public static FontFamily OSSemiBold
        {
            get
            {
                try
                {
                    return (FontFamily)FindResource("OSSemiBold");
                }
                catch
                {
                    return null;
                }
            }
        }

        public static BitmapImage Product
        {
            get
            {
                try
                {
                    return (BitmapImage)FindResource("Product");
                }
                catch
                {
                    return null;
                }
            }
        }

        public static object FindResource(string resourceKey)
        {
            return Application.Current.FindResource(resourceKey);
        }

        public static void SetImage(System.Windows.Controls.Image img, string strFileName)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(strFileName);
            bitmap.EndInit();

            img.Source = bitmap;
        }

        public static void SetImage(System.Windows.Controls.Image img, Stream stream)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();

            img.Source = bitmap;
        }

        public static void closeKeyboard()
        {
            uint WM_SYSCOMMAND = 274;
            uint SC_CLOSE = 61536;
            IntPtr KeyboardWnd = FindWindow("IPTip_Main_Window", null);
            PostMessage(KeyboardWnd.ToInt32(), WM_SYSCOMMAND, (int)SC_CLOSE, 0);
        }
    }
}