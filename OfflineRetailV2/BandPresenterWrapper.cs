using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.Native.Presenters;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace OfflineRetailV2
{
    class BandPresenterWrapper : BandPresenter
    {
        readonly BandPresenter presenter;
        readonly CultureInfo culture;

        public BandPresenterWrapper(BandPresenter presenter, CultureInfo culture, XtraReport report) : base(report)
        {
            this.presenter = presenter;
            this.culture = culture;
        }

        public override List<XRControl> GetPrintableControls(Band band,bool sort)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            return presenter.GetPrintableControls(band);
        }
    }
}
