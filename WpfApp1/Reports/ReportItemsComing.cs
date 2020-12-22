using System;
using System.Windows.Forms;

namespace WpfApp1.Reports
{
    public partial class ReportItemsComing : Form
    {
        public ReportItemsComing()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.getSuppliesTableAdapter.Fill(this.hardware_StoreDataSet.getSupplies, dateTimePicker1.Value, dateTimePicker2.Value);
            this.reportViewer1.RefreshReport();
        }
    }
}
