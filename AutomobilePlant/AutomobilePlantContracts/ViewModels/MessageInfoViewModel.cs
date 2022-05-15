using AutomobilePlantContracts.Attributes;
using System;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }

        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
