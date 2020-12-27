
namespace WpfApp1.Reports
{
    partial class ReportOrderWithDelivery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportOrderWithDelivery));
            this.getOrderInfoWithDeliveryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hardware_StoreDataSet = new WpfApp1.Hardware_StoreDataSet();
            this.getClientAddressBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.getOrdersItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.getOrderInfoWithDeliveryTableAdapter = new WpfApp1.Hardware_StoreDataSetTableAdapters.getOrderInfoWithDeliveryTableAdapter();
            this.getClientAddressTableAdapter = new WpfApp1.Hardware_StoreDataSetTableAdapters.getClientAddressTableAdapter();
            this.getOrdersItemsTableAdapter = new WpfApp1.Hardware_StoreDataSetTableAdapters.getOrdersItemsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.getOrderInfoWithDeliveryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardware_StoreDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getClientAddressBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getOrdersItemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // getOrderInfoWithDeliveryBindingSource
            // 
            this.getOrderInfoWithDeliveryBindingSource.DataMember = "getOrderInfoWithDelivery";
            this.getOrderInfoWithDeliveryBindingSource.DataSource = this.hardware_StoreDataSet;
            // 
            // hardware_StoreDataSet
            // 
            this.hardware_StoreDataSet.DataSetName = "Hardware_StoreDataSet";
            this.hardware_StoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getClientAddressBindingSource
            // 
            this.getClientAddressBindingSource.DataMember = "getClientAddress";
            this.getClientAddressBindingSource.DataSource = this.hardware_StoreDataSet;
            // 
            // getOrdersItemsBindingSource
            // 
            this.getOrdersItemsBindingSource.DataMember = "getOrdersItems";
            this.getOrdersItemsBindingSource.DataSource = this.hardware_StoreDataSet;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.getOrderInfoWithDeliveryBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.getClientAddressBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.getOrdersItemsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WpfApp1.Reports.ReportOrderWithDelivery_blank.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(13, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(775, 426);
            this.reportViewer1.TabIndex = 0;
            // 
            // getOrderInfoWithDeliveryTableAdapter
            // 
            this.getOrderInfoWithDeliveryTableAdapter.ClearBeforeFill = true;
            // 
            // getClientAddressTableAdapter
            // 
            this.getClientAddressTableAdapter.ClearBeforeFill = true;
            // 
            // getOrdersItemsTableAdapter
            // 
            this.getOrdersItemsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportOrderWithDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportOrderWithDelivery";
            this.Text = "Отчёт по заказу";
            this.Load += new System.EventHandler(this.ReportOrderWithDelivery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.getOrderInfoWithDeliveryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardware_StoreDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getClientAddressBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getOrdersItemsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource getOrderInfoWithDeliveryBindingSource;
        private Hardware_StoreDataSet hardware_StoreDataSet;
        private System.Windows.Forms.BindingSource getClientAddressBindingSource;
        private System.Windows.Forms.BindingSource getOrdersItemsBindingSource;
        private Hardware_StoreDataSetTableAdapters.getOrderInfoWithDeliveryTableAdapter getOrderInfoWithDeliveryTableAdapter;
        private Hardware_StoreDataSetTableAdapters.getClientAddressTableAdapter getClientAddressTableAdapter;
        private Hardware_StoreDataSetTableAdapters.getOrdersItemsTableAdapter getOrdersItemsTableAdapter;
    }
}