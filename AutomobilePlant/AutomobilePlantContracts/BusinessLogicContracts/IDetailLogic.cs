using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantContracts.BusinessLogicsContracts
{
    public interface IDetailLogic
    {
        List<DetailViewModel> Read(DetailBindingModel model);

        void CreateOrUpdate(DetailBindingModel model);

        void Delete(DetailBindingModel model);
    }
}
