using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantDatabaseImplement.Models;
using AutomobilePlantContracts.BindingModels;

namespace AutomobilePlantDatabaseImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new AutomobilePlantDatabase();
            return context.MessageInfoes
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = rec.MessageId,
                SenderName = rec.SenderName,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body,
                isRead = rec.isRead,
                Reply = rec.Reply,
            })
            .ToList();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            if (model.SkipMessage.HasValue && model.TakeMessage.HasValue && !model.ClientId.HasValue)
            {

                return context.MessageInfoes.Skip((int)model.SkipMessage).Take((int)model.TakeMessage)
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body,
                    isRead = rec.isRead,
                    Reply = rec.Reply,
                })
                .ToList();
            }

            return context.MessageInfoes
                .Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) || (!model.ClientId.HasValue &&
                rec.DateDelivery.Date == model.DateDelivery.Date) || (model.MessageId != null && rec.MessageId.Equals(model.MessageId)))
                .Skip(model.SkipMessage ?? 0)
                .Take(model.TakeMessage ?? context.MessageInfoes.Count())
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body,
                    isRead = rec.isRead,
                    Reply = rec.Reply,
                })
                .ToList();
        }
        public void Insert(MessageInfoBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            MessageInfo element = context.MessageInfoes.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                return;
            }
            context.MessageInfoes.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body,
                isRead = model.isRead,
                Reply = model.Reply
            });
            context.SaveChanges();
        }

        public void Update(MessageInfoBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            var element = context.MessageInfoes.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element == null)
            {
                throw new Exception("Письмо не найдено");
            }
            element.isRead = model.isRead;
            element.Reply = model.Reply;
            context.SaveChanges();
        }
    }
}
