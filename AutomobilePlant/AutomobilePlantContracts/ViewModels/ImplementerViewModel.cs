using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AutomobilePlantContracts.Attributes;

namespace AutomobilePlantContracts.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }

        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFullName { get; set; }

        [Column(title: "Время на заказ", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int PauseTime { get; set; }
    }
}
