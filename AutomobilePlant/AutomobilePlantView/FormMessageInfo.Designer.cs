namespace AutomobilePlantView
{
    partial class FormMessageInfo
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
            this.dataGridViewMessages = new System.Windows.Forms.DataGridView();
            this.buttonRef = new System.Windows.Forms.Button();
            this.buttonOpenMessage = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.labelAllPage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMessages
            // 
            this.dataGridViewMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMessages.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewMessages.Name = "dataGridViewMessages";
            this.dataGridViewMessages.RowHeadersWidth = 51;
            this.dataGridViewMessages.RowTemplate.Height = 29;
            this.dataGridViewMessages.Size = new System.Drawing.Size(733, 426);
            this.dataGridViewMessages.TabIndex = 0;
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(12, 455);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(94, 29);
            this.buttonRef.TabIndex = 1;
            this.buttonRef.Text = "Обновить";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // buttonOpenMessage
            // 
            this.buttonOpenMessage.Location = new System.Drawing.Point(126, 455);
            this.buttonOpenMessage.Name = "buttonOpenMessage";
            this.buttonOpenMessage.Size = new System.Drawing.Size(94, 29);
            this.buttonOpenMessage.TabIndex = 2;
            this.buttonOpenMessage.Text = "Открыть";
            this.buttonOpenMessage.UseVisualStyleBackColor = true;
            this.buttonOpenMessage.Click += new System.EventHandler(this.buttonOpenMessage_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(262, 455);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(139, 29);
            this.buttonPrevious.TabIndex = 3;
            this.buttonPrevious.Text = "<<";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(562, 455);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(156, 29);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = ">>";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(420, 460);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(50, 20);
            this.labelPage.TabIndex = 5;
            this.labelPage.Text = "label1";
            // 
            // labelAllPage
            // 
            this.labelAllPage.AutoSize = true;
            this.labelAllPage.Location = new System.Drawing.Point(476, 459);
            this.labelAllPage.Name = "labelAllPage";
            this.labelAllPage.Size = new System.Drawing.Size(50, 20);
            this.labelAllPage.TabIndex = 6;
            this.labelAllPage.Text = "label1";
            // 
            // FormMessageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 496);
            this.Controls.Add(this.labelAllPage);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonOpenMessage);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.dataGridViewMessages);
            this.Name = "FormMessageInfo";
            this.Text = "Письма клиентов";
            this.Load += new System.EventHandler(this.FormMessages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMessages;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Button buttonOpenMessage;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Label labelAllPage;
    }
}