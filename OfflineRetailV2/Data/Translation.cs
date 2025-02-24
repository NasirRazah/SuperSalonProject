using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OfflineRetailV2.Data
{
   public class Translation
    {
        public static void SetMultilingualTextInXtraReport(DevExpress.XtraReports.UI.XtraReport rept)
        {
            if (Settings.disableresource == "Y") return;

            Assembly a = Assembly.Load("pos");
            ResourceManager rm = new ResourceManager("pos.POSResource", Assembly.GetExecutingAssembly());
            if (SystemVariables.RM == null) SystemVariables.RM = rm;



            foreach (DevExpress.XtraReports.UI.Band band in rept.Bands)
            {
                foreach (DevExpress.XtraReports.UI.XRControl ctrl in band)
                {
                    if (ctrl.GetType() == typeof(DevExpress.XtraReports.UI.XRLabel))
                    {
                        if ((ctrl as DevExpress.XtraReports.UI.XRLabel).DataBindings.Count == 1) continue;
                        if (ctrl.Text.Trim() != "")
                        {
                            string default_txt = "";
                            default_txt = ctrl.Text.Trim();
                            ctrl.Text = rm.GetString(rept.GetType().Name + "_" + ctrl.Name, Settings.POS_Culture).Replace("$", SystemVariables.CurrencySymbol).Replace("Gware", SystemVariables.BrandName);
                            if (ctrl.Text == "") // when control not in resource
                            {
                                ctrl.Text = default_txt.Replace("$", SystemVariables.CurrencySymbol).Replace("Gware", SystemVariables.BrandName);
                            }
                        }
                    }

                    if (ctrl.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                    {
                        foreach (DevExpress.XtraReports.UI.XRTableRow xrRw in (ctrl as DevExpress.XtraReports.UI.XRTable).Rows)
                        {
                            foreach (DevExpress.XtraReports.UI.XRTableCell tabcell in (xrRw as DevExpress.XtraReports.UI.XRTableRow).Cells)
                            {
                                if (tabcell.DataBindings.Count == 1) continue;
                                try
                                {
                                    if (tabcell.Tag.ToString() == "X") continue;
                                }
                                catch
                                {
                                }
                                if (tabcell.Text.Trim() != "")
                                {
                                    string default_txt = "";
                                    default_txt = tabcell.Text.Trim();

                                    try
                                    {
                                        tabcell.Text = rm.GetString(rept.GetType().Name + "_" + tabcell.Name, Settings.POS_Culture).Replace("$", SystemVariables.CurrencySymbol).Replace("Gware", SystemVariables.BrandName);
                                        if (tabcell.Text == "") // when control not in resource
                                        {
                                            tabcell.Text = default_txt.Replace("$", SystemVariables.CurrencySymbol).Replace("Gware", SystemVariables.BrandName);
                                        }
                                    }
                                    catch
                                    {
                                        tabcell.Text = default_txt.Replace("$", SystemVariables.CurrencySymbol).Replace("Gware", SystemVariables.BrandName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
