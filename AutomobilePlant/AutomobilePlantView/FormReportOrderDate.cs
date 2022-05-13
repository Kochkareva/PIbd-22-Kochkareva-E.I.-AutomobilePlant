using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using Microsoft.Reporting.WinForms;

namespace AutomobilePlantView
{
    public partial class FormReportOrderDate : Form
    {
        private readonly ReportViewer reportViewer;

        private readonly IReportLogic _logic;
        public FormReportOrderDate(IReportLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            reportViewer = new ReportViewer
            {
                Dock = DockStyle.Fill
            };
            reportViewer.LocalReport.LoadReportDefinition(new FileStream("ReportOrderDate.rdlc", FileMode.Open));
            Controls.Clear();
            Controls.Add(reportViewer);
            Controls.Add(panel);
        }

        private void ButtonMake_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = _logic.GetOrdersDate();
                var source = new ReportDataSource("DataSetOrdersDate", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonToPdf_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _logic.SaveOrdersDateToPdfFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
