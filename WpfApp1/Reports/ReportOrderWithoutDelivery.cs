using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WpfApp1.Reports
{
    public partial class ReportOrderWithoutDelivery : Form
    {
        public ReportOrderWithoutDelivery()
        {
            InitializeComponent();
        }

        private void ReportOrderWithoutDelivery_Load(object sender, EventArgs e)
        {
            int id_order = getIdForOrderReports.id_order;
            this.getOrderInfoWithoutDeliveryTableAdapter.Fill(this.hardware_StoreDataSet.getOrderInfoWithoutDelivery, id_order);
            this.getOrdersItemsTableAdapter.Fill(this.hardware_StoreDataSet.getOrdersItems, id_order);
            reportViewer1.LocalReport.SetParameters(new ReportParameter("idOrder", Convert.ToString(id_order)));
            this.reportViewer1.RefreshReport();
        }
    }
}
