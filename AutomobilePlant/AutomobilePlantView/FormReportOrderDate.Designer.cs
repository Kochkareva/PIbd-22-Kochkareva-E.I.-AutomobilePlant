namespace AutomobilePlantView
{
    partial class FormReportOrderDate
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
            this.panel = new System.Windows.Forms.Panel();
            this.ButtonToPdf = new System.Windows.Forms.Button();
            this.ButtonMake = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.ButtonToPdf);
            this.panel.Controls.Add(this.ButtonMake);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1116, 69);
            this.panel.TabIndex = 0;
            // 
            // ButtonToPdf
            // 
            this.ButtonToPdf.Location = new System.Drawing.Point(219, 20);
            this.ButtonToPdf.Name = "ButtonToPdf";
            this.ButtonToPdf.Size = new System.Drawing.Size(94, 29);
            this.ButtonToPdf.TabIndex = 5;
            this.ButtonToPdf.Text = "В PDF";
            this.ButtonToPdf.UseVisualStyleBackColor = true;
            this.ButtonToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // ButtonMake
            // 
            this.ButtonMake.Location = new System.Drawing.Point(45, 20);
            this.ButtonMake.Name = "ButtonMake";
            this.ButtonMake.Size = new System.Drawing.Size(158, 29);
            this.ButtonMake.TabIndex = 4;
            this.ButtonMake.Text = "Сформировать";
            this.ButtonMake.UseVisualStyleBackColor = true;
            this.ButtonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // FormReportOrderDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 450);
            this.Controls.Add(this.panel);
            this.Name = "FormReportOrderDate";
            this.Text = "FormReportOrderDate";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button ButtonToPdf;
        private System.Windows.Forms.Button ButtonMake;
    }
}