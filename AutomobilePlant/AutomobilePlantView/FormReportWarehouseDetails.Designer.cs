namespace AutomobilePlantView
{
    partial class FormReportWarehouseDetails
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonSaveToExcel = new System.Windows.Forms.Button();
            this.WarehouseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WarehouseName,
            this.DetailName,
            this.Count});
            this.dataGridView.Location = new System.Drawing.Point(21, 63);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(599, 375);
            this.dataGridView.TabIndex = 0;
            // 
            // ButtonSaveToExcel
            // 
            this.ButtonSaveToExcel.Location = new System.Drawing.Point(21, 12);
            this.ButtonSaveToExcel.Name = "ButtonSaveToExcel";
            this.ButtonSaveToExcel.Size = new System.Drawing.Size(162, 29);
            this.ButtonSaveToExcel.TabIndex = 1;
            this.ButtonSaveToExcel.Text = "Сохранить в Excel";
            this.ButtonSaveToExcel.UseVisualStyleBackColor = true;
            this.ButtonSaveToExcel.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
            // 
            // WarehouseName
            // 
            this.WarehouseName.HeaderText = "Название склада";
            this.WarehouseName.MinimumWidth = 6;
            this.WarehouseName.Name = "WarehouseName";
            this.WarehouseName.Width = 125;
            // 
            // DetailName
            // 
            this.DetailName.HeaderText = "Название детали";
            this.DetailName.MinimumWidth = 6;
            this.DetailName.Name = "DetailName";
            this.DetailName.Width = 125;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.MinimumWidth = 6;
            this.Count.Name = "Count";
            this.Count.Width = 125;
            // 
            // FormReportWarehouseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 450);
            this.Controls.Add(this.ButtonSaveToExcel);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormReportWarehouseDetails";
            this.Text = "Загруженность складов";
            this.Load += new System.EventHandler(this.FormReportWarehouseDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button ButtonSaveToExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
    }
}