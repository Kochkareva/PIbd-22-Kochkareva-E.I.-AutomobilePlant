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
    public class CarStorage : ICarStorage
    {
        public List<CarViewModel> GetFullList()
        {
            using var context = new AutomobilePlantDatabase();
            return context.Cars
            .Include(rec => rec.CarDetails)
            .ThenInclude(rec => rec.Detail)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            return context.Cars
            .Include(rec => rec.CarDetails)
            .ThenInclude(rec => rec.Detail)
            .Where(rec => rec.CarName.Contains(model.CarName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            var car = context.Cars
            .Include(rec => rec.CarDetails)
            .ThenInclude(rec => rec.Detail)
            .FirstOrDefault(rec => rec.CarName == model.CarName ||
            rec.Id == model.Id);
            return car != null ? CreateModel(car) : null;
        }
        public void Insert(CarBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Car car = new Car()
                {
                    CarName = model.CarName,
                    Price = model.Price
                };
                context.Cars.Add(car);
                context.SaveChanges();
                CreateModel(model, car, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(CarBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(CarBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            Car element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Cars.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Car CreateModel(CarBindingModel model, Car car, AutomobilePlantDatabase context)
        {
            car.CarName = model.CarName;
            car.Price = model.Price;
            if (model.Id.HasValue)
            {
                var carDetails = context.CarDetails.Where(rec =>
               rec.CarId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.CarDetails.RemoveRange(carDetails.Where(rec =>
               !model.CarDetails.ContainsKey(rec.DetailId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateDetail in carDetails)
                {
                    updateDetail.Count =
                   model.CarDetails[updateDetail.DetailId].Item2;
                    model.CarDetails.Remove(updateDetail.DetailId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.CarDetails)
            {
                context.CarDetails.Add(new CarDetail
                {
                    CarId = car.Id,
                    DetailId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return car;
        }
        private static CarViewModel CreateModel(Car car)
        {
            return new CarViewModel
            {
                Id = car.Id,
                CarName = car.CarName,
                Price = car.Price,
                CarDetails = car.CarDetails
            .ToDictionary(recPC => recPC.DetailId,
            recPC => (recPC.Detail?.DetailName, recPC.Count))
            };
        }
    }
}
