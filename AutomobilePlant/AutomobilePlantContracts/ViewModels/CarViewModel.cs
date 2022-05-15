using AutomobilePlantContracts.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    // <summary>
    /// Автомобиль, изготавливаемый на автомобильном заводе
    /// </summary>
    public class CarViewModel
    {
        public int Id { get; set; }

        [Column(title: "Название автомобиля", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string CarName { get; set; }

        [Column(title: "Цена", gridViewAutoSize: GridViewAutoSize.Fill)]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> CarDetails { get; set; }
    }
}
