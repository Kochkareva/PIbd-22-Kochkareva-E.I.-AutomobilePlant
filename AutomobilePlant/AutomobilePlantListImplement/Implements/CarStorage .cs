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
    public class CarStorage : ICarStorage
    {
        private readonly DataListSingleton source;

        public CarStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CarViewModel> GetFullList()
        {
            var result = new List<CarViewModel>();
            foreach (var details in source.Cars)
            {
                result.Add(CreateModel(details));
            }
            return result;
        }

        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<CarViewModel>();
            foreach (var car in source.Cars)
            {
                if (car.CarName.Contains(model.CarName))
                {
                    result.Add(CreateModel(car));
                }
            }
            return result;
        }

        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var car in source.Cars)
            {
                if (car.Id == model.Id || car.CarName == model.CarName)
                {
                    return CreateModel(car);
                }
            }
            return null;
        }

        public void Insert(CarBindingModel model)
        {
            var tempCar = new Car
            {
                Id = 1,
                CarDetails = new Dictionary<int, int>()
            };
            foreach (var car in source.Cars)
            {
                if (car.Id >= tempCar.Id)
                {
                    tempCar.Id = car.Id + 1;
                }
            }
            source.Cars.Add(CreateModel(model, tempCar));
        }

        public void Update(CarBindingModel model)
        {
            Car tempCar = null;
            foreach (var car in source.Cars)
            {
                if (car.Id == model.Id)
                {
                    tempCar = car;
                }
            }
            if (tempCar == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempCar);
        }

        public void Delete(CarBindingModel model)
        {
            for (int i = 0; i < source.Cars.Count; ++i)
            {
                if (source.Cars[i].Id == model.Id)
                {
                    source.Cars.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
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
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            var carDetails = new Dictionary<int, (string, int)>();
            foreach (var cd in car.CarDetails)
            {
                string detailName = string.Empty;
                foreach (var detail in source.Details)
                {
                    if (cd.Key == detail.Id)
                    {
                        detailName = detail.DetailName;
                        break;
                    }
                }
                carDetails.Add(cd.Key, (detailName, cd.Value));
            }
            return new CarViewModel
            {
                Id = car.Id,
                CarName = car.CarName,
                Price = car.Price,
                CarDetails = carDetails
            };
        }
    }
}
