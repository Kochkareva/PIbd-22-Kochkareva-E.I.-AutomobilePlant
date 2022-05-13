using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantBusinessLogic.OfficePackage;
using AutomobilePlantBusinessLogic.OfficePackage.HelperModels;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IDetailStorage _detailStorage;
        private readonly ICarStorage _carStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;

        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ICarStorage carStorage, IDetailStorage detailStorage, IOrderStorage orderStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
       AbstractSaveToPdf saveToPdf, IWarehouseStorage warehouseStorage)
        {
            _carStorage = carStorage;
            _detailStorage = detailStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
            _warehouseStorage = warehouseStorage;
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
        /// Получение списка складов с указанием, какие детали хранятся
        /// </summary>
        /// <returns></returns>
        public List<ReportWarehouseDetailsViewModel> GetWarehouseDetails()
        {
            var warehouses = _warehouseStorage.GetFullList();
            var list = new List<ReportWarehouseDetailsViewModel>();
            foreach (var warehouse in warehouses)
            {
                var record = new ReportWarehouseDetailsViewModel
                {
                    WarehouseName = warehouse.WarehouseName,
                    Details = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var detail in warehouse.WarehouseDetails)
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
        /// Получение списка заказов за определенный период по датам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersDateViewModel> GetOrdersDate()
        {
            return _orderStorage.GetFullList()
            .GroupBy(rec => rec.DateCreate.ToShortDateString())
            .Select(x => new ReportOrdersDateViewModel
            {
                DateCreate = Convert.ToDateTime(x.Key),
                Count = x.Count(),
                Sum = x.Sum(rec => rec.Sum)
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
        /// Сохранение складов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveWarehousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateTableDoc(new WordInfoWarehouses
            {
                FileName = model.FileName,
                Title = "Список складов",
                Warehouses = _warehouseStorage.GetFullList()
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
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersDateToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocOrdersDate(new PdfInfoOrdersDate
            {
                FileName = model.FileName,
                Title = "Список количества заказов по датам",
                Orders = GetOrdersDate()
            });
        }
        /// <summary>
        /// Сохранение деталей с указаеним складов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveWarehouseDetailsToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReportWarehouse(new ExcelInfoWarehouse
            {
                FileName = model.FileName,
                Title = "Список складов и деталей",
                WarehouseDetails = GetWarehouseDetails()
            });
        }
    }
}
