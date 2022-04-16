﻿using System;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public int ClientId { get; set; }
        [DisplayName("ФИО Клиента")]
        public string ClientFullName { get; set; }

        [DisplayName("Автомобиль")]
        public string CarName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
