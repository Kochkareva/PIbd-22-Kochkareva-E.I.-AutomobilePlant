using System;
using System.Collections.Generic;
using System.Linq;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantFileImplement.Models;

namespace AutomobilePlantFileImplement.Implements
{
    public class CarStorage : ICarStorage
    {
        private readonly FileDataListSingleton source;

        public CarStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<CarViewModel> GetFullList()
        {
            return source.Cars.Select(CreateModel).ToList();
        }
        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Cars
            .Where(rec => rec.CarName.Contains(model.CarName))
            .Select(CreateModel)
            .ToList();
        }
        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var car = source.Cars
            .FirstOrDefault(rec => rec.CarName == model.CarName || rec.Id == model.Id);
            return car != null ? CreateModel(car) : null;
        }
        public void Insert(CarBindingModel model)
        {
            int maxId = source.Cars.Count > 0 ? source.Cars.Max(rec => rec.Id) : 0;
            var element = new Car
            {
                Id = maxId + 1,
                CarDetails = new Dictionary<int, int>()
            };
            source.Cars.Add(CreateModel(model, element));
        }
        public void Update(CarBindingModel model)
        {
            var element = source.Cars.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(CarBindingModel model)
        {
            Car element = source.Cars.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Cars.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Car CreateModel(CarBindingModel model, Car car)
        {
            car.CarName = model.CarName;
            car.Price = model.Price;
            // удаляем убранные
            foreach (var key in car.CarDetails.Keys.ToList())
            {
                if (!model.CarDetails.ContainsKey(key))
                {
                    car.CarDetails.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var detail in model.CarDetails)
            {
                if (car.CarDetails.ContainsKey(detail.Key))
                {
                    car.CarDetails[detail.Key] =
                   model.CarDetails[detail.Key].Item2;
                }
                else
                {
                    car.CarDetails.Add(detail.Key,
                   model.CarDetails[detail.Key].Item2);
                }
            }
            return car;
        }
        private CarViewModel CreateModel(Car car)
        {
            return new CarViewModel
            {
                Id = car.Id,
                CarName = car.CarName,
                Price = car.Price,
                CarDetails = car.CarDetails
                .ToDictionary(recPC => recPC.Key, recPC =>
                (source.Details.FirstOrDefault(recC => recC.Id ==
                recPC.Key)?.DetailName, recPC.Value))
            };
        }
    }
}
