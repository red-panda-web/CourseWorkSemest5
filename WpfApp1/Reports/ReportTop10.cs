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
            this.top10TableAdapter.Fill(this.hardware_StoreDataSet.top10, dateTimePicker1.Value, dateTimePicker2.Value);
            this.reportViewer1.RefreshReport();
        }
    }
}
