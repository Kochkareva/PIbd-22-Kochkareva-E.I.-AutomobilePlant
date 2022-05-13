using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantBusinessLogic.BusinessLogics
{
    public class CarLogic : ICarLogic
    {
        private readonly ICarStorage _carStorage;

        public CarLogic(ICarStorage carStorage)
        {
            _carStorage = carStorage;
        }

        public List<CarViewModel> Read(CarBindingModel model)
        {
            if (model == null)
            {
                return _carStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CarViewModel> { _carStorage.GetElement(model)};
            }
            return _carStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CarBindingModel model)
        {
            var element = _carStorage.GetElement(new CarBindingModel
            {
                CarName = model.CarName,
                Price = model.Price,
                CarDetails = model.CarDetails
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть автомобиль с таким названием");
            }
            if (model.Id.HasValue)
            {
                _carStorage.Update(model);
            }
            else
            {
                _carStorage.Insert(model);
            }
        }

        public void Delete(CarBindingModel model)
        {
            var element = _carStorage.GetElement(new CarBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Автомобиль не найден");
            }
            _carStorage.Delete(model);
        }
    }
}
