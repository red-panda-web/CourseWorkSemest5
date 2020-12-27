
namespace WpfApp1.Reports
{
    partial class ReportOrderWithoutDelivery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportOrderWithoutDelivery));
            this.getOrderInfoWithoutDeliveryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hardware_StoreDataSet = new WpfApp1.Hardware_StoreDataSet();
            this.getOrdersItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.getOrderInfoWithoutDeliveryTableAdapter = new WpfApp1.Hardware_StoreDataSetTableAdapters.getOrderInfoWithoutDeliveryTableAdapter();
            this.getOrdersItemsTableAdapter = new WpfApp1.Hardware_StoreDataSetTableAdapters.getOrdersItemsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.getOrderInfoWithoutDeliveryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardware_StoreDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getOrdersItemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // getOrderInfoWithoutDeliveryBindingSource
            // 
            this.getOrderInfoWithoutDeliveryBindingSource.DataMember = "getOrderInfoWithoutDelivery";
            this.getOrderInfoWithoutDeliveryBindingSource.DataSource = this.hardware_StoreDataSet;
            // 
            // hardware_StoreDataSet
            // 
            this.hardware_StoreDataSet.DataSetName = "Hardware_StoreDataSet";
            this.hardware_StoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            reportDataSource1.Value = this.getOrderInfoWithoutDeliveryBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.getOrdersItemsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WpfApp1.Reports.ReportOrderWithoutDelivery_blank.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(776, 426);
            this.reportViewer1.TabIndex = 0;
            // 
            // getOrderInfoWithoutDeliveryTableAdapter
            // 
            this.getOrderInfoWithoutDeliveryTableAdapter.ClearBeforeFill = true;
            // 
            // getOrdersItemsTableAdapter
            // 
            this.getOrdersItemsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportOrderWithoutDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportOrderWithoutDelivery";
            this.Text = "Отчёт по заказу";
            this.Load += new System.EventHandler(this.ReportOrderWithoutDelivery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.getOrderInfoWithoutDeliveryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardware_StoreDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getOrdersItemsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource getOrderInfoWithoutDeliveryBindingSource;
        private Hardware_StoreDataSet hardware_StoreDataSet;
        private System.Windows.Forms.BindingSource getOrdersItemsBindingSource;
        private Hardware_StoreDataSetTableAdapters.getOrderInfoWithoutDeliveryTableAdapter getOrderInfoWithoutDeliveryTableAdapter;
        private Hardware_StoreDataSetTableAdapters.getOrdersItemsTableAdapter getOrdersItemsTableAdapter;
    }
}