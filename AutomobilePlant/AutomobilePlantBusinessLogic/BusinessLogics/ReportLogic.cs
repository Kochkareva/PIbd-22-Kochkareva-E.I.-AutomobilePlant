using System;
using System.Collections.Generic;
using System.Linq;
using AutomobilePlantBusinessLogic.OfficePackage;
using AutomobilePlantBusinessLogic.OfficePackage.HelperModels;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;
//
namespace AutomobilePlantBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IDetailStorage _detailStorage;
        private readonly ICarStorage _carStorage;
        private readonly IOrderStorage _orderStorage;

        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ICarStorage carStorage, IDetailStorage detailStorage, IOrderStorage orderStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
       AbstractSaveToPdf saveToPdf)
        {
            _carStorage = carStorage;
            _detailStorage = detailStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        /// <summary>
        /// Получение списка автомобилей с указанием, какие детали используются
        /// </summary>
        /// <returns></returns>
        public List<ReportCarDetailViewModel> GetCarDetail()
        {
            var cars = _carStorage.GetFullList();
            var list = new List<ReportCarDetailViewModel>();
            foreach (var car in cars)
            {
                var record = new ReportCarDetailViewModel
                {
                    CarName = car.CarName,
                    Details = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var detail in car.CarDetails)
                {
                    record.Details.Add(new Tuple<string, int>(detail.Value.Item1, detail.Value.Item2));
                    record.TotalCount += detail.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                CarName = x.CarName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение автомобилей в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveCarsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список автомобилей",
                Cars = _carStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение автомобилей с указаеним деталей в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveCarDetailToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список автомобилей",
                CarDetails = GetCarDetail()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

    }
}
