using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WpfApp1.Reports
{
    public partial class ReportItemsRelease : Form
    {
        public ReportItemsRelease()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.getOrdersTableAdapter.Fill(this.hardware_StoreDataSet.getOrders, dateFrom.Value, dateTo.Value);
            reportViewer1.LocalReport.SetParameters(new ReportParameter("dateFrom", dateFrom.Value.ToShortDateString()));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("dateTo", dateTo.Value.ToShortDateString()));
            this.reportViewer1.RefreshReport();
        }
    }
}
