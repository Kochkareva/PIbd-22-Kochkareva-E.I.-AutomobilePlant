using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobilePlantDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new AutomobilePlantDatabase();

            return context.Orders.Include(rec => rec.Car).Include(rec => rec.Client).Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                CarId = rec.CarId,
                ClientId = rec.ClientId,
                ClientFullName = rec.Client.FullName,
                CarName = rec.Car.CarName,
                Count = rec.Count,
                Sum = rec.Sum,
                Status = rec.Status.ToString(),
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement
            }).ToList();

        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            return context.Orders.Include(rec => rec.Car).Include(rec => rec.Client)
            .Where(rec => (rec.CarId == model.CarId) || (model.DateFrom.HasValue && model.DateTo.HasValue &&
            rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) || (rec.ClientId == model.ClientId))
           .Select(rec => new OrderViewModel
           {
               Id = rec.Id,
               CarId = rec.CarId,
               ClientId = rec.ClientId,
               ClientFullName = rec.Client.FullName,
               CarName = rec.Car.CarName,
               Count = rec.Count,
               Sum = rec.Sum,
               Status = rec.Status.ToString(),
               DateCreate = rec.DateCreate,
               DateImplement = rec.DateImplement
           }).ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            var order = context.Orders.Include(rec => rec.Car).Include(rec => rec.Client)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.CarId = model.CarId;
            order.ClientId = model.ClientId.Value;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ClientId = order.ClientId,
                ClientFullName = order.Client.FullName,
                CarName = order.Car.CarName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
