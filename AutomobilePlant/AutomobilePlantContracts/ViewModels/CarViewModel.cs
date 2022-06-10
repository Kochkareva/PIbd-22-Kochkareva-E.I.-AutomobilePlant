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

        [DisplayName("Название автомобиля")]
        public string CarName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> CarDetails { get; set; }
    }
}
