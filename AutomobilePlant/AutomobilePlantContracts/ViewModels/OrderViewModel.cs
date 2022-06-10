using AutomobilePlantContracts.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AutomobilePlantContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ClientId { get; set; }

        public int? ImplementerId { get; set; }

        [Column(title: "Исполнитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFullName { get; set; }
        
        [Column(title: "Клиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFullName { get; set; }

        [Column(title: "Автомобиль", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string CarName { get; set; }

        [Column(title: "Количество", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int Count { get; set; }

        [Column(title: "Сумма", gridViewAutoSize: GridViewAutoSize.Fill)]
        public decimal Sum { get; set; }

        [Column(title: "Статус", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Status { get; set; }

        [Column(title: "Дата создания", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime DateCreate { get; set; }

        [Column(title: "Дата выполнения", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime? DateImplement { get; set; }
    }
}
