using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.reportViewer1.RefreshReport();
        }
    }
}
