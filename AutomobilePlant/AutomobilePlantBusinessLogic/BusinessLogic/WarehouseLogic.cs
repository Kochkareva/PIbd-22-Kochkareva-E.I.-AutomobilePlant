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
    public class WarehouseLogic : IWarehouseLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly IDetailStorage _detailStorage;

        public WarehouseLogic(IWarehouseStorage warehouseStorage, IDetailStorage detailStorage)
        {
            _warehouseStorage = warehouseStorage;

            _detailStorage = detailStorage;
        }

        public List<WarehouseViewModel> Read(WarehouseBindingModel model){
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
            }
            return _warehouseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                WarehouseName = model.WarehouseName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _warehouseStorage.Delete(model);
        }

        public void AddDetail(AddDetailBindingModel model)
        {
            var warehouse = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.WarehouseId
            });

            var detail = _detailStorage.GetElement(new DetailBindingModel
            {
                Id = model.DetailId
            });

            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }

            if (detail == null)
            {
                throw new Exception("Деталь не найдена");
            }

            Dictionary<int, (string, int)> warehousedetails = warehouse.WarehouseDetails;

            if (warehousedetails.ContainsKey(model.DetailId))
            {
                warehousedetails[model.DetailId] = (warehousedetails[model.DetailId].Item1,
                    warehousedetails[model.DetailId].Item2 + model.Count);
            }
            else
            {
                warehousedetails.Add(model.DetailId, (detail.DetailName, model.Count));
            }

            _warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                OwnerFullName = warehouse.OwnerFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseDetails = warehousedetails
            });
        }
    }
}
