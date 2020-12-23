using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WpfApp1.Reports
{
    public partial class ReportRemains : Form
    {
        public ReportRemains()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.remainsTableAdapter.Fill(this.hardware_StoreDataSet.Remains, dateTimePicker1.Value);
            reportViewer1.LocalReport.SetParameters(new ReportParameter("date", dateTimePicker1.Value.ToShortDateString()));
            this.reportViewer1.RefreshReport();
        }
    }
}
