using Microsoft.AspNetCore.Mvc;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace AutomobilePlantRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseLogic _warehouseLogic;
        private readonly IDetailLogic _detailLogic;
        public WarehouseController(IWarehouseLogic warehouseLogic, IDetailLogic detailLogic)
        {
            _warehouseLogic = warehouseLogic;
            _detailLogic = detailLogic;
        }
        [HttpGet]
        public List<WarehouseViewModel> GetWarehouseList() => _warehouseLogic.Read(null)?.ToList();
        [HttpPost]
        public void CreateWarehouse(WarehouseBindingModel model) => _warehouseLogic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateWarehouse(WarehouseBindingModel model) => _warehouseLogic.CreateOrUpdate(model);
        [HttpPost]
        public void DeleteWarehouse(WarehouseBindingModel model) => _warehouseLogic.Delete(model);
        [HttpPost]
        public void FillingWarehouse(AddDetailBindingModel model) => _warehouseLogic.AddDetail(model);
        [HttpGet]
        public List<DetailViewModel> GetDetailList() => _detailLogic.Read(null)?.ToList();
    }
}
