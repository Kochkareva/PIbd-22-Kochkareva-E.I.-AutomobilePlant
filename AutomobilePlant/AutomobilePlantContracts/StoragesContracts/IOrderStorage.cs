﻿using System;
using System.Collections.Generic;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantContracts.StoragesContracts
{
    public interface IOrderStorage
    {
        List<OrderViewModel> GetFullList();

        List<OrderViewModel> GetFilteredList(OrderBindingModel model);

        OrderViewModel GetElement(OrderBindingModel model);

        void Insert(OrderBindingModel model);

        void Update(OrderBindingModel model);

        void Delete(OrderBindingModel model);
    }
}
