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
using AutomobilePlantBusinessLogic.MailWorker;
using AutomobilePlantContracts.BusinessLogicsContracts;

namespace AutomobilePlantView
{
    public partial class FormMessage : Form
    {
        private string id;
        public string Id { set { id = value; } }

        private readonly IMessageInfoLogic _logic;
        private readonly AbstractMailWorker _mailWorker;
        public FormMessage(IMessageInfoLogic logic, AbstractMailWorker mailWorker)
        {
            InitializeComponent();
            _logic = logic;
            _mailWorker = mailWorker;
        }
        private void FormMessage_Load(object sender, EventArgs e)
        {
            if (id != null)
            {
                try
                {
                    var view = _logic.Read(new MessageInfoBindingModel
                    {
                        MessageId = id,
                    })?[0];

                    if (view != null)
                    {
                        labelSenderName.Text = view.SenderName;
                        labelSubject.Text = view.Subject;
                        foreach (string line in view.Body.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
                        {
                            listBoxBody.Items.Add(line);
                        }
                        textBoxReply.Text = view.Reply;
                    }
                    if (view.Reply != null)
                    {
                        textBoxReply.ReadOnly = true;
                        buttonSend.Enabled = false;
                        buttonSend.Visible = false;
                    }
                    if (!view.isRead)
                    {
                        _logic.Update(new MessageInfoBindingModel
                        {
                            MessageId = id,
                            isRead = true,
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {       
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxReply.Text))
            {
                MessageBox.Show("Введите ответ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.Update(new MessageInfoBindingModel
                {
                    MessageId = id,
                    Reply = textBoxReply.Text,
                });

                var message = _logic.Read(new MessageInfoBindingModel
                {
                    MessageId = id,
                })?[0];

                _mailWorker.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = message.SenderName,
                    Subject = $"Re: {message.Subject}",
                    Text = $"{textBoxReply.Text} \n\n | Ответ на: \n | {message.SenderName} {message.DateDelivery.ToShortTimeString()} \n | {message.Body}"
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
