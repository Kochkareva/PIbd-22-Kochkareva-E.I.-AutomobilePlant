using System;
using System.Collections.Generic;
using System.Linq;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
using AutomobilePlantDatabaseImplement.Models;

namespace AutomobilePlantDatabaseImplement.Implements
{
    public class DetailStorage : IDetailStorage
    {
        public List<DetailViewModel> GetFullList()
        {
            using var context = new AutomobilePlantDatabase();
            return context.Details
            .Select(CreateModel)
            .ToList();
        }
        public List<DetailViewModel> GetFilteredList(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            return context.Details
            .Where(rec => rec.DetailName.Contains(model.DetailName))
            .Select(CreateModel)
            .ToList();
        }
        public DetailViewModel GetElement(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AutomobilePlantDatabase();
            var detail = context.Details
            .FirstOrDefault(rec => rec.DetailName == model.DetailName || rec.Id
           == model.Id);
            return detail != null ? CreateModel(detail) : null;
        }
        public void Insert(DetailBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            context.Details.Add(CreateModel(model, new Detail()));
            context.SaveChanges();
            
        }
        public void Update(DetailBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            var element = context.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(DetailBindingModel model)
        {
            using var context = new AutomobilePlantDatabase();
            Detail element = context.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Details.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Detail CreateModel(DetailBindingModel model, Detail detail)
        {
            detail.DetailName = model.DetailName;
            return detail;
        }
        private static DetailViewModel CreateModel(Detail detail)
        {
            return new DetailViewModel
            {
                Id = detail.Id,
                DetailName = detail.DetailName
            };
        }
    }
}
