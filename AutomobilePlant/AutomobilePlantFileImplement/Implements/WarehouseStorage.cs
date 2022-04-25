using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantFileImplement.Models;

namespace AutomobilePlantFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;

        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses
            .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
            .Select(CreateModel)
            .ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses
            .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0
                ? source.Warehouses.Max(rec => rec.Id) : 0;
            var warehouse = new Warehouse
            {
                Id = maxId + 1,
                WarehouseDetails = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            source.Warehouses.Add(CreateModel(model, warehouse));
        }

        public void Update(WarehouseBindingModel model)
        {
            var warehouse = source.Warehouses
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }
            CreateModel(model, warehouse);
        }

        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        
        public bool CheckCountDetails(Dictionary<int, (string, int)> details, int detailCount)
        {
            foreach (var warehouseDetails in details)
            {
                int count = source.Warehouses
                    .Where(rec => rec.WarehouseDetails.ContainsKey(warehouseDetails.Key))
                    .Sum(rec => rec.WarehouseDetails[warehouseDetails.Key]);
                if (count < warehouseDetails.Value.Item2 * detailCount)
                {
                    return false;
                }
            }

            foreach (var detailsWarehouse in details)
            {
                int count = detailsWarehouse.Value.Item2 * detailCount;
                IEnumerable<Warehouse> warehouses = source.Warehouses
                    .Where(rec => rec.WarehouseDetails
                    .ContainsKey(detailsWarehouse.Key));

                foreach (var warehouse in warehouses)
                {
                    if (warehouse
                        .WarehouseDetails[detailsWarehouse.Key] <= count)
                    {
                        count -= warehouse.WarehouseDetails[detailsWarehouse.Key];
                        warehouse.WarehouseDetails.Remove(detailsWarehouse.Key);
                    }
                    else
                    {
                        warehouse.WarehouseDetails[detailsWarehouse.Key] -= count;
                        count = 0;
                    }

                    if (count == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.OwnerFullName = model.OwnerFullName;
            warehouse.DateCreate = model.DateCreate;
            foreach (var key in warehouse.WarehouseDetails.Keys.ToList())
            {
                if (!model.WarehouseDetails.ContainsKey(key))
                {
                    warehouse.WarehouseDetails.Remove(key);
                }
            }
            foreach (var detail in model.WarehouseDetails)
            {
                if (warehouse.WarehouseDetails.ContainsKey(detail.Key))
                {
                    warehouse.WarehouseDetails[detail.Key] = model.WarehouseDetails[detail.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseDetails.Add(detail.Key, model.WarehouseDetails[detail.Key].Item2);
                }
            }
            return warehouse;
        }
        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            Dictionary<int, (string, int)> warehouseDetails = new Dictionary<int, (string, int)>();

            foreach (var warehouseDetail in warehouse.WarehouseDetails)
            {
                string DetailName = string.Empty;
                foreach (var detail in source.Details)
                {
                    if (warehouseDetail.Key == detail.Id)
                    {
                        DetailName = detail.DetailName;
                        break;
                    }
                }
                warehouseDetails.Add(warehouseDetail.Key, (DetailName, warehouseDetail.Value));
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
