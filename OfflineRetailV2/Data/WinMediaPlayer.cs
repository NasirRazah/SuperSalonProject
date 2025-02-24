// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace OfflineRetailV2.Data
{
    public class WinMediaPlayer
    {
        private static Form frm_wmp;
        private static AxWMPLib.AxWindowsMediaPlayer player;
        private static Panel pnlclient;

        public WinMediaPlayer()
        {
            bool IsOpen = false;
            foreach (Window f in System.Windows.Application.Current.Windows)
            {
                if (f.Name == "WnPlayer")
                {
                    IsOpen = true;
                    break;
                }
            }
            if (!IsOpen)
            {
                Process[] pArry = Process.GetProcesses();
                foreach (Process p in pArry)
                {
                    int procid = p.Id;
                    if ((procid == SystemVariables.SecondMonitorAppID) && (SystemVariables.SecondMonitorAppID > 0))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch
                        {
                        }
                    }
                }

                int posL = 0;
                int posT = 0;
                int posW = 600;
                int posH = 800;
                if (System.Windows.Forms.Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor)
                {
                    if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                    {
                        if (System.Windows.Forms.Screen.AllScreens[1] != null)
                        {
                            Rectangle secondMonitor = System.Windows.Forms.Screen.AllScreens[1].WorkingArea;
                            posL = secondMonitor.Left;
                            posT = secondMonitor.Top;
                            posW = secondMonitor.Width;
                            posH = secondMonitor.Height;
                        }
                    }
                }

                frm_wmp = new Form();
                frm_wmp.FormBorderStyle = FormBorderStyle.None;
                frm_wmp.StartPosition = FormStartPosition.Manual;
                frm_wmp.Left = posL;
                frm_wmp.Top = posT;
                frm_wmp.Width = posW;
                frm_wmp.Height = posH;
                frm_wmp.TopMost = true;
                frm_wmp.Name = "WnPlayer";
                frm_wmp.Show();
                InitializeComponent();
            }
        }

        public static void CloseWinPlayer()
        {
            player.Dispose();
            frm_wmp.Close();
            frm_wmp.Dispose();
        }

        public static void playfile(string filepath)
        {
            //player.fullScreen = true;
            player.uiMode = "none";
            player.URL = filepath;
            //player.settings.setMode("loop", true);
        }

        private void InitializeComponent()
        {
            player = new AxWMPLib.AxWindowsMediaPlayer();
            pnlclient = new Panel();
            player.SuspendLayout();
            pnlclient.SuspendLayout();

            player.Dock = DockStyle.Fill;
            pnlclient.Dock = DockStyle.Fill;
            pnlclient.Controls.Add(player);
            frm_wmp.Controls.Add(pnlclient);
            player.ResumeLayout();
            pnlclient.ResumeLayout();
        }
    }
}