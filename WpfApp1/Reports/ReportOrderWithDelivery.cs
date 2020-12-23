using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace WpfApp1.Reports
{
    public partial class ReportOrderWithDelivery : Form
    {
        public ReportOrderWithDelivery()
        {
            InitializeComponent();
        }

        private void ReportOrderWithDelivery_Load(object sender, EventArgs e)
        {
            int id_order = getIdForOrderReports.id_order;
            int id_client = getIdForOrderReports.id_client;
            this.getOrderInfoWithDeliveryTableAdapter.Fill(this.hardware_StoreDataSet.getOrderInfoWithDelivery, id_order);
            this.getClientAddressTableAdapter.Fill(this.hardware_StoreDataSet.getClientAddress, id_client);
            this.getOrdersItemsTableAdapter.Fill(this.hardware_StoreDataSet.getOrdersItems, id_order);
            reportViewer1.LocalReport.SetParameters(new ReportParameter("idOrder", Convert.ToString(id_order)));
            this.reportViewer1.RefreshReport();
        }
    }
}
