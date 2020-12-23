using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WpfApp1.Reports
{
    public partial class ReportTop10 : Form
    {
        public ReportTop10()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.top10TableAdapter.Fill(this.hardware_StoreDataSet.top10, dateFrom.Value, dateTo.Value);
            reportViewer1.LocalReport.SetParameters(new ReportParameter("dateFrom", dateFrom.Value.ToShortDateString()));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("dateTo", dateTo.Value.ToShortDateString()));
            this.reportViewer1.RefreshReport();
        }
    }
}
