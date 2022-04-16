using System;
using System.Collections.Generic;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantContracts.StoragesContracts
{
    public interface IDetailStorage
    {
        List<DetailViewModel> GetFullList();

        List<DetailViewModel> GetFilteredList(DetailBindingModel model);

        DetailViewModel GetElement(DetailBindingModel model);

        void Insert(DetailBindingModel model);

        void Update(DetailBindingModel model);

        void Delete(DetailBindingModel model);
    }
}
