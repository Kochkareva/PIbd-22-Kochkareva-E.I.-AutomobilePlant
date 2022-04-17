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
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly DataListSingleton source;

        public WarehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            var result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                result.Add(CreateModel(warehouse));
            }
            return result;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.WarehouseName.Contains(model.WarehouseName))
                {
                    result.Add(CreateModel(warehouse));
                }
            }
            return result;
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id || warehouse.WarehouseName == model.WarehouseName)
                {
                    return CreateModel(warehouse);
                }
            }
            return null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            var tempWarehouse = new Warehouse
            {
                Id = 1,
                WarehouseDetails = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id >= tempWarehouse.Id)
                {
                    tempWarehouse.Id = warehouse.Id + 1;
                }
            }
            source.Warehouses.Add(CreateModel(model, tempWarehouse));
        }

        public void Update(WarehouseBindingModel model)
        {
            Warehouse tempWarehouse = null;
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id)
                {
                    tempWarehouse = warehouse;
                }
            }
            if (tempWarehouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWarehouse);
        }

        public void Delete(WarehouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }            

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.OwnerFullName = model.OwnerFullName;
            warehouse.DateCreate = model.DateCreate;

            foreach (int key in warehouse.WarehouseDetails.Keys.ToList())
            {
                if (!model.WarehouseDetails.ContainsKey(key))
                {
                    warehouse.WarehouseDetails.Remove(key);
                }
            }
            foreach (KeyValuePair<int, (string, int)> details in model.WarehouseDetails)
            {
                if (warehouse.WarehouseDetails.ContainsKey(details.Key))
                {
                    warehouse.WarehouseDetails[details.Key] = model.WarehouseDetails[details.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseDetails.Add(details.Key, model.WarehouseDetails[details.Key].Item2);
                }
            }
            return warehouse;
        }

        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            Dictionary<int, (string, int)> warehouseDetails = new Dictionary<int, (string, int)>();

            foreach (KeyValuePair<int, int> warehouseDetail in warehouse.WarehouseDetails)
            {
                string detailName = string.Empty;
                foreach (var detail in source.Details)
                {
                    if (warehouseDetail.Key == detail.Id)
                    {
                        detailName = detail.DetailName;
                        break;
                    }
                }
                warehouseDetails.Add(warehouseDetail.Key, (detailName, warehouseDetail.Value));
            }
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                OwnerFullName = warehouse.OwnerFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseDetails = warehouseDetails
            };
        }
    }
}
