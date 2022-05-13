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
    public class DetailStorage : IDetailStorage
    {
        private readonly FileDataListSingleton source;

        public DetailStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<DetailViewModel> GetFullList()
        {
            return source.Details
            .Select(CreateModel)
           .ToList();
        }

        public List<DetailViewModel> GetFilteredList(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Details
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
            var detail = source.Details
            .FirstOrDefault(rec => rec.DetailName == model.DetailName ||
           rec.Id == model.Id);
            return detail != null ? CreateModel(detail) : null;
        }

        public void Insert(DetailBindingModel model)
        {
            int maxId = source.Details.Count > 0 ? source.Details.Max(rec => rec.Id) : 0;
            var element = new Detail { Id = maxId + 1 };
            source.Details.Add(CreateModel(model, element));
        }

        public void Update(DetailBindingModel model)
        {
            var element = source.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(DetailBindingModel model)
        {
            Detail element = source.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Details.Remove(element);
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
        private DetailViewModel CreateModel(Detail detail)
        {
            return new DetailViewModel
            {
                Id = detail.Id,
                DetailName = detail.DetailName
            };
        }
    }
}
