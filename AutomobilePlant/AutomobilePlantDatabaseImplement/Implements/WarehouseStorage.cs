using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobilePlantDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList()
        {
            using var context = new AutomobilePlantDatabase();
            return context.Warehouses
            .Include(rec => rec.WarehouseDetails)
            .ThenInclude(rec => rec.Detail)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            return context.Warehouses
            .Include(rec => rec.WarehouseDetails)
            .ThenInclude(rec => rec.Detail)
            .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            var warehouse = context.Warehouses
            .Include(rec => rec.WarehouseDetails)
            .ThenInclude(rec => rec.Detail)
            .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Warehouse warehouse = new Warehouse()
                {
                    WarehouseName = model.WarehouseName,
                    OwnerFullName = model.OwnerFullName,
                    DateCreate = model.DateCreate
                };
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
                CreateModel(model, warehouse, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(WarehouseBindingModel model)
        {

            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Warehouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public bool CheckCountDetails(Dictionary<int, (string, int)> details, int detailCount)
        {
            using var context = new AutomobilePlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                foreach(KeyValuePair<int, (string, int)> detail in details)
                {
                    int requiredCount = detail.Value.Item2 * detailCount;
                    var warehouseDetails = context.WarehouseDetails.Where(rec => rec.DetailId == detail.Key);
                    foreach(var warehouseDetail in warehouseDetails)
                    {
                        if(warehouseDetail.Count <= requiredCount)
                        {
                            requiredCount -= warehouseDetail.Count;
                            context.WarehouseDetails.Remove(warehouseDetail);
                        }
                        else
                        {
                            warehouseDetail.Count -= requiredCount;
                            requiredCount = 0;
                            break;
                        }
                    }
                    if (requiredCount != 0)
                    {
                        throw new Exception("Деталей на складе недостаточно");
                    }
                }
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, AutomobilePlantDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.OwnerFullName = model.OwnerFullName;
            warehouse.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var warehouseDetails = context.WarehouseDetails.Where(rec => rec.WarehouseId == model.Id.Value).ToList();
                context.WarehouseDetails.RemoveRange(warehouseDetails.Where(rec => !model.WarehouseDetails.ContainsKey(rec.DetailId)).ToList());
                context.SaveChanges();
                foreach (var updateDetail in warehouseDetails)
                {
                    updateDetail.Count = model.WarehouseDetails[updateDetail.DetailId].Item2;
                    model.WarehouseDetails.Remove(updateDetail.DetailId);
                }
                context.SaveChanges();
            }
            foreach (var wd in model.WarehouseDetails)
            {
                context.WarehouseDetails.Add(new WarehouseDetail
                {
                    WarehouseId = warehouse.Id,
                    DetailId = wd.Key,
                    Count = wd.Value.Item2
                });
                context.SaveChanges();
            }
            return warehouse;
        }
        private static WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                OwnerFullName = warehouse.OwnerFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseDetails = warehouse.WarehouseDetails.ToDictionary(recWD => recWD.DetailId, recWD => (recWD.Detail?.DetailName, recWD.Count))
            };
        }
    }
}
