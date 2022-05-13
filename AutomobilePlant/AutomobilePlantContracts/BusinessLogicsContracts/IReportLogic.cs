using System;
using System.Collections.Generic;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// <summary>
        /// Получение списка деталей с указанием, в каких автомобилях используются
        /// </summary>
        /// <returns></returns>
        List<ReportCarDetailViewModel> GetCarDetail();
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        /// <summary>
        /// Сохранение автомобилей в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveCarsToWordFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение автомобилей с указаеним деталей в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveCarDetailToExcelFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveOrdersToPdfFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение складов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveWarehousesToWordFile(ReportBindingModel model);
        /// <summary>
        /// Получение списка складов с указанием, какие детали хранятся
        /// </summary>
        /// <returns></returns>
        List<ReportWarehouseDetailsViewModel> GetWarehouseDetails();
        void SaveWarehouseDetailsToExcelFile(ReportBindingModel model);
        void SaveOrdersDateToPdfFile(ReportBindingModel model);
        List<ReportOrdersDateViewModel> GetOrdersDate();
    }
}
