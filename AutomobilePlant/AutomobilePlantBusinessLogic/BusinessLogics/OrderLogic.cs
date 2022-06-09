using System;
using System.Collections.Generic;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantContracts.Enums;
using AutomobilePlantBusinessLogic.MailWorker;

namespace AutomobilePlantBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IClientStorage _clientStorage;
        private readonly AbstractMailWorker _abstractMailWorker;

        public OrderLogic(IOrderStorage orderStorage, IClientStorage clientStorage, AbstractMailWorker abstractMailWorker)
        {
            _orderStorage = orderStorage;
            _clientStorage = clientStorage;
            _abstractMailWorker = abstractMailWorker;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }

            return _orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                CarId = model.CarId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят,
                DateCreate = DateTime.Now,
            });
            _abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = model.ClientId
                })?.Login,
                Subject = "Заказ создан",
                Text = $"Дата заказа: {DateTime.Now} Сумма заказа: {model.Sum}"
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Принят"))
            {
                throw new Exception("Заказ еще не принят");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                Status = OrderStatus.Выполняется,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now
            });
            _abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Login,
                Subject = $"Заказ {order.Id}",
                Text = $"Заказ {order.Id} передан в работу"
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Выполняется"))
            {
                throw new Exception("Заказ не начал выполняться");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                Status = OrderStatus.Готов,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now
            });
            _abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Login,
                Subject = $"Заказ {order.Id}",
                Text = $"Заказ {order.Id} готов"
            });
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Готов"))
            {
                throw new Exception("Заказ не готов");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                Status = OrderStatus.Выдан,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now
            });
            _abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Login,
                Subject = $"Заказ {order.Id}",
                Text = $"Заказ {order.Id} выдан"
            });
        }
    }
}
