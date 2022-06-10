using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using Unity;

namespace AutomobilePlantView
{
    public partial class FormMessageInfo : Form
    {
        private readonly IMessageInfoLogic _logic;
        private bool checkNext = false;
        private readonly int messagesOnPage = 2;
        private int numOfPage;
        private int currentPage = 0;
        public FormMessageInfo(IMessageInfoLogic logic)
        {
            if(messagesOnPage < 1)
            {
                messagesOnPage = 5;
            }
            InitializeComponent();
            _logic = logic;
        }

        private void FormMessages_Load(object sender, EventArgs e)
        {
            try
            {
                labelPage.Text = (currentPage + 1).ToString();
                var list = _logic.Read(null);
                numOfPage = list.Count / messagesOnPage;
                if(list.Count % messagesOnPage != 0)
                {
                    numOfPage++;
                }
                labelAllPage.Text = "из " + numOfPage.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _logic.Read(new MessageInfoBindingModel { SkipMessage = currentPage * messagesOnPage, TakeMessage = messagesOnPage });
                checkNext = !(currentPage >= numOfPage - 1);
                if (checkNext)
                {
                    buttonNext.Enabled = true;
                }
                else
                {
                    buttonNext.Enabled = false;
                }
                if (list != null)
                {
                    dataGridViewMessages.DataSource = list;
                    dataGridViewMessages.Columns[0].Visible = false;
                    dataGridViewMessages.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (currentPage != 0)
                {
                    buttonPrevious.Enabled = true;
                }
                else
                {
                    buttonPrevious.Enabled= false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (checkNext)
            {
                currentPage++;
                labelPage.Text = (currentPage + 1).ToString();
                buttonPrevious.Enabled = true;
                LoadData();
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if ((currentPage - 1) >= 0)
            {
                currentPage--;
                labelPage.Text = (currentPage + 1).ToString();
                buttonNext.Enabled = true;
                if (currentPage == 0)
                {
                    buttonPrevious.Enabled = false;
                }
                LoadData();
            }
        }

        private void buttonOpenMessage_Click(object sender, EventArgs e)
        {
            if (dataGridViewMessages.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormMessage>();
                form.Id = dataGridViewMessages.SelectedRows[0].Cells[0].Value.ToString();
                form.ShowDialog();
                LoadData();                
            }
        }
    }
}
