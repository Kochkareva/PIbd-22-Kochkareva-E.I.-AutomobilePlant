using System;
using System.Collections.Generic;
using System.Linq;
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

            return context.Orders.Include(rec => rec.Car).Include(rec => rec.Client).Include(rec => rec.Implementer)
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                CarId = rec.CarId,
                ClientId = rec.ClientId,
                ClientFullName = rec.Client.FullName,
                ImplementerId = rec.ImplementerId,
                ImplementerFullName = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFullName : string.Empty,
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
            return context.Orders
            .Include(rec => rec.Car)
            .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue &&
                rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <=
                model.DateTo.Value.Date) ||
                (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
                (model.SearchStatus.HasValue && model.SearchStatus.Value == rec.Status) ||
                (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && model.Status == rec.Status))
           .Select(rec => new OrderViewModel
           {
               Id = rec.Id,
               CarId = rec.CarId,
               ClientId = rec.ClientId,
               ClientFullName = rec.Client.FullName,
               ImplementerId = rec.ImplementerId,
               ImplementerFullName = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFullName : string.Empty,
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
            var order = context.Orders.Include(rec => rec.Car).Include(rec => rec.Client).Include(rec => rec.Implementer)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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
            order.ImplementerId = model.ImplementerId;
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
                ImplementerId = order.ImplementerId,
                ImplementerFullName = order.ImplementerId.HasValue ? order.Implementer.ImplementerFullName : String.Empty,
                CarName = order.Car.CarName,
                Count = order.Count,
                Sum = order.Sum,
                Status = Enum.GetName(order.Status),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
