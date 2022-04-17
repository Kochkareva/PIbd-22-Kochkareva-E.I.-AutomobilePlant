using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantListImplement.Models;


namespace AutomobilePlantListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;

        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetFullList()
        {
            var result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                result.Add(CreateModel(order));
            }
            return result;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || (model.DateFrom.HasValue && model.DateTo.HasValue &&
                    order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo)
                    || (model.ClientId.HasValue && order.ClientId == model.ClientId.Value)
                    || (model.SearchStatus.HasValue && model.SearchStatus.Value == order.Status)
                    || (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && model.Status == order.Status))
                {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || order.CarId == model.CarId)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }

        public void Insert(OrderBindingModel model)
        {
            var tempOrder = new Order { Id = 1 };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }

        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.CarId = model.CarId;
            order.ClientId = model.ClientId.Value;
            order.ImplementerId = model.ImplementerId.Value;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            string carsName = string.Empty;
            foreach (var cars in source.Cars)
            {
                if (order.CarId == cars.Id)
                {
                    carsName = cars.CarName;
                    break;
                }
            }
            string ClientFullName = string.Empty;
            foreach(var client in source.Clients)
            {
                if(order.CarId == client.Id)
                {
                    ClientFullName = client.ClientFullName;
                    break;
                }
            }
            string ImplementerFullName = string.Empty;
            foreach(var implementer in source.Implementers)
            {
                if(order.ImplementerId == implementer.Id)
                {
                    ImplementerFullName = implementer.ImplementerFullName;
                    break;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ClientId = order.ClientId,
                ClientFullName = ClientFullName,
                ImplementerId = order.ImplementerId,
                ImplementerFullName = ImplementerFullName,
                CarName = carsName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
