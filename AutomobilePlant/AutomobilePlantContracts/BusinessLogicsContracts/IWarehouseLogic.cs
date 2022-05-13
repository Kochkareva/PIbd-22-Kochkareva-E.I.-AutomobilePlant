using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantContracts.BusinessLogicsContracts
{
    public interface IWarehouseLogic
    {
        List<WarehouseViewModel> Read(WarehouseBindingModel model);

        void CreateOrUpdate(WarehouseBindingModel model);

        void Delete(WarehouseBindingModel model);

        void AddDetail(AddDetailBindingModel model);
    }
}
