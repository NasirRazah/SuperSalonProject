using System;
using System.Collections.Generic;
using System.Data;
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

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frm_Calendar.xaml
    /// </summary>
    public partial class frm_Calendar : Window
    {
        public frm_Calendar()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);

            this.cal.Footer.Align = Pabo.Calendar.mcTextAlign.Right;
            this.cal.Footer.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cal.Footer.Format = Pabo.Calendar.mcTodayFormat.Long;
            this.cal.Footer.TextColor = System.Drawing.Color.OrangeRed;
            this.cal.Header.BackColor1 = System.Drawing.Color.Black;
            this.cal.Header.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cal.Header.MonthSelectors = false;
            this.cal.Header.TextColor = System.Drawing.Color.White;
            this.cal.Location = new System.Drawing.Point(27, 163);
            this.cal.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.cal.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.cal.Month.BackgroundImage = null;
            this.cal.Month.Colors.Days.Date = System.Drawing.Color.DodgerBlue;
            this.cal.Month.Colors.Focus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(189)))), ((int)(((byte)(235)))));
            this.cal.Month.Colors.Focus.Border = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(166)))), ((int)(((byte)(228)))));
            this.cal.Month.Colors.Selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(166)))), ((int)(((byte)(228)))));
            this.cal.Month.Colors.Selected.Border = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(82)))), ((int)(((byte)(177)))));
            this.cal.Month.DateFont = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cal.Month.TextFont = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cal.Name = "cal";
            this.cal.SelectionMode = Pabo.Calendar.mcSelectionMode.One;
            this.cal.Size = new System.Drawing.Size(543, 360);
            this.cal.TabIndex = 8;
            this.cal.TodayColor = System.Drawing.Color.Green;
            this.cal.Weekdays.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cal.Weekdays.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(135)))), ((int)(((byte)(220)))));
            this.cal.Weeknumbers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cal.Weeknumbers.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(135)))), ((int)(((byte)(220)))));
        }

        private void OnCloseCommand(object obj)
        {
            DialogResult = false;
            Close();
        }

        private bool blchange = false;
        private string[] m_daysSelected = null;
        private int selDay = 1;
        private string strPassingValue;
        private DateTime selDate = DateTime.Today;
        private DateTime dtcaldate;
        private int FirstYear = 0;

        private bool bCallForAge = false;

        public bool iscallforAge
        {
            get { return bCallForAge; }
            set { bCallForAge = value; }
        }

        public DateTime caldate
        {
            get { return dtcaldate; }
            set { dtcaldate = value; }
        }
        public string PassingValue
        {
            get { return strPassingValue; }
            set { strPassingValue = value; }
        }
        private void CmbYear_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blchange)
            {
                cal.ActiveMonth.Year = GeneralFunctions.fnInt32(cmbYear.EditValue.ToString());
                SelectDay();
            }
        }

        private void CmbMonth_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (blchange)
            {
                cal.ActiveMonth.Month = GeneralFunctions.fnInt32(cmbMonth.EditValue.ToString());
                SelectDay();
            }
        }
        private void GetCalMinMaxDate()
        {
            cal.MinDate = new DateTime(cal.ActiveMonth.Year, 1, 1);
            cal.MinDate = new DateTime(cal.ActiveMonth.Year, 12, 31);
        }
        private void Btnyp_Click(object sender, RoutedEventArgs e)
        {
            blchange = false;
            cal.ActiveMonth.Year = cal.ActiveMonth.Year - 1;
            PopulateYear1();
            SelectDay();
            blchange = true;
        }

        private void PopulateYear()
        {
            cmbYear.Items.Clear();
            if (!bCallForAge)
            {
                for (int intCount = FirstYear - 10; intCount <= FirstYear + 10; intCount++)
                {
                    cmbYear.Items.Add(intCount.ToString());
                }
            }
            else
            {
                for (int intCount = FirstYear - 80; intCount <= DateTime.Today.Year; intCount++)
                {
                    cmbYear.Items.Add(intCount.ToString());
                }
            }
            cmbYear.SelectedIndex = cmbYear.Items.IndexOf(cal.ActiveMonth.Year.ToString());
        }
        private void PopulateYear1()
        {
            cmbYear.SelectedIndex = cmbYear.Items.IndexOf(cal.ActiveMonth.Year.ToString());
        }

        private void Btnyn_Click(object sender, RoutedEventArgs e)
        {
            blchange = false;
            cal.ActiveMonth.Year = cal.ActiveMonth.Year + 1;
            PopulateYear1();
            SelectDay();
            blchange = true;
        }

        private void Btnmp_Click(object sender, RoutedEventArgs e)
        {
            blchange = false;
            if (cal.ActiveMonth.Month == 1)
            {
                cal.ActiveMonth.Month = 12;
                cal.ActiveMonth.Year = cal.ActiveMonth.Year - 1;
                PopulateYear1();
            }
            else
            {
                cal.ActiveMonth.Month = cal.ActiveMonth.Month - 1;
            }
            cmbMonth.EditValue = cal.ActiveMonth.Month;
            SelectDay();
            blchange = true;
        }

        private void Btnmn_Click(object sender, RoutedEventArgs e)
        {
            blchange = false;
            if (cal.ActiveMonth.Month == 12)
            {
                cal.ActiveMonth.Month = 1;
                cal.ActiveMonth.Year = cal.ActiveMonth.Year + 1;
                PopulateYear1();
            }
            else
            {
                cal.ActiveMonth.Month = cal.ActiveMonth.Month + 1;
            }
            cmbMonth.EditValue = cal.ActiveMonth.Month;
            SelectDay();
            blchange = true;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (m_daysSelected == null) return;
            dtcaldate = selDate;
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Cal_DayClick(object sender, Pabo.Calendar.DayClickEventArgs e)
        {

        }

        private void Cal_DaySelected(object sender, Pabo.Calendar.DaySelectedEventArgs e)
        {
            m_daysSelected = e.Days;
            foreach (string str in m_daysSelected)
            {
                selDate = GeneralFunctions.fnDate(str);
                selDay = selDate.Day;
                break;
            }
        }

        private void SelectDay()
        {

            try
            {
                cal.SelectDate(new DateTime(cal.ActiveMonth.Year, cal.ActiveMonth.Month, selDay));
            }
            catch
            {
                cal.SelectDate(new DateTime(cal.ActiveMonth.Year, cal.ActiveMonth.Month, 1));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Calendar;
            blchange = false;
            if (strPassingValue != "")
            {
                cal.SelectDate(GeneralFunctions.fnDate(strPassingValue));
            }
            else
            {
                cal.SelectDate(DateTime.Today);
            }
            cal.Culture = Settings.POS_Culture;
            cal.ActiveMonth.Year = selDate.Year;
            cal.ActiveMonth.Month = selDate.Month;

            cmbYear.Items.Clear();

            if (!bCallForAge)
            {
                for (int intCount = cal.ActiveMonth.Year - 10; intCount <= cal.ActiveMonth.Year + 10; intCount++)
                {
                    cmbYear.Items.Add(intCount.ToString());
                }
            }
            else
            {
                for (int intCount = cal.ActiveMonth.Year - 80; intCount <= DateTime.Today.Year; intCount++)
                {
                    cmbYear.Items.Add(intCount.ToString());
                }
                lbCustomText.Text = "Minimum Age " + (DateTime.Today.Year - GeneralFunctions.fnDate(strPassingValue).Year).ToString() + " : Birthday on or before " + GeneralFunctions.fnDate(strPassingValue).ToString(SystemVariables.DateFormat);
                lbCustomText.Visibility = Visibility.Visible;
            }
            cmbYear.SelectedIndex = cmbYear.Items.IndexOf(cal.ActiveMonth.Year.ToString());
            FirstYear = cal.ActiveMonth.Year;

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Month", System.Type.GetType("System.String"));
            dtbl.Rows.Add(new object[] { "1",Properties.Resources.January });
            dtbl.Rows.Add(new object[] { "2",Properties.Resources.February });
            dtbl.Rows.Add(new object[] { "3",Properties.Resources.March  });
            dtbl.Rows.Add(new object[] { "4",Properties.Resources.April  });
            dtbl.Rows.Add(new object[] { "5",Properties.Resources.May  });
            dtbl.Rows.Add(new object[] { "6",Properties.Resources.June });
            dtbl.Rows.Add(new object[] { "7",Properties.Resources.July });
            dtbl.Rows.Add(new object[] { "8",Properties.Resources.August });
            dtbl.Rows.Add(new object[] { "9", Properties.Resources.September });
            dtbl.Rows.Add(new object[] { "10",Properties.Resources.October });
            dtbl.Rows.Add(new object[] { "11",Properties.Resources.November  });
            dtbl.Rows.Add(new object[] { "12", Properties.Resources.December });
            cmbMonth.ItemsSource = dtbl;
            cmbMonth.EditValue = cal.ActiveMonth.Month;
            SelectDay();
            blchange = true;
        }

        private void CmbYear_PopupOpening(object sender, DevExpress.Xpf.Editors.OpenPopupEventArgs e)
        {
            DevExpress.Utils.Win.IPopupControl popup = sender as DevExpress.Utils.Win.IPopupControl;
            DevExpress.XtraEditors.Popup.PopupGridLookUpEditForm popupForm = popup.PopupWindow as DevExpress.XtraEditors.Popup.PopupGridLookUpEditForm;
            popupForm.Width =(int) (sender as Control).Width;
        }
    }
}
