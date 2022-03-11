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
    public class DetailStorage : IDetailStorage
    {
        private readonly DataListSingleton source;

        public DetailStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<DetailViewModel> GetFullList()
        {
            var result = new List<DetailViewModel>();
            foreach (var detail in source.Details)
            {
                result.Add(CreateModel(detail));
            }
            return result;
        }

        public List<DetailViewModel> GetFilteredList(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<DetailViewModel>();
            foreach (var detail in source.Details)
            {
                if (detail.DetailName.Contains(model.DetailName))
                {
                    result.Add(CreateModel(detail));
                }
            }
            return result;
        }

        public DetailViewModel GetElement(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var detail in source.Details)
            {
                if (detail.Id == model.Id || detail.DetailName == model.DetailName)
                {
                    return CreateModel(detail);
                }
            }
            return null;
        }

        public void Insert(DetailBindingModel model)
        {
            var tempDetail = new Detail { Id = 1 };
            foreach (var detail in source.Details)
            {
                if (detail.Id >= tempDetail.Id)
                {
                    tempDetail.Id = detail.Id + 1;
                }
            }
            source.Details.Add(CreateModel(model, tempDetail));
        }

        public void Update(DetailBindingModel model)
        {
            Detail tempDetail = null;
            foreach (var detail in source.Details)
            {
                if (detail.Id == model.Id)
                {
                    tempDetail = detail;
                }
            }
            if (tempDetail == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempDetail);
        }

        public void Delete(DetailBindingModel model)
        {
            for (int i = 0; i < source.Details.Count; ++i)
            {
                if (source.Details[i].Id == model.Id.Value)
                {
                    source.Details.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static Detail CreateModel(DetailBindingModel model, Detail detail)
        {
            detail.DetailName = model.DetailName;
            return detail;
        }

        private static DetailViewModel CreateModel(Detail details)
        {
            return new DetailViewModel
            {
                Id = details.Id,
                DetailName = details.DetailName
            };
        }
    }
}
