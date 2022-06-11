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
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        public string Subject { get; set; }
        [DisplayName("Текст")]
        public string Body { get; set; }
        [DisplayName("Прочитанно")]
        public bool isRead { get; set; }
        [DisplayName("Ответ")]
        public string Reply { get; set; }
    }
}
