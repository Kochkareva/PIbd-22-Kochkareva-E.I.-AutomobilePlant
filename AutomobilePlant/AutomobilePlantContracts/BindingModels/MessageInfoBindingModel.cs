using System;

namespace AutomobilePlantContracts.BindingModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfoBindingModel
    {
        public int? ClientId { get; set; }
        public string MessageId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
        public int? SkipMessage { get; set; }
        public int? TakeMessage { get; set; }
        public bool isRead { get; set; }
        public string Reply { get; set; }
    }
}
