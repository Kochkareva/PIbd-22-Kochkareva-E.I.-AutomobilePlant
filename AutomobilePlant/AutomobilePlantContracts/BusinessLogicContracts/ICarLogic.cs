using System;
using System.Collections.Generic;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantContracts.BusinessLogicsContracts
{
    public interface ICarLogic
    {
        List<CarViewModel> Read(CarBindingModel model);

        void CreateOrUpdate(CarBindingModel model);

        void Delete(CarBindingModel model);
    }
}
